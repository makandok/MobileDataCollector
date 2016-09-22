using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MobileCollector.model;
using Android.Content;
using MobileCollector.modules;

namespace MobileCollector.projects.ppx.activity
{
    [Activity(Label = "Client Selector")]
    public class PpxClientSelectionActivity : ClientSelectorBase<PPClientSummary>
    {
        PpxClientSummaryAdapter _defaultAdapter = null;

        protected override void doOnClick(PPClientSummary client)
        {
            Android.Widget.Toast.MakeText(this, client.Names, Android.Widget.ToastLength.Short).Show();
        }

        protected override List<PPClientSummary> getClientSummaries()
        {
            return new PpxLookupProvider().Get();
        }

        protected override void setClientSummaryAdapter(ListView view, List<PPClientSummary> clientSumaries)
        {
            _defaultAdapter = new PpxClientSummaryAdapter(this, view, clientSumaries);
            view.Adapter = _defaultAdapter;
        }
    }
}