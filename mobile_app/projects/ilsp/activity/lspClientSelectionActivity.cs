using System.Collections.Generic;
using Android.App;
using Android.Widget;
using MobileCollector.modules;

namespace MobileCollector.projects.ilsp.activity
{
    [Activity(Label = "Client Selector")]
    public class lspClientSelectionActivity : ClientSelectorBase<lspClientSummary>
    {
        lspClientSummaryAdapter _defaultAdapter = null;

        protected override void doOnClick(lspClientSummary client)
        {
            Android.Widget.Toast.MakeText(this,
                client.BeneficiaryName + " " + client.BeneficiaryAge + " " + client.BeneficiarySex, Android.Widget.ToastLength.Short).Show();
        }

        protected override List<lspClientSummary> getClientSummaries()
        {
            return new lspProvider().Get();
        }

        protected override void setClientSummaryAdapter(ListView view, List<lspClientSummary> clientSumaries)
        {
            _defaultAdapter = new lspClientSummaryAdapter(this, view, clientSumaries);
            view.Adapter = _defaultAdapter;
        }
    }
}