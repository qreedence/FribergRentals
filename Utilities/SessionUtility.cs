using Azure;
using FribergRentals.Data.Interfaces;
using FribergRentals.Data.Models;
using FribergRentals.Data.Repositories;

namespace FribergRentals.Utilities
{
    public class SessionUtility
    {
        private readonly IUser userRepo;

        public SessionUtility(IUser userRepo)
        {
            this.userRepo = userRepo;
        }

        public string ExtractSessionToken(HttpContext httpContext)
        {
            var sessionToken = httpContext.Request.Cookies["SessionToken"];
            if (sessionToken != null) 
            {
                return sessionToken;
            }
            else
            {
                return null;
            }
        }

        public void ClearSession(HttpContext httpContext)
        {
            var sessionToken = httpContext.Request.Cookies["SessionToken"].ToString();
            if (!string.IsNullOrEmpty(sessionToken))
            {
                httpContext.Response.Cookies.Delete("SessionToken");
            }
        }

        public string CheckUser(HttpContext httpContext)
        {
            var sessionToken = httpContext.Request.Cookies["SessionToken"].ToString();
            
            if (string.IsNullOrEmpty(sessionToken))
            {
                return "Not logged in";
            }
            
            if (!string.IsNullOrEmpty(sessionToken))
            {
                var user = userRepo.ValidateSessionToken(sessionToken);
                if (user is AppUser appUser)
                {
                    return "user";
                }
                else if (user is Admin admin)
                {
                    return "admin";
                }

            }
           return "Not logged in";
        }
    }
}
