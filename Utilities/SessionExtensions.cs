using Newtonsoft.Json;

namespace FribergRentals.Utilities
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);

            session.SetString(key, serializedValue);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var serializedValue = session.GetString(key);

            return serializedValue != null ? JsonConvert.DeserializeObject<T>(serializedValue) : default(T);
        }

        public static void CheckSession(HttpContext httpContext)
        {
            if (string.IsNullOrEmpty(httpContext.Request.Cookies["SessionToken"]))
            {
                httpContext.Response.Redirect("/Login/SessionExpired");
            }
        }
    }
}
