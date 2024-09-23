using Bogus;
using sassy.bulk.Dtos;
using System;

namespace ModelTraining.Trainner
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class InvoiceDataDtoTrainner : Faker<SaleInvoiceDto>
    {
        public InvoiceDataDtoTrainner(string customerId = "", string customerName = "")
        {
            RuleFor(x => x.InvoiceNumber, f => f.Random.Guid().ToString());
            RuleFor(x => x.OrderNo, f => f.Random.Int());
            RuleFor(x => x.RefInvoiceNumber, f => f.Random.Number().ToString());

            RuleFor(x => x.CustomerId, f => f.Random.Guid().ToString());
            RuleFor(x => x.CustomerName, f => f.Company.CompanyName());
            RuleFor(x => x.RegisterId, f => DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
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
