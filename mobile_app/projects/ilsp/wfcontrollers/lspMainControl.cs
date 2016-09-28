using System;
using System.Collections.Generic;

namespace MobileCollector.projects.ilsp.workflow
{
    public class lspMainControl : BaseWorkflowController
    {
        public lspMainControl()
        {
            MyActivities = new Dictionary<int, Type>() {
            {Resource.Layout.ilsp_main1,typeof(IlaspMainStart) },
             {Resource.Layout.ilsp_main2,typeof(IlaspMain2) },
              {Resource.Layout.ilsp_main3,typeof(IlaspMain3) },
              {Resource.Layout.ilsp_main4,typeof(IlaspMain4) },
              {Resource.Layout.ilsp_main5,typeof(IlaspMain5) },
              {Resource.Layout.ilsp_main6,typeof(IlaspMain6) },
              {Resource.Layout.ilsp_main7,typeof(IlaspMain7) },
              {Resource.Layout.ilsp_main8,typeof(IlaspMain8) },
              {Resource.Layout.ilsp_main9,typeof(IlaspMain9) },
              {Resource.Layout.ilsp_main10,typeof(IlaspMain10) },
              {Resource.Layout.ilsp_main11,typeof(IlaspMain11) },
              {Resource.Layout.ilsp_main12,typeof(IlaspMain12) },
              {Resource.Layout.ilsp_main13,typeof(IlaspMain13) },
              {Resource.Layout.ilsp_main14,typeof(IlaspMain14) },
              {Resource.Layout.ilsp_main15,typeof(IlaspMain15) },
            { Resource.Layout.DataEntryEnd,typeof(IlaspMainEnd) }
            };

            MyLayouts = new[] {
                Resource.Layout.ilsp_main1,
                Resource.Layout.ilsp_main2,
                Resource.Layout.ilsp_main3,
                Resource.Layout.ilsp_main4,
                Resource.Layout.ilsp_main5,
                Resource.Layout.ilsp_main6,
                Resource.Layout.ilsp_main7,
                Resource.Layout.ilsp_main8,
                Resource.Layout.ilsp_main9,
                Resource.Layout.ilsp_main10,
                Resource.Layout.ilsp_main11,
                                Resource.Layout.ilsp_main12,
                                                Resource.Layout.ilsp_main13,
                                                                Resource.Layout.ilsp_main14,
                                                                                Resource.Layout.ilsp_main15,
                Resource.Layout.DataEntryEnd
            };
        }
    }
}