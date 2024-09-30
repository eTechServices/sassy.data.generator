namespace sassy.bulk.Endpoints
{
    public static class ClientEndPoints
    {
        public static string BaseLinscellUrl { get; private set; } = "https://lsapim.azure-api.net/";
        public static string BaseSassylUrl { get; private set; } = "https://connect360-stg.azure-api.net/";
        public static string Localhost { get; private set; } = "http://localhost:7071/";
        public static string Api { get; private set; } = "api/";
        public static string CustomerService { get; private set; } = "customer-svc/";
        public static string OrderService { get; private set; } = "order-svc/";
        public static string AccountService { get; private set; } = "account-svc/";
        public static string InventoryService { get; private set; } = "inventory-svc/";
        public static string AuthService { get; private set; } = "auth-svc/";
        public static string SaveInvoice { get; private set; } = "SaveInvoiceV1";
        public static string CreateCustomer { get; private set; } = "Customer";
        public static string GetProduct { get; private set; } = "Item?pageNo={{pageNo}}&pageSize={{pageSize}}";
        public static string GetCustomers { get; private set; } = "Customer?pageNo=0&pageSize={{pageSize}}&type=all&filterValue={{CName}}";
        public static string SearchCustomer { get; private set; } = "SearchCustomer/";
        public static string SaleInvoice { get; private set; } = "Invoice/SaveInvoiceV1";
        public static string GraphClientApi { get; private set; } = "B2CGraphClientApi";
        public static string SignIn { get; private set; } = "SignIn";
        public static string InvoiceFilter { get; private set; } = "Invoice/FilterInvoice";
        public static string Configurations { get; private set; } = "AllFilters";
    }
}
