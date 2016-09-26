namespace MobileCollector.projects.ilsp
{
    public class lspProvider : ClientLookupProvider<lspClientSummary>
    {
        public lspProvider():base(Constants.KIND_DERIVED_LSP_CLIENTSUMMARY)
        {
        }
    }
}