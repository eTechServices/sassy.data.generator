using InvoiceBulkRegisteration.Dtos;
using ModelTraining.Trainner;
using sassy.bulk.Dtos;
using System.Collections.Generic;
namespace sassy.bulk.DataGenerator
{
    internal class Trainner
    {
        public static IEnumerable<SampleCustomerDto> GetCustomerData(int count)
        {
            var data = new CustomerDtoTrainner();
            return data.Generate(count);
        }
        public static SaleInvoiceDto GenerateSampleInvoiceData(string customerId, string customerName)
        {
            var sampleData = new SaleInvoiceDtoTrainner(customerId,customerName);
            return sampleData.Generate();
        }
    }
}
