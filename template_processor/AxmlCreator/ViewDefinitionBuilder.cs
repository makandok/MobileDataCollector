using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelToAndroidXML
{
    public class ViewDefinitionBuilder
    {
        internal const string SYS_DATE_SELECT_TEXT = "sys_dateselect";
        internal const string DATE_BUTTON_PREFIX = "dtbtn_";
        internal const string DATE_TEXT_PREFIX = "dttxt_";
        internal const string LABEL_PREFIX = "sylbl_";
        internal MetaDataProvider metaDataProvider;

        public string ViewPageName { get; set; }
        public List<FieldDefinition> ViewFields { get; set; }

        internal bool addDateTitleResource { get; set; }

        public ViewDefinitionBuilder()
        {
            ViewFields = new List<FieldDefinition>();
        }

        internal string getFieldList(string datasetLabel)
        {
            return string.Empty;
        }

        public string build()
        {
            if (ViewFields.Count == 0)
            {
                return string.Empty;
            }

            var start = @"<?xml version='1.0' encoding='utf-8'?>
<ScrollView xmlns:android='http://schemas.android.com/apk/res/android'
    android:layout_width='match_parent'
    android:layout_height='match_parent'>
<LinearLayout
    android:orientation='vertical'
    android:layout_width='match_parent'
    android:layout_height='wrap_content'>
<include layout='@layout/nextandprevbuttons' />
{0}
</LinearLayout>
</ScrollView>
";
            //metaDataProvider = new MetaDataProvider();

            metaDataProvider.AddStringResource(SYS_DATE_SELECT_TEXT, "Select Date");

            var asString = getData(ViewFields);

            var builder = new StringBuilder()
            .AppendFormat(start.Replace("'", "\""), asString);
            return builder.ToString();
        }

        StringBuilder processViews(List<FieldDefinition> groupFields)
        {
            var builder = new StringBuilder();
            //we add the horizontal linear layout
            //we get all fields and use their labels as headers
            //we get the first row defined as textview, label Type and get its choices
            //we'll use these to repeat the fields
            //for each choice in type, rename fields a bit and add all
            //repeat

            //we write the label
            var addColumnHeaders = false;
            var firstField = groupFields.FirstOrDefault(t => t.ViewType == "Unique Dataset" || t.ViewType == "Simple Table");
            for (var i =0; i< firstField.FieldOptions.Values.Count;i++)
            {
                var cleanItemName = firstField.FieldOptions.CleanValues[i];
                var itemName = firstField.FieldOptions.Values[i];
                var definitionBuilder = new MinimalViewDefinitionBuilder()
                {
                    ItemName = itemName,
                    FieldNameSuffix = cleanItemName,
                    metaDataProvider = this.metaDataProvider,
                    ViewPageName = this.ViewPageName
                };

                if (addColumnHeaders)
                {
                    addColumnHeaders = false;
                    var labels = (from field in groupFields
                                  where field != firstField && field.DisplayLabel.ToLowerInvariant()!="index"
                                  select field.DisplayLabel).ToList();
                    var headerText = definitionBuilder.getTableHeaderText(labels, firstField);
                    builder.Append(headerText);
                }

                foreach (var field in groupFields)
                {
                    if (field.DisplayLabel.ToLowerInvariant() == "index") continue;

                    var fieldXml = getViewDefinitions(definitionBuilder, field);
                    builder.Append(fieldXml);
                }
            }

            //var asString = builder.ToString();
            //System.IO.File.WriteAllText("tables.axml", asString);
            //System.IO.File.WriteAllText("meta.txt", this.metaDataProvider.StringResourcesItems.ToString());
            //Environment.Exit(0);

            return builder;
        }

        string getViewDefinitions(MinimalViewDefinitionBuilder definitionBuilder, FieldDefinition field)
        {
            var fieldXml = string.Empty;
            switch (field.ViewType.ToLowerInvariant())
            {
                case "simple table":
                case "unique dataset":
                    {
                        //we treat these as labels
                        fieldXml = definitionBuilder.getXamlDefinitionForGrandLabel(field);
                        break;
                    }
                case "int":
                case "integer":
                case "cell":
                case "cellnumber":
                case "number":
                    {
                        fieldXml = definitionBuilder.getXamlDefinitionForTextField(field, true);
                        break;
                    }
                case "time":
                    {
                        //fieldXml = definitionBuilder.getXamlDefinitionForDate(field, "TimePicker");
                        break;
                    }
                case "today":
                case "date":
                case "datepicker":
                    {
                        //fieldXml = definitionBuilder.getXamlDefinitionForDate(field, "DatePicker");
                        break;
                    }
                case "username":
                case "text":
                case "edittext":
                    {
                        fieldXml = definitionBuilder.getXamlDefinitionForTextField(field, false);
                        break;
                    }
                case "label":
                case "image":
                case "start group":
                case "block label":
                case "labelbig":
                case "textview":
                    {
                        fieldXml = definitionBuilder.getXamlDefinitionForLabel(field, true);
                        break;
                    }
                case "multiselect":
                case "single multiple":
                case "checkbox":
                    {
                        fieldXml = (
                            definitionBuilder.getXamlLabelDefForEnumeratedFields(field) +
                            string.Join(Environment.NewLine, definitionBuilder.getXamlLabelDefForCheckBox(field)) + @"");
                        break;
                    }
                case "singleselect":
                case "single select":
                case "radiogroup":
                    {
                        if (field.GridColumn == 5)
                        {
                            fieldXml = (definitionBuilder.getXamlLabelDefForRadioGroup(field, false) +
                                definitionBuilder.getXamlLabelDefForEnumeratedFields(field) +
                                string.Join(Environment.NewLine, definitionBuilder.getXamlLabelDefForRadioButton(field))
                                + "</RadioGroup >");
                        }
                        else
                        {
                            //we look at the length of the enums and  
                            //we change orientation if too long
                            var changeOrientation = false;
                            var lengths = (field.FieldOptions.Values.Select(t => t.Length)).Sum();
                            if (lengths > 50)
                                changeOrientation = true;

                            fieldXml = (definitionBuilder.getXamlLabelDefForEnumeratedFields(field) +
                                definitionBuilder.getXamlLabelDefForRadioGroup(field, changeOrientation) +
                                string.Join(Environment.NewLine, definitionBuilder.getXamlLabelDefForRadioButton(field))
                                + "</RadioGroup >");
                        }
                        break;
                    }
                default:
                    {
                        var fieldype = field.ViewType;
                        throw new ArgumentNullException("Please addlogic for " + field.ViewType);
                    }
            }
            return fieldXml;
        }

        string getData(List<FieldDefinition> viewFields)
        {
            //adding this here to make sure we process all fields correctly in the loop below
            var dummyField = new FieldDefinition() { ViewName = "dummy" };
            viewFields.Add(dummyField);

            var builder = new StringBuilder();

            var currentGroupName = string.Empty;
            List<FieldDefinition> groupFields = null;
            foreach (var field in viewFields)
            {
                if (!string.IsNullOrWhiteSpace(currentGroupName))
                {
                    if (currentGroupName == field.GroupName)
                    {
                        groupFields.Add(field);
                        continue;
                    }

                    //we process for the existing as we have transitioned to a new group
                    var defns = processViews(groupFields);
                    builder.AppendLine(defns.ToString());

                    //we reset the group
                    currentGroupName = string.Empty;
                    groupFields = new List<FieldDefinition>();
                }

                var fieldXml = string.Empty;

                if (field == dummyField)
                {
                    //we are at the end
                    break;
                }

                switch (field.ViewType.ToLowerInvariant())
                {
                    case "simple table":
                    case "unique dataset":
                        {
                            //we identify this is a unique dataset looking the first defn
                            currentGroupName = field.GroupName;
                            groupFields = new List<FieldDefinition>();

                            groupFields.Add(field);
                            break;
                        }
                    case "int":
                    case "integer":
                    case "cell":
                    case "cellnumber":
                    case "number":
                        {
                            fieldXml = getXamlDefinitionForTextField(field, true);
                            break;
                        }
                    case "time":
                        {
                            fieldXml = getXamlDefinitionForDate(field, "TimePicker");
                            break;
                        }
                    case "today":
                    case "date":
                    case "datepicker":
                        {
                            fieldXml = getXamlDefinitionForDate(field, "DatePicker");
                            break;
                        }
                    case "username":
                    case "text":
                    case "edittext":
                        {
                            fieldXml = getXamlDefinitionForTextField(field, false);
                            break;
                        }
                    case "label":
                    case "image":
                    case "start group":
                    case "block label":
                    case "labelbig":
                    case "textview":
                        {
                            fieldXml = getXamlDefinitionForLabel(field);
                            break;
                        }
                    case "multiselect":
                    case "single multiple":
                    case "checkbox":
                        {
                            fieldXml = (
                                getXamlLabelDefForEnumeratedFields(field) +
                                string.Join(Environment.NewLine, getXamlLabelDefForCheckBox(field)) + @"");
                            break;
                        }
                    case "singleselect":
                    case "single select":
                    case "radiogroup":
                        {
                            if (field.GridColumn == 5)
                            {
                                fieldXml = (getXamlLabelDefForRadioGroup(field, false) +
                                    getXamlLabelDefForEnumeratedFields(field) +
                                    string.Join(Environment.NewLine, getXamlLabelDefForRadioButton(field))
                                    + "</RadioGroup >");
                            }
                            else
                            {
                                //we look at the length of the enums and  
                                //we change orientation if too long
                                var changeOrientation = false;
                                var lengths = (field.FieldOptions.Values.Select(t => t.Length)).Sum();
                                if (lengths > 50)
                                    changeOrientation = true;

                                fieldXml = (getXamlLabelDefForEnumeratedFields(field) +
                                    getXamlLabelDefForRadioGroup(field, changeOrientation) +
                                    string.Join(Environment.NewLine, getXamlLabelDefForRadioButton(field))
                                    + "</RadioGroup >");
                            }
                            break;
                        }
                    default:
                        {
                            var fieldype = field.ViewType;
                            throw new ArgumentNullException("Please addlogic for " + field.ViewType);
                        }
                }
                if (!string.IsNullOrWhiteSpace(fieldXml))
                    builder.AppendLine(fieldXml);
            }

            viewFields.Remove(dummyField);
            return builder.ToString();
        }

        string getXamlDefinitionForDate(FieldDefinition field, string dataTypeLabel)
        {
            var stringsEntryText = field.DisplayLabel;
            var stringsEntryName = field.ViewName;
            //for dates, we do the following, show an EditText and a Buttton
            //button called dtbtn_{sysfieldname}, textfield called txt_{sysfieldname}
            //Button hass text: Select Date [{Label}]
            var fieldXml = getDateControlsDef(stringsEntryName);
            //we need this for the button label
            metaDataProvider.AddStringResource(stringsEntryName, stringsEntryText);
            metaDataProvider.ModelItems.Add(
                new FieldItem()
                {
                    dataType = dataTypeLabel,
                    name = stringsEntryName,
                    IsIndexed = field.IsIndexed == "1",
                    IsRequired = field.IsIndexed == "1",
                    Label = field.DisplayLabel,
                    pageName = ViewPageName
                    ,
                    fieldType = field.ViewType,
                    fieldName = ""
                });
            return fieldXml;
        }

        string getDateControlsDef(string fieldName)
        {
            var buttonName = DATE_BUTTON_PREFIX + fieldName;
            var textName = DATE_TEXT_PREFIX + fieldName;
            var labelName = LABEL_PREFIX + fieldName;

            var fieldXml = @"
    <TextView
        android:text='@string/" + fieldName + @"'
        android:textColor='@android:color/holo_blue_dark'
        android:textAppearance='?android:attr/textAppearanceMedium'
        android:layout_width='match_parent'
        android:layout_height='wrap_content'
        android:labelFor='@+id/" + textName + @"' />
    <LinearLayout
        android:orientation='horizontal'
        android:layout_width='match_parent'
        android:layout_height='wrap_content'>
        <Button
            android:id='@+id/" + buttonName + @"'
            android:layout_width='150dp'
            android:layout_height='55dp'
            android:text='@string/" + SYS_DATE_SELECT_TEXT + @"'/>
        <EditText
            android:text=''
            android:textColor='@android:color/holo_blue_dark'
            android:layout_width='220dp'
            android:layout_height='50dp'
            android:id='@+id/" + textName + @"'
            android:textSize='25sp' />
    </LinearLayout>";
            return fieldXml.Replace("'", "\"");
        }

        string getXamlDefinitionForTextField(FieldDefinition field, bool isNumeric)
        {
            var stringsEntryText = field.DisplayLabel;
            var stringsEntryName = field.ViewName;
            var fieldXml = (@"
    <TextView
        android:text='@string/" + stringsEntryName + @"'
        android:textColor='@android:color/holo_blue_dark'
        android:textAppearance='?android:attr/textAppearanceMedium'
        android:layout_width='match_parent'
        android:layout_height='wrap_content'
        android:labelFor='@+id/" + stringsEntryName + @"' />
    <EditText
        android:layout_height='40dp'
        android:layout_width='match_parent'
        android:inputType='" + (isNumeric ? "number" : "text") + @"'
        android:id='@+id/" + stringsEntryName + @"'
        android:hint='@string/" + stringsEntryName + @"' />");
            metaDataProvider.ModelItems.Add(
                new FieldItem()
                {
                    dataType = "EditText",
                    name = stringsEntryName,
                    IsIndexed = field.IsIndexed == "1",
                    IsRequired = field.IsIndexed == "1",
                    Label = field.DisplayLabel,
                    pageName = ViewPageName,
                    fieldType = field.ViewType,
                    fieldName = ""
                });
            metaDataProvider.AddStringResource(stringsEntryName, stringsEntryText);
            return fieldXml.Replace("'", "\"");
        }

        string getXamlDefinitionForLabel(FieldDefinition field)
        {
            var stringsEntryText = field.DisplayLabel;
            var stringsEntryName = field.ViewName;
            var headerText = string.Empty;
            if (field.GridColumn == 5 && field.FieldOptions.Values.Count > 0)
            {
                //this is a tale layout, which requires special logic to handle 
                var headerLabels = new List<string>();
                var isFirst = true;
                foreach (var option in field.FieldOptions.Values)
                {
                    headerLabels.Add(getTableHeaderText(field, option, isFirst));
                    isFirst = false;
                }
                var asString = string.Join(System.Environment.NewLine, headerLabels);
                var headerFieldsContainer = @"
    <LinearLayout
        android:background='@color/colorTableHeader'
        android:orientation='horizontal'
        android:layout_width='match_parent'
        android:layout_height='wrap_content'>
        {0}
    </LinearLayout>".Replace("'", "\"");

                headerText =
                    string.Format(headerFieldsContainer, asString);
            }

            var fieldXml = (@"
    <TextView
        android:text='@string/" + stringsEntryName + @"'
        android:background='@color/colorPrimaryDark'
        android:textColor='@color/white'
        android:textAppearance='?android:attr/textAppearanceLarge'
        android:layout_width='match_parent'
        android:layout_height='wrap_content'
        android:id='@+id/" + stringsEntryName + @"' />").Replace("'", "\"");

            //metaDataProvider.ModelItems.Add(new FieldItem() { dataType = "TextView", name = stringsEntryName });
            metaDataProvider.AddStringResource(stringsEntryName, stringsEntryText);
            return fieldXml + headerText;
        }

        private string getTableHeaderText(FieldDefinition field, string option, bool isFirst)
        {
            var optionName = field.ViewName + "_" + option.Clean();
            var leftMargin = isFirst ? 350 : 10;
            var fieldXml = @"
        <TextView
            android:textColor='@android:color/holo_blue_dark'
            android:gravity='center_vertical'
            android:layout_marginLeft='" + leftMargin + @"dp'
            android:text='@string/" + optionName + @"'
            android:layout_height='wrap_content'
            android:layout_width='100dp'
            android:textAppearance='?android:attr/textAppearanceMedium' />";
            metaDataProvider.AddStringResource(optionName, option);
            return fieldXml.Replace("'", "\"");
        }

        string getXamlLabelDefForEnumeratedFields(FieldDefinition field)
        {
            var stringsEntryText = field.DisplayLabel;
            var stringsEntryName = field.ViewName;
            var layoutWidth = field.GridColumn == 5 ? "350dp" : "match_parent";

            var fieldXml = (@"
    <TextView
android:text='@string/" + stringsEntryName + @"'
android:textColor='@android:color/holo_blue_dark'
android:textAppearance='?android:attr/textAppearanceMedium'
        android:minWidth='25dp'
        android:minHeight='25dp'
        android:layout_width='" + layoutWidth + @"'
        android:layout_height='wrap_content'
        android:id='@+id/l_" + stringsEntryName + "' />");
            metaDataProvider.AddStringResource(stringsEntryName, stringsEntryText);
            return fieldXml.Replace("'", "\"");
        }

        string getXamlLabelDefForRadioGroup(FieldDefinition field, bool setVertical)
        {
            var stringsEntryName = field.ViewName;
            var fieldXml = (@"
<RadioGroup
android:orientation='" + (setVertical ? "vertical" : "horizontal") + @"'
        android:minWidth='25dp'
        android:minHeight='25dp'
        android:layout_width='match_parent'
        android:layout_height='wrap_content'
        android:id='@+id/rg_" + stringsEntryName + "' >");
            return fieldXml.Replace("'", "\"");
        }

        List<string> getXamlLabelDefForCheckBox(FieldDefinition field)
        {
            var fieldOptionDefinitions = new List<string>();
            foreach (var option in field.FieldOptions.Values)
            {
                var optionName = field.ViewName + "_" + option.Clean();
                var fieldXml = (@" <CheckBox
            android:layout_width='wrap_content'
            android:layout_height='wrap_content'
            android:checked='false'
             android:text='@string/" + optionName + @"'
             android:id='@+id/" + optionName + "' />"
                 );
                metaDataProvider.ModelItems.Add(
                    new FieldItem()
                    {
                        dataType = "CheckBox",
                        name = optionName,
                        IsIndexed = field.IsIndexed == "1",
                        IsRequired = field.IsIndexed == "1",
                        Label = field.DisplayLabel + " [" + option + "]",
                        pageName = ViewPageName,
                        fieldType = field.ViewType,
                        fieldName = field.ViewName,
                        listName = field.ListName,
                        lookupValue = option
                    });
                metaDataProvider.AddStringResource(optionName, option);

                fieldOptionDefinitions.Add(fieldXml.Replace("'", "\""));
            }
            return fieldOptionDefinitions;
        }

        List<string> getXamlLabelDefForRadioButton(FieldDefinition field)
        {
            var fieldOptionDefinitions = new List<string>();
            var isFirst = true;
            foreach (var option in field.FieldOptions.Values)
            {
                var optionName = field.ViewName + "_" + option.Clean();
                var fieldXml = string.Empty;
                if (field.GridColumn == 5)
                {
                    fieldXml = (@"
<RadioButton
    android:layout_marginStart='10dp'
    android:layout_width='100dp'
    android:layout_height='wrap_content'
    android:checked='false'
android:text=''
android:id='@+id/" + optionName + @"' />"
);
                }
                else
                {
                    fieldXml = (@"
<RadioButton
            android:layout_marginStart='30dp'
            android:layout_width='wrap_content'
            android:layout_height='wrap_content'
            android:checked='false'
            android:text='@string/" + optionName + @"'
android:id='@+id/" + optionName + "' />"
    );
                    metaDataProvider.AddStringResource(optionName, option);
                }

                metaDataProvider.ModelItems.Add(
                    new FieldItem()
                    {
                        dataType = "RadioButton",
                        name = optionName,
                        IsIndexed = field.IsIndexed == "1",
                        IsRequired = field.IsIndexed == "1",
                        Label = field.DisplayLabel + " [" + option + "]",
                        pageName = ViewPageName,
                        fieldType = field.ViewType,
                        fieldName = field.ViewName,
                        listName = field.ListName,
                        lookupValue = option
                    });

                fieldOptionDefinitions.Add(fieldXml.Replace("'", "\""));
            }
            return fieldOptionDefinitions;
        }
    }

}
