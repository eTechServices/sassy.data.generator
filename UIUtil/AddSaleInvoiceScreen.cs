using Newtonsoft.Json;
using sassy.bulk.Cache;
using sassy.bulk.Dtos;
using sassy.bulk.Endpoints;
using sassy.bulk.Enums;
using sassy.bulk.ResponseDto;
using sassy.bulk.UIUtil.Abstract;
using sassy.bulk.Webhooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sassy.bulk.UIUtil
{
    public class AddSaleInvoiceScreen : Base
    {
        public override void StartScreen()
        {
            label:
            var invoiceData = new SaleInvoiceDto();

            var displayUser = DisplayUserData();
            int indexer = InputInt($"{displayUser} how many records you want to add?: ");
            int itemsToInclude = GetNumberOfItems();
            decimal tipAmount = GetDecimalInput("Tip amount");
            decimal tenderedAmount = GetDecimalInput("Tendered amount");
            decimal cashAmount = GetDecimalInput("Cash amount");
            decimal creditCardAmount = GetDecimalInput("Credit card amount");
            decimal rewardDiscount = GetDecimalInput("Reward disount amount");
            decimal otherChargesAmount = GetDecimalInput("Other charges amount");
            decimal cashdicount = GetDecimalInput("Cash discount amount");
            string customerNote = TakeInput("PLease Enter Customer Note: ");

            invoiceData.TipAmount = tipAmount;
            invoiceData.TenderedAmount = tenderedAmount;
            invoiceData.CashAmount = cashAmount;
            invoiceData.CreditCardAmount = creditCardAmount;
            invoiceData.RewardDiscount = rewardDiscount;
            invoiceData.TotalSaleItems = itemsToInclude;
            invoiceData.OtherChargesAmount = otherChargesAmount;
            invoiceData.UserId = GetData(CacheKey.UserId).ToString();
            invoiceData.UserName = displayUser;
            invoiceData.InvoiceItems = GetRandomInvoiceItems(indexer, cashdicount, invoiceData.InvoiceNumber).GetAwaiter().GetResult();
            invoiceData.InvoiceDiscounts = AddDiscounts(invoiceData);
            invoiceData.InvoiceProductNotes = AddNotes(invoiceData);
            invoiceData.ElectronicTenders = AddElectronicTender(invoiceData);
            invoiceData.InvoiceTenders = AddTenderList(invoiceData);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("This may take time");
            Console.WriteLine("Your Task is started take a while we complete......");

            for (int i = 0; i < indexer; i++)
            {
                Console.Write("\rGenerating invoices... ({0}/{1})", i + 1, indexer);
                Thread.Sleep(1000);
                var isSaved = StartInvoiceTaskAsync(invoiceData).GetAwaiter().GetResult();
                if (!isSaved)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invoice No {0} failed to saved.", i);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\rYes for start process again.\nNo for continue execution.");
                    Console.ForegroundColor = ConsoleColor.White;
                    string strtAgain = Input("\rDo you want to start again?:");
                    if (strtAgain != "no" || strtAgain != "n")
                    {
                        goto label;
                    }
                }
                ProgressBar(i, indexer);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        private int GetNumberOfItems()
        {
            int numItems = 0;
            string number = "";
            do
            {
                number = TakeInput("Please enter number of items to include?: ");
            } while (!int.TryParse(number, out numItems) || numItems < 1 || numItems > 100);
            return numItems;
        }
         private decimal GetDecimalInput(string type)
        {
            decimal finalAmount = 0;
            string amount = TakeInput($"Please Enter {type}: ");
            switch (type)
            {
                case "Tip amount":
                case "Tendered amount":
                case "Cash amount":
                case "Credit card amount":
                case "Reward disount amount":
                case "Other charges amount":
                case "Cash discount amount":
                    if (decimal.TryParse(amount, out decimal cash))
                    {
                        finalAmount = cash;
                    }
                    break;

                default:
                    finalAmount = 0; break;
                    
            }
            return finalAmount;
        }
        private List<InvoiceTenderDto> AddTenderList(SaleInvoiceDto saleInvoice)
        {
            var dataSet = new List<InvoiceTenderDto>();
            foreach (var item in saleInvoice.InvoiceItems)
            {
                var data = new InvoiceTenderDto();
                data.InvoiceNumber = item.InvoiceNumber;
                dataSet.Add(data);
            }
            return dataSet;
        }
        private List<ElectronicTenderDto> AddElectronicTender(SaleInvoiceDto invoiceDto)
        {
            var dataSet = new List<ElectronicTenderDto>();
            foreach (var item in invoiceDto.InvoiceItems)
            {
                var data = new ElectronicTenderDto();
                data.InvoiceId = item.InvoiceId;
                data.InvoiceNumber = item.InvoiceNumber;
                dataSet.Add(data);
            }
            return dataSet;
        }
        private async Task<List<InvoiceItemDto>> GetRandomInvoiceItems(int toTake, decimal discount, string saleInvoiceId)
        {
            var dataSet = new List<InvoiceItemDto>();

            var itemsData = await GetInvoiceItems(toTake).ConfigureAwait(false);

            foreach(var item in itemsData)
            {
                var data = new InvoiceItemDto();
                data.InvoiceItemId = item.ProductCode;
                data.ItemId = item.Id;
                data.SKUCode = item.ItemSKUs?.FirstOrDefault()?.SKUCode;
                data.ItemName = item.ItemName;
                data.IsReturnable = item.IsReturnable;
                data.ItemSKUId = item.ItemSKUs.FirstOrDefault()?.Id;
                data.DepartmentId = item.DepartmentId;
                data.Discount = discount;
                data.InvoiceId = saleInvoiceId;
                data.SoldAs = item.SoldAs;
                if (item.IsTaxable)
                {
                    data.Tax = 2;
                    data.TaxRate = 2;
                }
                data.Type = Enums.TransactionType.Sale;
                data.Image = item.Image;
                data.InvoiceItemDiscounts = null;
                
                dataSet.Add(data);
            }
            return dataSet;
        }
        private List<InvoiceDiscountDto> AddDiscounts(SaleInvoiceDto items)
        {
            var dataSet = new List<InvoiceDiscountDto>();
            foreach(var item in items.InvoiceItems)
            {
                var data = new InvoiceDiscountDto();
                data.InvoiceId = item.InvoiceId;
                data.InvoiceItemId = items.InvoiceDiscounts?.FirstOrDefault()?.InvoiceItemId;
                dataSet.Add(data);
            }
            return dataSet;
        }
        private List<InvoiceProductNoteDto> AddNotes(SaleInvoiceDto invoiceDto)
        {
            var dataSet = new List<InvoiceProductNoteDto>();
            foreach (var item in invoiceDto.InvoiceItems)
            {
                var data = new InvoiceProductNoteDto();
                data.InvoiceId = item.InvoiceId;
                data.InvoiceNumber = item.InvoiceNumber;
                data.ItemName = item.ItemName;
                data.SKUCode = item.SKUCode;
                data.Note = invoiceDto.CustomerNote;
                dataSet.Add(data);
            }
            return dataSet;
        }

        private async Task<List<Items>> GetInvoiceItems(int toTake)
        {
            var items = new List<Items>();

            var token = GetData(CacheKey.BearerToken).ToString();

            string formattedEndpoint = string.Empty;

            var builder = new StringBuilder();
            builder.Append(ClientEndPoints.BaseSassylUrl);
            builder.Append(ClientEndPoints.InventoryService);
            builder.Append(ClientEndPoints.Api);

            if (toTake > 0 && toTake <= 100)
            {
                builder.Append(ClientEndPoints.GetProduct);
                builder.Replace("{{pageNo}}", "1");
                builder.Replace("{{pageSize}}", $"{toTake}");
            }

            formattedEndpoint = builder.ToString();


            Webhook.BearerToken = token;

            var responseData = await Webhook.SendAsync<Connect360Response, Connect360Response>(formattedEndpoint, "application/json", AddHeaders()).ConfigureAwait(false);

            var responseObj = JsonConvert.DeserializeObject<Connect360Response>(JsonConvert.SerializeObject(responseData));

            if (responseObj.Success)
            {
                items = JsonConvert.DeserializeObject<List<Items>>(JsonConvert.SerializeObject(responseObj.Data));
            }
            if (!responseObj.Success)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {responseObj.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            return items;
        }
        
        private async Task<bool> StartInvoiceTaskAsync(SaleInvoiceDto invoiceDto)
        {
            var token = GetData(CacheKey.BearerToken);

            var builder = new StringBuilder();
            builder.Append(ClientEndPoints.Localhost);
            //builder.Append(ClientEndPoints.OrderService);
            builder.Append(ClientEndPoints.Api);
            builder.Append(ClientEndPoints.SaleInvoice);

            string formattedEndpoint = builder.ToString();
            Webhook.BearerToken = token.ToString();
            var responseData = await Webhook.SendAsync(formattedEndpoint,invoiceDto,"application/json",AddHeaders()).ConfigureAwait(false);

            var stringContent = await responseData.Content.ReadAsStringAsync().ConfigureAwait(false);
            var responseObj = JsonConvert.DeserializeObject<Connect360Response>(stringContent);

            if (responseObj.Success)
            {
                DisplayPrettyData(responseObj);
                return true;
            }
            if (!responseObj.Success)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                DisplayPrettyData(responseObj);
            }
            return false;
        }
    }
}
