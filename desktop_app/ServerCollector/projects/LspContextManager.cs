
using Android.App;
using Android.Content.Res;
using MobileCollector;

namespace ServerCollector.projects
{
    public class LspContextManager : BaseContextManager        
    {
        public LspContextManager(AssetManager assetManager, Activity mainContext)
            : base(Constants.FILE_LSP_FIELDS, assetManager, mainContext)
        {
            Name = "lsp";
            ProjectCtxt = ProjectContext.None;
            KindDisplayNames = Constants.LSP_KIND_DISPLAYNAMES;
            FIELD_VISITDATE = Constants.FIELD_LSP_DATEOFVISIT;
        }
    }
}