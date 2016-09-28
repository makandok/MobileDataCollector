using System;
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

        public void reset()
        {
            resources.Clear();
            StringResourcesItems.Clear();
            ModelItems.Clear();
        }
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
}
