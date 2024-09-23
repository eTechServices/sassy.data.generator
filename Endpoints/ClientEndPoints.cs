namespace sassy.bulk.Endpoints
{
    public static class ClientEndPoints
    {
        public static string BaseUrl { get; private set; } = "https://lsapim.azure-api.net/";
        public static string SaveInvoice { get; private set; } = "order-svc/api/SaveInvoiceV1";
        public static string CreateCustomer { get; private set; } = "customer-svc/api/Customer";
    }
}
