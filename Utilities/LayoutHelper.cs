using FribergRentals.Data.Interfaces;
using FribergRentals.Data.Models;

namespace FribergRentals.Utilities
{
    public static class LayoutHelper
    {
        public static string GetLayout(HttpContext context)
        {
            Console.WriteLine("GetLayout method reached");
            var serviceProvider = context.RequestServices;
            var sessionToken = context.Request.Cookies[("SessionToken")];
            var userRepo = serviceProvider.GetService<IUser>();

            if (!string.IsNullOrEmpty(sessionToken))
            {
                var user = userRepo.ValidateSessionToken(sessionToken);
                if (user is AppUser appUser)
                {
                    Console.WriteLine($"AppUser is: {user.Email}");
                    return "_AppUserLayout";
                }
                if (user is Admin admin)
                {
                    Console.WriteLine($"Admin is: {user.Email}");
                    return "_AdminLayout";
                }
            }
            return "_Layout";
        }
    }
}
