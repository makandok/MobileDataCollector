using Android.App;
using Android.Content.Res;
using MobileCollector;

namespace ServerCollector.projects
{
    public class PpxContextManager : BaseContextManager
    {
        public PpxContextManager(AssetManager assetManager, Activity mainContext)
            : base(Constants.FILE_PPX_FIELDS, assetManager, mainContext)
        {
            Name = "ppx";
            ProjectCtxt = ProjectContext.Ppx;
            KindDisplayNames = Constants.PPX_KIND_DISPLAYNAMES;
            FIELD_VISITDATE = Constants.FIELD_PPX_DATEOFVISIT;
        }
    }
}