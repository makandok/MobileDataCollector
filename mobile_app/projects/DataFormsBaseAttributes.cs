using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileCollector.model;
using Android.Locations;

namespace MobileCollector.projects
{
    public class DataFormsBaseAttributes : Activity, ILocationListener
    {
        protected bool _isRegistration = false;
        protected int myView = -1;
        protected IPP_NavController myNavController = null;
        protected bool IsFirstPage = false;
        public KindName _kindName { get; set; }
        //public bool AllowShowView = true;
        public DataFormsBaseAttributes InitialiseAttributes()
        {
            doPreCreate(null);
            return this;
        }

        protected virtual void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = null; // new KindName(Constants.KIND_PPX_UNSCHEDULEDVISIT);
            myView = -1; // Resource.Layout.prepexunscheduled2;
            myNavController = null; // new PP_UnscheduledVisitControl() { };
        }

        #region GPS Stuff
        public void OnStatusChanged(string provider, Availability status, Android.OS.Bundle extras)
        {
            Toast.MakeText(this, "Status has changed", ToastLength.Long).Show();
        }

        LocationManager _locationManager;
        Location _currentLocation;
        public void InitializeLocationManager()
        {
            var provider = string.Empty;
            _locationManager = (LocationManager)this.GetSystemService(LocationService);
            var locationProviders = _locationManager.GetProviders(new Criteria
            {
                Accuracy = Accuracy.Fine
            }, true);
            provider = locationProviders.FirstOrDefault();
            if (provider == null)
                provider = string.Empty;

            _locationManager.RequestLocationUpdates(provider, 0, 0, this);
            Toast.MakeText(this, "Using " + provider, ToastLength.Long).Show();
        }
        public void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            if (_currentLocation == null)
            {
                Toast.MakeText(this,
                    "Unable to determine your location. Try again in a short while.",
                    ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this,
                    string.Format("{0:f6},{1:f6}", _currentLocation.Latitude, _currentLocation.Longitude)
                    , ToastLength.Long).Show();
                //consider logic to disable this once we have a known location
            }
        }

        public void OnProviderDisabled(string provider)
        {
            //_locationManager.
            Toast.MakeText(this, "Please ensure Location is turned on", ToastLength.Long).Show();
        }

        public void OnProviderEnabled(string provider)
        {
            Toast.MakeText(this, "Location is turned on", ToastLength.Long).Show();
        }
        #endregion


    }
}