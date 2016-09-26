using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Widget;
using MobileCollector.model;
using MobileCollector.modules;

namespace MobileCollector.projects.ilsp.activity
{
    [Activity(Label = "Select Record")]
    public class lspRecordSelectorActivity : RecordSelectorBase, ListView.IOnItemClickListener
    {
        protected override List<VisitSummary> getRecordsForClient(string entityId)
        {
            //we get entityid from the intent
            var records = getRecordSummaries(entityId);
            return (from table in records select new VisitSummary(table, Constants.LSP_KIND_DISPLAYNAMES[table.KindName])).ToList();
        }

        protected override string getCurrentClientId(string clientString)
        {
            var client = Newtonsoft.Json.JsonConvert.DeserializeObject<lspClientSummary>(clientString);
            return client.EntityId.Value;
        }
    }
}