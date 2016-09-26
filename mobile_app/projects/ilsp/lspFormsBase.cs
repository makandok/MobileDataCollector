using System;
using System.Collections.Generic;
using MobileCollector.model;
using System.Linq;

namespace MobileCollector.projects.ilsp
{
    public class lspFormsBase : DataFormsBase<lspClientSummary>
    {
        protected override Type getHomeActivityType()
        {
            return typeof(lspHomeActivity);
        }

        protected override List<NameValuePair> getModuleClientSummaries(IEnumerable<NameValuePair> data)
        {
            return new List<NameValuePair>();
        }

        protected override List<NameValuePair> getIndexedFormData(List<NameValuePair> data)
        {
            var indexFieldNames = Constants.LSP_IndexedFieldNames;
            return (data.Where(
                t => indexFieldNames.Contains(t.Name))).ToList();
        }

        protected override List<FieldItem> GetFieldsForView(int viewId)
        {
            return AppInstance.Instance.ModuleContext.ContextManager.FieldItems.Where(t => t.PageId == viewId).ToList();
        }
    }
}