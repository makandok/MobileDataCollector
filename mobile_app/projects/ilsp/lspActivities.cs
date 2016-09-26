using Android.App;
using Android.OS;
using MobileCollector.model;
using MobileCollector.projects.ilsp.workflow;

namespace MobileCollector.projects.ilsp
{
    [Activity(Label = "ILASP - Start")]
    public class IlaspMainStart : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            IsFirstPage = true;
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main1;
            myNavController = new lspMainControl();
        }
    }

    //[Activity(Label = "Registration and Procedure - 2")]
    //public class VmmcRegAndProc2 : VmmcFormsBase
    //{
    //    protected override void doPreCreate(Bundle savedInstanceState)
    //    {
    //        _kindName = new KindName(Constants.KIND_VMMC_REGANDPROCEDURE);
    //        myView = Resource.Layout.vmmc_regandproc2;
    //        myNavController = new VmmcRegAndProcControl();
    //    }
    //}

    //[Activity(Label = "Registration and Procedure - 3")]
    //public class VmmcRegAndProc3 : VmmcFormsBase
    //{
    //    protected override void doPreCreate(Bundle savedInstanceState)
    //    {
    //        _kindName = new KindName(Constants.KIND_VMMC_REGANDPROCEDURE);
    //        myView = Resource.Layout.vmmc_regandproc3;
    //        myNavController = new VmmcRegAndProcControl();
    //    }
    //}

    //[Activity(Label = "Registration and Procedure - 4")]
    //public class VmmcRegAndProc4 : VmmcFormsBase
    //{
    //    protected override void doPreCreate(Bundle savedInstanceState)
    //    {
    //        _kindName = new KindName(Constants.KIND_VMMC_REGANDPROCEDURE);
    //        myView = Resource.Layout.vmmc_regandproc4;
    //        myNavController = new VmmcRegAndProcControl();
    //    }
    //}

    //[Activity(Label = "Registration and Procedure - 5")]
    //public class VmmcRegAndProc5 : VmmcFormsBase
    //{
    //    protected override void doPreCreate(Bundle savedInstanceState)
    //    {
    //        _kindName = new KindName(Constants.KIND_VMMC_REGANDPROCEDURE);
    //        myView = Resource.Layout.vmmc_regandproc5;
    //        myNavController = new VmmcRegAndProcControl();
    //    }
    //}

    [Activity(Label = "Survey - End")]
    public class IlaspMainEnd : lspFormsBase
    {
        protected override bool IsRegistrationEndPage()
        {
            return true;
        }

        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.DataEntryEnd;
            myNavController = new lspMainControl();
        }
    }
}