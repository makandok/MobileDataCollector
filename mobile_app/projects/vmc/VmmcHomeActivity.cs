using System.Collections.Generic;
using Android.App;
using Android.Widget;
using MobileCollector.projects.vmc.activity;
using MobileCollector.projects.vmc.workflow;

namespace MobileCollector.projects.vmc
{
    [Activity(Label = "@string/vmc_activitylabel", Icon = "@drawable/DC")]
    public class VmmcHomeActivity : BaseHomeActivity<VmmcClientSummary>
    {
        protected override void showDefaultHome()
        {
            showPPXHome();
        }

        protected override List<BaseWorkflowController> getActivityWFControllers()
        {
            return new List<BaseWorkflowController>()
                                {
                                    new VmmcRegAndProcControl(),
                                    new VmmcPostOperationControl(),
                            };
        }

        void showPPXHome()
        {
            SetContentView(Resource.Layout.PrepexHome);
            var closeButton = FindViewById<Button>(Resource.Id.buttonClose);
            closeButton.Click += (sender, e) => {
                //close activity
                StartActivity(typeof(LauncherActivity));
            };

            var buttonClientEvaluation = FindViewById<Button>(Resource.Id.buttonClientEvaluation);
            buttonClientEvaluation.Click += (sender, e) =>
            {
                StartActivity(typeof(VmmcRegAndProc1));
            };
            buttonClientEvaluation.Text = "Registration and Procedure";


            var buttonUnscheduled = FindViewById<Button>(Resource.Id.buttonUnscheduled);
            buttonUnscheduled.Visibility = Android.Views.ViewStates.Gone;

            var buttonDeviceRemovalVisit = FindViewById<Button>(Resource.Id.buttonDeviceRemovalVisit);
            buttonDeviceRemovalVisit.Click += (sender, e) =>
            {
                StartActivity(typeof(VmmcClientSelectionActivity), typeof(VmmcPostOp1));
            };
            buttonDeviceRemovalVisit.Text = "Post Operation";

            var buttonPostRemovalVisit = FindViewById<Button>(Resource.Id.buttonPostRemovalVisit);
            buttonPostRemovalVisit.Visibility = Android.Views.ViewStates.Gone;

            //buttonViewList
            var buttonViewList = FindViewById<Button>(Resource.Id.buttonViewList);
            buttonViewList.Click += (sender, e) => {
                StartActivity(typeof(VmmcFilteredGridDisplayActivity));
            };

            //buttonEditRecords
            var buttonEditRecords = FindViewById<Button>(Resource.Id.buttonEditRecords);
            buttonEditRecords.Click += (sender, e) => {
                StartActivity(typeof(VmmcClientSelectionActivity), typeof(VmmcRecordSelectorActivity));
            };

            var buttonSupplies = FindViewById<Button>(Resource.Id.buttonSupplies);
            buttonSupplies.Visibility = Android.Views.ViewStates.Gone;

            var buttonViewRecordSummaries = FindViewById<Button>(Resource.Id.buttonViewRecordSummaries);
            buttonViewRecordSummaries.Click += async (sender, e) =>
            {
                await getClientSummaryReport(new VmmcLookupProvider(), Constants.VMMC_KIND_DISPLAYNAMES);
            };
        }
    }
}