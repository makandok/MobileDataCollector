using Android.App;
using System;

namespace MobileCollector
{
    public class WaitDialogHelper
    {
        Activity _context;
        Action<string, Android.Widget.ToastLength> _makeToast;
        public WaitDialogHelper(Activity context, Action<string, Android.Widget.ToastLength> makeToast)
        {
            _context = context;
            _makeToast = makeToast;
        }

        public async System.Threading.Tasks.Task<int> showDialog(
            Func<Action<string, Android.Widget.ToastLength>, System.Threading.Tasks.Task<int>> worker)
        {
            var progressDialog = ProgressDialog.Show(_context, "Please wait...", "Synchronising with server", true);
            await worker(_makeToast);
            progressDialog.Hide();

            return 0;
        }
    }
}