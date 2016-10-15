using Android.App;
using Android.Content.Res;
using MobileCollector;

namespace ServerCollector.projects
{
    public class GeneralContextManager: BaseContextManager        
    {
        public GeneralContextManager(AssetManager assetManager, Activity mainContext)
            : base(Constants.FILE_VMMC_FIELDS, assetManager, mainContext)
        {
            Name = "gen";
            ProjectCtxt = ProjectContext.None;
            KindDisplayNames = Constants.VMMC_KIND_DISPLAYNAMES;
            FIELD_VISITDATE = Constants.FIELD_VMMC_DATEOFVISIT;
        }
    }
}