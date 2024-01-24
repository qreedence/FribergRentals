using FribergRentals.Data.Interfaces;

namespace FribergRentals.Utilities
{
    public class SessionUtility
    {
        private readonly IAppUser appUserRepo;

        public SessionUtility(IAppUser appUserRepo)
        {
            this.appUserRepo = appUserRepo;
        }

        public string ExtractSessionToken(HttpContext httpContext)
        {
            var sessionToken = httpContext.Request.Cookies["SessionToken"].ToString();
            return sessionToken;
        }

        public void ClearSession(HttpContext httpContext)
        {
            var sessionToken = httpContext.Request.Cookies["SessionToken"].ToString();
            if (!string.IsNullOrEmpty(sessionToken))
            {

            }
        }
    }
}
