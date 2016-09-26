using System;
using System.Collections.Generic;

namespace MobileCollector.projects.ilsp.workflow
{
    public class lspMainControl : BaseWorkflowController
    {
        public lspMainControl()
        {
            MyActivities = new Dictionary<int, Type>() {
            { Resource.Layout.ilsp_main1,typeof(IlaspMainStart) },
            //{ Resource.Layout.vmmc_regandproc2,typeof(VmmcRegAndProc2) },
            //{ Resource.Layout.vmmc_regandproc3,typeof(VmmcRegAndProc3) },
            //{ Resource.Layout.vmmc_regandproc4,typeof(VmmcRegAndProc4) },
            //{ Resource.Layout.vmmc_regandproc5,typeof(VmmcRegAndProc5) },
            { Resource.Layout.DataEntryEnd,typeof(IlaspMainEnd) }
            };

            MyLayouts = new[] {
                Resource.Layout.ilsp_main1,
                //Resource.Layout.vmmc_regandproc2,
                //Resource.Layout.vmmc_regandproc3,
                //Resource.Layout.vmmc_regandproc4,
                //Resource.Layout.vmmc_regandproc5,
                Resource.Layout.DataEntryEnd
            };
        }
    }
}