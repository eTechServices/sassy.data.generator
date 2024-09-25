namespace sassy.bulk.Endpoints
{
    public static class ClientEndPoints
    {
        public static string BaseLinscellUrl { get; private set; } = "https://lsapim.azure-api.net/";
        public static string BaseSassylUrl { get; private set; } = "https://connect360-stg.azure-api.net/";
        public static string CustomerService { get; private set; } = "customer-svc/api";
        public static string OrderService { get; private set; } = "order-svc/api";
        public static string AuthService { get; private set; } = "auth-svc/api";
        public static string SaveInvoice { get; private set; } = "/SaveInvoiceV1";
        public static string CreateCustomer { get; private set; } = "/Customer";
        public static string SearchCustomer { get; private set; } = "/SearchCustomer/";
        public static string SaleInvoice { get; private set; } = "/Invoice/SaveInvoiceV1";
        public static string SignIn { get; private set; } = "/SignIn";
    }
}
