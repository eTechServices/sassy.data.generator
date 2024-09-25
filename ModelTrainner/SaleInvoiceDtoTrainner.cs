using Bogus;
using System;
using sassy.bulk.Dtos;

namespace sassy.bulk.DataGenerator
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class SaleInvoiceDtoTrainner : Faker<SaleInvoiceDto>
    {
        public SaleInvoiceDtoTrainner(string customerId = "", string customerName = "")
        {
            RuleFor(x => x.RefInvoiceNumber, f => f.Random.Number(1,5000).ToString()); 

            if (!string.IsNullOrEmpty(customerId) && !string.IsNullOrEmpty(customerName))
            {
                RuleFor(x => x.CustomerId, f => customerId);
                RuleFor(x => x.CustomerName, f => customerName);
            }
            if (string.IsNullOrEmpty(customerId) && string.IsNullOrEmpty(customerName))
            {
                RuleFor(x => x.CustomerId, f => DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
                RuleFor(x => x.CustomerName, f => f.Name.FullName());
            }
            
        }
    }
}
