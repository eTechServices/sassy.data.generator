using InvoiceBulkRegisteration.Dtos;
using sassy.bulk.Dtos;
using System.Collections.Generic;

namespace sassy.bulk.DataGenerator
{
    internal class Data
    {
        public static IEnumerable<SampleCustomerDto> GenerateSampleData(int count)
        {
            var data = new SampleCustomerDtoFaker();
            return data.Generate(count);
        }
        public static SaleInvoiceDto GenerateSampleInvoiceData(string customerId, string customerName)
        {
            var sampleData = new SaleInvoiceDtoFaker(customerId,customerName);
            return sampleData.Generate();
        }
    }
}
