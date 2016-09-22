using Android.App;
using Android.OS;

namespace MobileCollector.modules
{
    [Activity(Label = "SmsActivity")]
    public class SmsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.smscenter); 
        }
    }
}