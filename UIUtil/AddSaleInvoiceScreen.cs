using Newtonsoft.Json;
using sassy.bulk.Cache;
using sassy.bulk.Dtos;
using sassy.bulk.Endpoints;
using sassy.bulk.ResponseDto;
using sassy.bulk.UIUtil.Abstract;
using sassy.bulk.Webhooks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sassy.bulk.UIUtil
{
    public class AddSaleInvoiceScreen : Base
    {
        private int CuNoOfItems { get; set; } = 0;
        private decimal CuTipAmount { get; set; } = 0;
        private decimal CuTenderedAmount { get; set; } = 0;
        private decimal CuCashAmount { get; set; } = 0;
        private decimal CuCreditCardAmount { get; set; } = 0;
        private decimal CuRewardAmount { get; set; } = 0;
        private decimal CuOtherChargesAmount { get; set; } = 0;
        private decimal CuCashDiscountAmount { get; set; } = 0;
        private string CuNote { get; set; } = "";

        public override void StartScreen()
        {
            var displayUser = DisplayUserData();
            label:
            var invoiceData = new SaleInvoiceDto();
            int indexer = InputInt($"{displayUser} how many records you want to add?: ");
            
            CuNoOfItems = GetNumberOfItems();
            CuTipAmount = GetDecimalInput("Tip amount");
            CuTenderedAmount = GetDecimalInput("Tendered amount");
            CuCashAmount = GetDecimalInput("Cash amount");
            CuCreditCardAmount = GetDecimalInput("Credit card amount");
            CuRewardAmount = GetDecimalInput("Reward disount amount");
            CuOtherChargesAmount = GetDecimalInput("Other charges amount");
            CuCashDiscountAmount = GetDecimalInput("Cash discount amount");
            CuNote = TakeInput("PLease Enter Customer Note: ");

            PromptToEdit();

            invoiceData.TipAmount = CuTipAmount;
            invoiceData.TenderedAmount = CuTenderedAmount;
            invoiceData.CashAmount = CuCashAmount;
            invoiceData.CreditCardAmount = CuCreditCardAmount;
            invoiceData.RewardDiscount = CuRewardAmount;
            invoiceData.TotalSaleItems = CuNoOfItems;
            invoiceData.OtherChargesAmount = CuOtherChargesAmount;
            invoiceData.UserId = GetData(CacheKey.UserId).ToString();
            invoiceData.UserName = displayUser;
            invoiceData.InvoiceItems = GetRandomInvoiceItems(CuNoOfItems, CuCashDiscountAmount, invoiceData.InvoiceNumber).GetAwaiter().GetResult();
            invoiceData.InvoiceDiscounts = AddDiscounts(invoiceData);
            invoiceData.InvoiceProductNotes = AddNotes(invoiceData);
            invoiceData.ElectronicTenders = AddElectronicTender(invoiceData);
            invoiceData.InvoiceTenders = AddTenderList(invoiceData);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Your Task is started take a while we complete......");
            var sw = Stopwatch.StartNew();
            Console.WriteLine($"Started at: {DateTime.Now}");

            for (int i = 0; i < indexer; i++)
            {
                Console.WriteLine("\rGenerating invoices... ({0}/{1})", i + 1, indexer);
                invoiceData.InvoiceNumber = RandomInvoiceNo();
                var isSaved = StartInvoiceTaskAsync(invoiceData).GetAwaiter().GetResult();
                if (!isSaved)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invoice No {invoiceData.InvoiceNumber} failed to saved.");
                    Console.ForegroundColor = ConsoleColor.White;
                    string strtAgain = Input("\rDo you want to start again?:");
                    if (strtAgain == "yes" || strtAgain == "y")
                    {
                        goto label;
                    }
                }
            }

            sw.Stop();
            Console.ForegroundColor = ConsoleColor.White;
            DisplayCompletionTime(sw.Elapsed);

            string anotherThread = Input("Add another?: ");
            if (anotherThread == "yes" || anotherThread == "y")
            {
                goto label;
            }
        }
        private bool PromptToEdit()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string changes = Input("Do you want to make changes?: ");
            Console.ForegroundColor = ConsoleColor.White;
            if(changes == "yes" || changes == "y")
            {
                EditValues:

                Console.WriteLine("Review and edit values:");
                Console.WriteLine("1. Tip Amount: {0}", CuTipAmount);
                Console.WriteLine("2. Tendered Amount: {0}", CuTenderedAmount);
                Console.WriteLine("3. Cash Amount: {0}", CuCashAmount);
                Console.WriteLine("4. Credit Card Amount: {0}", CuCreditCardAmount);
                Console.WriteLine("5. Reward Discount: {0}", CuRewardAmount);
                Console.WriteLine("6. Other Charges: {0}", CuOtherChargesAmount);
                Console.WriteLine("7. Cash Discount: {0}", CuCashDiscountAmount);
                Console.WriteLine("8. Number of items: {0}", CuNoOfItems);
                Console.WriteLine("9. Customer Note: {0}", CuNote);

                int choice = Input("Select any option: ", 1, 9);
                switch (choice)
                {
                    case 1:
                        CuTipAmount = GetDecimalInput("Tip amount");break;
                    case 2:
                        CuTenderedAmount = GetDecimalInput("Tendered amount");break;
                    case 3:
                        CuCashAmount = GetDecimalInput("Cash amount");break;
                    case 4:
                        CuCreditCardAmount = GetDecimalInput("Credit card amount"); break;
                    case 5:
                        CuRewardAmount = GetDecimalInput("Reward disount amount"); break;
                    case 6:
                        CuOtherChargesAmount = GetDecimalInput("Other charges amount");break;
                    case 7:
                        CuCashDiscountAmount = GetDecimalInput("Cash discount amount");break;
                    case 8:
                        CuNoOfItems = GetNumberOfItems();break;
                    case 9:
                        CuNote = TakeInput("PLease Enter Customer Note: ");break;
                }
                string editOther = Input("Do you want to make another change?: ");
                
                if (editOther == "yes" || editOther == "n")
                {
                    goto EditValues;
                }

                return true;
            }
            return false;
        }
        private string RandomInvoiceNo()
        {
            string prefix = "RMA";
            string suffix = "ZSV";
            Random random = new Random();
            int randomNumber = random.Next(1, 999);
            string randomString = $"{prefix}{randomNumber}{DateTimeOffset.Now.ToUnixTimeSeconds().ToString()}_auto_{suffix}";
            return randomString;
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
                data.InvoiceId = item.InvoiceId;
                data.InvoiceNumber = item.InvoiceNumber;
                data.TipAmount = saleInvoice.TipAmount;
                data.CashDiscount = saleInvoice.CashDiscount;
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
                data.TipAmount = invoiceDto.TipAmount;
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
            builder.Append(ClientEndPoints.BaseSassylUrl);
            builder.Append(ClientEndPoints.OrderService);
            builder.Append(ClientEndPoints.Api);
            builder.Append(ClientEndPoints.SaleInvoice);

            string formattedEndpoint = builder.ToString();
            Webhook.BearerToken = token.ToString();
            var responseData = await Webhook.SendAsync(formattedEndpoint, invoiceDto,"application/json", AddHeaders()).ConfigureAwait(false);

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
