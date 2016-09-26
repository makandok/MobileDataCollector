using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MobileCollector.model;
using MobileCollector.store;
using MobileCollector.Utilities;
using System.Threading.Tasks;

namespace MobileCollector.projects.ilsp
{
    [Activity(Label = "Beneficiary List")]
    public class lspFilteredGridDisplayActivity : Activity, ListView.IOnItemClickListener
    {
        lspClientSummaryAdapter _defaultAdapter = null;
        List<lspClientSummary> _allPrepexClients;
        lspClientSummary _selectedClient = null;
        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            _selectedClient = _allPrepexClients[position];
            Android.Widget.Toast.MakeText(this,
                _selectedClient.BeneficiaryName + " " + _selectedClient.BeneficiarySex, Android.Widget.ToastLength.Short).Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.clientlist);

            var listview = FindViewById<ListView>(Resource.Id.listviewClientList);
            listview.FastScrollEnabled = true;
            listview.FastScrollAlwaysVisible = true;

            listview.OnItemClickListener = this;

            _allPrepexClients = new lspProvider().Get();
            _defaultAdapter = new lspClientSummaryAdapter(this, listview, _allPrepexClients);

            listview.Adapter = _defaultAdapter;

            var rgroup = FindViewById<RadioGroup>(Resource.Id.rgroupCSOptions);
            rgroup.Visibility = ViewStates.Gone;

            //buttonPerformAction
            var buttonPerformAction = FindViewById<Button>(Resource.Id.buttonPerformAction);
            buttonPerformAction.Visibility = ViewStates.Gone;
        }

        void OnListItemClick(ListView l, View v, int position, long id)
        {
            var t = _allPrepexClients[position];
            Android.Widget.Toast.MakeText(this, t.BeneficiaryName + " " + t.BeneficiarySex, Android.Widget.ToastLength.Short).Show();
        }        
    }
}