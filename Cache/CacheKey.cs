namespace sassy.bulk.Cache
{
    public static class CacheKey
    {
        public static string UserName { get; private set; } = "userName";
        public static string BearerToken { get; private set; } = "bearerToken";
        public static string Password { get; private set; } = "password";
        public static string UserId { get; private set; } = "userId";
        public static string BusinessName { get; private set; } = "businessName";
        public static string DisplayName { get; private set; } = "displayName";
        public static string FirstName { get; private set; } = "firstName";
        public static string Country { get; private set; } = "country";
        public static string State { get; private set; } = "state";
        public static string PhoneNumber { get; private set; } = "phoneNumber";
        public static string CompanyName { get; private set; } = "companyName";
        public static string Type { get; private set; } = "type";
        public static string LocationList { get; private set; } = "locationList";
        public static string RegisterList { get; private set; } = "registerList";
        public static string CustomerList { get; private set; } = "customerList";
    }
}
