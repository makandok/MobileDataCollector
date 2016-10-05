using Android.App;
using Android.OS;
using MobileCollector.model;
using MobileCollector.projects.ilsp.workflow;

namespace MobileCollector.projects.ilsp
{
    [Activity(Label = "ILASP - Start Page 1 of 15")]
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

    [Activity(Label = "ILASP - Page 2 of 15")]
    public class IlaspMain2 : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main2;
            myNavController = new lspMainControl();
        }
    }

    [Activity(Label = "ILASP - Page 3 of 15")]
    public class IlaspMain3 : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main3;
            myNavController = new lspMainControl();
        }
    }
    [Activity(Label = "ILASP - Page 4 of 15")]
    public class IlaspMain4 : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main4;
            myNavController = new lspMainControl();
        }
    }

    [Activity(Label = "ILASP - Page 5 of 15")]
    public class IlaspMain5 : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main5;
            myNavController = new lspMainControl();
        }
    }
    [Activity(Label = "ILASP - Page 6 of 15")]
    public class IlaspMain6 : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main6;
            myNavController = new lspMainControl();
        }
    }

    [Activity(Label = "ILASP - Page 7 of 15")]
    public class IlaspMain7 : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main7;
            myNavController = new lspMainControl();
        }
    }
    [Activity(Label = "ILASP - Page 8 of 15")]
    public class IlaspMain8 : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main8;
            myNavController = new lspMainControl();
        }
    }

    [Activity(Label = "ILASP - Page 9 of 15")]
    public class IlaspMain9 : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main9;
            myNavController = new lspMainControl();
        }
    }
    [Activity(Label = "ILASP - Page 10 of 15")]
    public class IlaspMain10 : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main10;
            myNavController = new lspMainControl();
        }
    }

    [Activity(Label = "ILASP - Page 11 of 15")]
    public class IlaspMain11 : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main11;
            myNavController = new lspMainControl();
        }
    }
    [Activity(Label = "ILASP - Page 12 of 15")]
    public class IlaspMain12 : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main12;
            myNavController = new lspMainControl();
        }
    }
    [Activity(Label = "ILASP - Page 13 of 15")]
    public class IlaspMain13 : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main13;
            myNavController = new lspMainControl();
        }
    }
    [Activity(Label = "ILASP - Page 14 of 15")]
    public class IlaspMain14 : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main14;
            myNavController = new lspMainControl();
        }
    }
    [Activity(Label = "ILASP - Page 15 of 15")]
    public class IlaspMain15 : lspFormsBase
    {
        protected override void doPreCreate(Bundle savedInstanceState)
        {
            _kindName = new KindName(Constants.KIND_LSP_MAIN);
            myView = Resource.Layout.ilsp_main15;
            myNavController = new lspMainControl();
        }
    }

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