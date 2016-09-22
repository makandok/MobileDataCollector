namespace MobileCollector.model
{
    public class UserSession
    {
        public KindKey Id { get; set; }

        public string AuthorisationToken { get; set; }
        public AppUser User { get; set; }
    }
}