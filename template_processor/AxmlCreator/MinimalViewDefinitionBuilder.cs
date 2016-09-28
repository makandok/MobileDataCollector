using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToAndroidXML
{
    public class MinimalViewDefinitionBuilder
    {
        internal const string SYS_DATE_SELECT_TEXT = "sys_dateselect";
        internal const string DATE_BUTTON_PREFIX = "dtbtn_";
        internal const string DATE_TEXT_PREFIX = "dttxt_";
        internal const string LABEL_PREFIX = "sylbl_";

        public string ViewPageName { get; set; }
        public string FieldNameSuffix { get; set; }
        public string ItemName { get; set; }

        string suffixSpacer = "_sx_";
        internal MetaDataProvider metaDataProvider;

        public string getXamlDefinitionForTextField(FieldDefinition field, bool isNumeric)
        {            
            var stringsEntryText = field.DisplayLabel;
            var stringsEntryName = field.ViewName;
            var suffixedName = field.ViewName + suffixSpacer + ItemName.Clean();
            var fieldXml = (@"
    <TextView
        android:text='@string/" + stringsEntryName + @"'
        android:textColor='@android:color/holo_blue_dark'
        android:textAppearance='?android:attr/textAppearanceMedium'
        android:layout_width='match_parent'
        android:layout_height='wrap_content'
        android:layout_marginLeft='50dp'
        android:labelFor='@+id/" + suffixedName + @"' />
    <EditText
        android:layout_height='40dp'
        android:layout_width='match_parent'
        android:layout_marginLeft='50dp'
        android:inputType='" + (isNumeric ? "number" : "text") + @"'
        android:id='@+id/" + suffixedName + @"'/>"
);
            metaDataProvider.ModelItems.Add(
                new FieldItem()
                {
                    dataType = "EditText",
                    name = suffixedName,
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

        public string getXamlDefinitionForGrandLabel(FieldDefinition field, bool setTypeValue = false)
        {
            var stringsEntryText = field.DisplayLabel;
            var stringsEntryName = field.ViewName;
            var suffixedName = field.ViewName + suffixSpacer + ItemName.Clean();

            var fieldXml = (@"
    <TextView
        android:text='@string/" + stringsEntryName + @"'
        android:background='@color/colorPrimaryDark'
        android:textColor='@color/white'
        android:textAppearance='?android:attr/textAppearanceLarge'
        android:layout_width='match_parent'
        android:layout_height='wrap_content'
        android:id='@+id/l_" + suffixedName + @"' />").Replace("'", "\"");

            metaDataProvider.AddStringResource(stringsEntryName, stringsEntryText);
            return fieldXml;
        }

        public string getXamlDefinitionForLabel(FieldDefinition field, bool setTypeValue = false)
        {
            var stringsEntryText = field.DisplayLabel;
            //var stringsEntryName = field.ViewName;
            //var headerText = string.Empty;
            var suffixedName = field.ViewName + suffixSpacer + ItemName.Clean();
            if (setTypeValue)
            {
                stringsEntryText = ItemName;
            }

            var fieldXml = (@"
    <TextView
        android:text='@string/" + suffixedName + @"'
        android:background='@color/colorPrimaryDark'
        android:textColor='@color/white'
        android:textAppearance='?android:attr/textAppearanceLarge'
        android:layout_width='match_parent'
        android:layout_height='wrap_content'
        android:id='@+id/" + suffixedName + @"' />").Replace("'", "\"");

            if (setTypeValue)
            {
                metaDataProvider.ModelItems.Add(
    new FieldItem()
    {
        dataType = "TextView",
        name = suffixedName,
        IsIndexed = field.IsIndexed == "1",
        IsRequired = field.IsIndexed == "1",
        Label = field.DisplayLabel,
        pageName = ViewPageName
                            ,
        fieldType = field.ViewType
    });
            }

            metaDataProvider.AddStringResource(suffixedName, stringsEntryText);
            return fieldXml; //+ headerText;
        }

        public string getTableHeaderText(List<string> headers, FieldDefinition field)
        {
            var builder = new StringBuilder();
            var rnd = new Random(DateTime.Now.Millisecond);
            var isFirst = true;
            builder.Append(@"
 <LinearLayout
    android:orientation='horizontal'
    android:layout_width='match_parent'
    android:layout_height='wrap_content'>");
            foreach (var header in headers)
            {
                var optionName = field.ViewName + "_" + rnd.Next();
                var leftMargin = isFirst ? 350 : 10;
                isFirst = false;
                var fieldXml = @"
        <TextView
            android:textColor='@android:color/holo_blue_dark'
            android:gravity='center_vertical'
            android:layout_marginLeft='" + leftMargin + @"dp'
            android:text='@string/" + optionName + @"'
            android:layout_height='wrap_content'
            android:layout_width='0dp'
            android:layout_weight='1'
            android:textAppearance='?android:attr/textAppearanceMedium' />";
                metaDataProvider.AddStringResource(optionName, header);
                builder.Append(fieldXml.Replace("'", "\""));
            }
            builder.Append("</LinearLayout>");
            return builder.ToString();
        }

        public string getTableHeaderText(FieldDefinition field, string option, bool isFirst)
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

        public string getXamlLabelDefForRadioGroup(FieldDefinition field, bool setVertical)
        {
            var stringsEntryName = field.ViewName;
            var suffixedName = field.ViewName + suffixSpacer + ItemName.Clean();
            var fieldXml = (@"
<RadioGroup
android:orientation='" + (setVertical ? "vertical" : "horizontal") + @"'
        android:minWidth='25dp'
        android:minHeight='25dp'
        android:layout_width='match_parent'
        android:layout_height='wrap_content'
        android:id='@+id/rg_" + suffixedName + "' >");
            return fieldXml.Replace("'", "\"");
        }

        public List<string> getXamlLabelDefForCheckBox(FieldDefinition field)
        {
            var fieldOptionDefinitions = new List<string>();
            foreach (var option in field.FieldOptions.Values)
            {
                var optionName = field.ViewName + "_" + option.Clean();
                var suffixedName = optionName + suffixSpacer + FieldNameSuffix;
                var fieldXml = (@" <CheckBox
            android:layout_width='wrap_content'
            android:layout_height='wrap_content'
            android:checked='false'
             android:text='@string/" + optionName + @"'
             android:id='@+id/" + suffixedName + "' />"
                 );
                metaDataProvider.ModelItems.Add(
                    new FieldItem()
                    {
                        dataType = "CheckBox",
                        name = suffixedName,
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

        public string getXamlLabelDefForEnumeratedFields(FieldDefinition field)
        {
            var stringsEntryText = field.DisplayLabel;
            var stringsEntryName = field.ViewName;
            var suffixedName = stringsEntryName + suffixSpacer + ItemName.Clean();
            var layoutWidth = field.GridColumn == 5 ? "350dp" : "match_parent";

            var fieldXml = (@"
    <TextView
android:text='@string/" + stringsEntryName + @"'
android:textColor='@android:color/holo_blue_dark'
android:textAppearance='?android:attr/textAppearanceMedium'
        android:minWidth='25dp'
        android:minHeight='25dp'
        android:layout_width='" + layoutWidth + @"'
        android:layout_height='wrap_content'/>"
        );
            metaDataProvider.AddStringResource(stringsEntryName, stringsEntryText);
            return fieldXml.Replace("'", "\"");
        }

        public List<string> getXamlLabelDefForRadioButton(FieldDefinition field)
        {
            var fieldOptionDefinitions = new List<string>();
            foreach (var option in field.FieldOptions.Values)
            {
                var optionName = field.ViewName + "_" + option.Clean();
                var suffixedName = optionName + suffixSpacer + ItemName.Clean();
                var fieldXml = string.Empty;
//                if (field.GridColumn == 5)
//                {
//                    fieldXml = (@"
//<RadioButton
//    android:layout_marginStart='10dp'
//    android:layout_width='100dp'
//    android:layout_height='wrap_content'
//    android:checked='false'
//android:text=''
//android:id='@+id/" + optionName + @"' />"
//);
//                }
//                else
//                {
                    fieldXml = (@"
<RadioButton
            android:layout_marginStart='30dp'
            android:layout_width='wrap_content'
            android:layout_height='wrap_content'
            android:checked='false'
            android:text='@string/" + optionName + @"'
android:id='@+id/" + suffixedName + "' />"
    );
                    metaDataProvider.AddStringResource(optionName, option);
                //}

                metaDataProvider.ModelItems.Add(
                    new FieldItem()
                    {
                        dataType = "RadioButton",
                        name = suffixedName,
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
