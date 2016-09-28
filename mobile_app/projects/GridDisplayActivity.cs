//using System;
//using System.Collections.Generic;
//using Android.App;
//using Android.Views;
//using Android.Widget;
//using MobileCollector.model;
//using System.Globalization;
//using Android.OS;
//using System.Data;

//namespace MobileCollector.projects
//{
//    [Activity(Label = "GridDisplayActivity")]
//    public class GridDisplayActivity : Activity
//    {
//        xGridDisplayAdapter _adpter = null;
//        protected override void OnCreate(Bundle savedInstanceState)
//        {
//            base.OnCreate(savedInstanceState);

//            // Create your application here
//            SetContentView(Resource.Layout.griddisplay);

//            var ds = getData();

//            var listview = FindViewById<ListView>(Resource.Id.listView1);
//            _adpter = new xGridDisplayAdapter(this, ds);
            
//            listview.Adapter = _adpter;
//        }

//        DataTable getData()
//        {
//            var _myTable = new DataTable();
//            _myTable.Columns.Add("Column1");
//            _myTable.Columns.Add("Column2");
//            _myTable.Columns.Add("Column3");
//            _myTable.Columns.Add("Column4");
//            _myTable.Columns.Add("Column5");
//            _myTable.AcceptChanges();
//            //var row1 = _myTable.NewRow();
//            _myTable.Rows.Add("Cain", "Killed", 1964, DateTime.Now, 60000);
//            _myTable.Rows.Add("Abel", "Offered Better", 56, DateTime.Now.AddMonths(-4), 17000);
//            _myTable.Rows.Add("Expelled", "used Craigs List", 56, DateTime.Now.AddMonths(-7), 23000);
//            return _myTable;
//        }
//    }

//    public class xGridDisplayAdapter : BaseAdapter<DataRow>
//    {
//        DataTable _myTable;
//        Activity _context;
//        public xGridDisplayAdapter(Activity context, DataTable data)
//        {
//            _myTable = data;
//            _context = context;
//        }

//        public override DataRow this[int position]
//        {
//            get
//            {
//                return _myTable.Rows[position];
//            }
//        }

//        public override int Count
//        {
//            get
//            {
//                return _myTable.Rows.Count;
//            }
//        }

//        public override long GetItemId(int position)
//        {
//            return this[position].GetHashCode();
//        }

//        public override View GetView(int position, View convertView, ViewGroup parent)
//        {
//            var rec = _myTable.Rows[position];
//            var view = convertView ??
//                _context.LayoutInflater.Inflate(Resource.Layout.gridview_item,
//                parent,false
//                );

//            //we fill the cells with what we know so far
//            var txt1 = view.FindViewById<TextView>(Resource.Id.editText1);        
//            var txt2 = view.FindViewById<EditText>(Resource.Id.editText2);
//            var txt3 = view.FindViewById<EditText>(Resource.Id.editText3);
//            var txt4 = view.FindViewById<EditText>(Resource.Id.editText4);

//            txt1.Text = Convert.ToString(rec[0]);
//            txt2.Text = Convert.ToString(rec[1]);
//            txt3.Text = Convert.ToString(rec[2]);
//            txt4.Text = Convert.ToString(rec[3]);

//            return view;
//        }
//    }

//}