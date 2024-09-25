namespace sassy.bulk.Cache
{
    public static class CacheKey
    {
        public static string UserName { get; private set; } = "userName";
        public static string BearerToken { get; private set; } = "bearerToken";
        public static string Password { get; private set; } = "password";
        public static string UserId { get; private set; } = "userId";
        public static string BusinessName { get; private set; } = "businessName";
    }
}
