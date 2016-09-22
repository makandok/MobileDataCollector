﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelToAndroidXML
{
    public class MetaDataProvider
    {
        public MetaDataProvider()
        {
            StringResourcesItems = new StringBuilder();
            ModelItems = new List<FieldItem>();
            resources = new Dictionary<string, string>();
        }

        Dictionary<string, string> resources;

        public void AddStringResource(string resourceName, string resourceValue)
        {
            if (resources.ContainsKey(resourceName))
                return;
            resources[resourceName] = resourceValue;
            StringResourcesItems.AppendLine(string.Format("<string name=\"{0}\">{1}</string>", resourceName, resourceValue));
        }
        public StringBuilder StringResourcesItems { get; set; }
        public List<FieldItem> ModelItems { get; set; }
    }

    public class SharedInstance
    {
        static SharedInstance instance;
        public static SharedInstance Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SharedInstance();

                }
                return instance;
            }
        }

        public MetaDataProvider metaDataProvider { get; set; }
    }

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

        string getData(List<FieldDefinition> viewFields)
        {
            var builder = new StringBuilder();
            foreach (var field in viewFields)
            {
                var fieldXml = string.Empty;
                switch (field.ViewType.ToLowerInvariant())
                {
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
                                fieldXml = (getXamlLabelDefForRadioGroup(field) + 
                                    getXamlLabelDefForEnumeratedFields(field) +                                        
                                    string.Join(Environment.NewLine, getXamlLabelDefForRadioButton(field))
                                    + "</RadioGroup >");
                            }
                            else
                            {
                                fieldXml = (getXamlLabelDefForEnumeratedFields(field) +
                                    getXamlLabelDefForRadioGroup(field) +
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
                builder.AppendLine(fieldXml);
            }
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
                    ,fieldType = field.ViewType
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
        android:inputType='" + (isNumeric?"number":"text") + @"'
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
                    pageName = ViewPageName
                                        ,
                    fieldType = field.ViewType

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

        string getXamlLabelDefForRadioGroup(FieldDefinition field)
        {
            var stringsEntryName = field.ViewName;
            var fieldXml = (@"
<RadioGroup
android:orientation='horizontal'
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
                        fieldType = field.ViewType

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
                        fieldType = field.ViewType

                    });

                fieldOptionDefinitions.Add(fieldXml.Replace("'", "\""));
            }
            return fieldOptionDefinitions;
        }
    }
}