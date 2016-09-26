
using Android.App;
using Android.Content.Res;

namespace MobileCollector.projects
{
    public class lspContextManager: BaseContextManager        
    {
        public lspContextManager(AssetManager assetManager, Activity mainContext)
            : base(Constants.FILE_LSP_FIELDS, assetManager, mainContext)
        {
            ProjectCtxt = ProjectContext.Vmmc;
            KindDisplayNames = Constants.LSP_KIND_DISPLAYNAMES;
            FIELD_VISITDATE = Constants.FIELD_LSP_DATEOFVISIT;
        }
    }
}