using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using MobileCollector.model;
using Android.Content.Res;

namespace MobileCollector.projects
{
    public class BaseContextManager
    {
        AssetManager _assetManager;
        Activity _mainContext;
        string _fieldJsonFile;
        public BaseContextManager(
            string fieldJsonFile,
            AssetManager assetManager, Activity mainContext) {
            _fieldJsonFile = fieldJsonFile;
            _assetManager = assetManager;
            _mainContext = mainContext;
            initialiseFieldDictionary();
        }

        void initialiseFieldDictionary()
        {
            FieldItems =
                readFields(_fieldJsonFile, _assetManager, _mainContext);
        }

        public Dictionary<string, string> KindDisplayNames
        {
            get; protected set;
        }

        public ProjectContext ProjectCtxt { get; set; }
        public List<FieldItem> FieldItems
        {
            get; private set;
        }

        public Dictionary<string, int> ViewIdDictionary { get; private set; }

        public string FIELD_VISITDATE { get; protected set; }

        List<FieldItem> readFields(string fieldsAssetName, AssetManager assetManager, Activity context)
        {
            //we load the fields
            var fieldStream = assetManager.Open(fieldsAssetName);

            var asString = fieldStream.toText();

            var fields = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FieldItem>>(asString);

            var viewPages = fields.Select(t => t.pageName).Distinct().ToList();

            var dictionary = new Dictionary<string, int>();
            foreach (var page in viewPages)
            {
                var id = context.Resources.GetIdentifier(page, "layout", context.PackageName);
                if (id == 0) throw new ArgumentOutOfRangeException("Could not determine Id for layout " + page);
                dictionary[page] = id;
            }

            ViewIdDictionary = dictionary;

            foreach (var field in fields)
            {
                field.PageId = dictionary[field.pageName];
            }

            return fields;
        }
    }
}