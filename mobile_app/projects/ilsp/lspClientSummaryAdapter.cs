using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using MobileCollector.model;
using System.Globalization;

namespace MobileCollector.projects.ilsp
{
    public class lspClientSummaryAdapter : BaseAdapter<lspClientSummary>
    {
        List<lspClientSummary> _myList;
        Activity _context;
        public int tMinus { get; set; }
        public lspClientSummaryAdapter(Activity context, ListView listview, List<lspClientSummary> clientList)
        {
            tMinus = -1;
            _context = context;
            _myList = clientList;
        }

        public override lspClientSummary this[int position]
        {
            get
            {
                return _myList[position];
            }
        }

        public override int Count
        {
            get
            {
                return _myList.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return _myList[position].getItemId();
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var client = _myList[position];
            var myView = convertView ??
                 _context.LayoutInflater.Inflate(Resource.Layout.clientsummary, parent, false);
            myView.FindViewById<TextView>(Resource.Id.clientSummaryTDate)
                .Text = client.NameofGroup;
            myView.FindViewById<TextView>(Resource.Id.clientSummaryNames)
                .Text = Convert.ToString(client.BeneficiaryName);
            myView.FindViewById<TextView>(Resource.Id.clientSummaryCardSerial)
                .Text = "Village: " + Convert.ToString(client.NameOfVillage);
            myView.FindViewById<TextView>(Resource.Id.clientSummaryMCNumber)
                .Text = "Inteview Date: " + Convert.ToString(client.InterviewDate);
            myView.FindViewById<TextView>(Resource.Id.clientSummaryPlacementDate)
                .Text = "Sex: " + client.BeneficiarySex;
            myView.FindViewById<TextView>(Resource.Id.clientSummaryTelephone)
                .Text = Convert.ToString(client.BeneficiaryAge);
            return myView;
        }
    }
}