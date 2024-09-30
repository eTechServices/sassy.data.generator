using Newtonsoft.Json;
using sassy.bulk.Cache;
using sassy.bulk.DBContext;
using sassy.bulk.Dtos;
using sassy.bulk.Endpoints;
using sassy.bulk.Enums;
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
        private ObjectResponse ConfigurationData = new ObjectResponse();
        private int CuNoOfItems { get; set; } = 0;
        private decimal CuTipAmount { get; set; } = 0;
        private decimal CuTenderedAmount { get; set; } = 0;
        private decimal CuCashAmount { get; set; } = 0;
        private decimal CuCreditCardAmount { get; set; } = 0;
        private decimal CuRewardAmount { get; set; } = 0;
        private decimal CuOtherChargesAmount { get; set; } = 0;
        private decimal CuCashDiscountAmount { get; set; } = 0;
        private decimal SubTotal { get; set; } = 0;
        private decimal GrandTotal { get; set; } = 0;
        private string CuNote { get; set; } = "";
        private string CustomerType { get; set; } = "";
        private string LocationName { get; set; } = "";
        private string RegisterName { get; set; } = "";
        private string RegisterId { get; set; } = "";
        private string LocationId { get; set; } = "";
        private string CustomerId { get; set; } = "";
        private string CustomerName { get; set; } = "";

        public override void StartScreen()
        {
            begin:
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            var displayUser = DisplayUserData();
            DisplayCustomerType();
            DisplayLocationsAndRegisters().GetAwaiter().GetResult();
            Console.ForegroundColor = ConsoleColor.White;
            
            takeData:
            var invoiceData = new SaleInvoiceDto();
            int indexer = InputInt($"{displayUser} how many records you want to add?: ");

            AskForCustomerType();
            AskForLocation();
            AskForRegister();

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
            GetCustomerList().GetAwaiter().GetResult();

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
            invoiceData.SubTotal = SubTotal;
            invoiceData.CustomerNote = CuNote;
            invoiceData.GrandTotal = Total();

            PrepaireData();
            PickRandomCustomer();

            invoiceData.CustomerId = CustomerId;
            invoiceData.CustomerName = CustomerName;
            invoiceData.LocationName = LocationName;
            invoiceData.locationId = LocationId;
            invoiceData.RegisterName = RegisterName;
            invoiceData.RegisterId = RegisterId;

            var tryagain = BeginThread(invoiceData, indexer).GetAwaiter().GetResult();

            if (!tryagain)
            {
                goto takeData;
            }

            string anotherThread = Input("Add another?: ");
            if (anotherThread == "yes" || anotherThread == "y")
            {
                Console.Clear();
                goto begin;
            }
        }
        private void AskForLocation()
        {
            LocationName = TakeInput("Please Enter location name: ");
        }
        private void AskForRegister()
        {
            RegisterName = TakeInput("PLease Enter Register Name: ");
        }
        private void AskForCustomerType()
        {
            while (true)
            {
                CustomerType customerType;
                string inputCustomer = TakeInput("Please enter customer type: ");
                if (Enum.TryParse(inputCustomer, true, out customerType))
                {
                    CustomerType = inputCustomer;
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Customer selected");
                Console.ForegroundColor= ConsoleColor.White;
            }
        }
        private void PrepaireData()
        {
            var location = GetData(CacheKey.LocationList) as List<LocationsResponseDto>;
            var registers = GetData(CacheKey.RegisterList) as List<RegisterResponseDto>;

            if(location.Count > 0)
            {
                var findLocationData = location.Where(x => x.LocationName == LocationName).FirstOrDefault();
                LocationId = findLocationData?.LocationId.ToString();
                LocationName = findLocationData?.LocationName;
            }
            if(registers.Count > 0)
            {
                var findRegistersData = registers.Where(x => x.RegisterName == RegisterName).FirstOrDefault();
                RegisterId = findRegistersData.RegisterId.ToString();
                RegisterName = findRegistersData?.RegisterName;
            }
        }
        private void PickRandomCustomer()
        {
            var random = new Random();

            var customerData = GetData(CacheKey.CustomerList) as List<CustomerDataResponse>;

            if (customerData.Count > 0)
            {
                int randomNumber = random.Next(1, 21);
                if (randomNumber <= customerData.Count)
                {
                    var randomCustomer = customerData[randomNumber - 1];
                    CustomerId = randomCustomer.CustID;
                    CustomerName = randomCustomer.FirstName + randomCustomer.LastName;
                }
            }

        }
        private async Task GetCustomerList()
        {
            var customerDataSet = new List<CustomerDataResponse>();
            var token = GetData(CacheKey.BearerToken).ToString();
            
            var builder = new StringBuilder();
            builder.Append(ClientEndPoints.BaseSassylUrl);
            builder.Append(ClientEndPoints.CustomerService);
            builder.Append(ClientEndPoints.Api);
            builder.Append(ClientEndPoints.CreateCustomer);
            if (CustomerType != "")
            {
                builder.Append($"?pageNo=0&pageSize=20&type={CustomerType}");
            }

            var formattedEndpoint = builder.ToString();
            Webhook.BearerToken = token;

            var responseData = await Webhook.SendAsync<Connect360Response, Connect360Response>(formattedEndpoint, "application/json", AddHeaders()).ConfigureAwait(false);
            
            var responseObj = JsonConvert.DeserializeObject<Connect360Response>(JsonConvert.SerializeObject(responseData));

            if(responseObj.Success)
            {
               customerDataSet = JsonConvert.DeserializeObject<List<CustomerDataResponse>>(JsonConvert.SerializeObject(responseObj.Data));
                Stack.Insert(CacheKey.CustomerList, customerDataSet);
            }

        }
        private void PromptToEdit()
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
                Console.WriteLine("10: Customer Type: {0}", CustomerType);
                Console.WriteLine("11: Location Name: {0}", LocationName);
                Console.WriteLine("12: Register Name: {0}", RegisterName);

                int choice = Input("Select any option: ", 1, 12);

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
                    case 10:
                         AskForCustomerType();break;
                    case 11:
                        AskForLocation();break;
                    case 12:
                        AskForRegister();break;
                }

                string editOther = Input("Do you want to make another change?: ");
                
                if (editOther == "yes" || editOther == "y")
                {
                    goto EditValues;
                }
            }
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

        private decimal SumTotal(decimal priceA) => SubTotal+=priceA;
        private decimal Total() => SubTotal - CuCashDiscountAmount;

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
        private void DisplayCustomerType()
        {
            int i = 1;
            Console.WriteLine("--------------Customer Types---------------");
            foreach (var customerTypeName in Enum.GetNames(typeof(CustomerType)))
            {
                Console.WriteLine($"Customer {i++}: {Enum.Parse(typeof(CustomerType), customerTypeName)}");
            }
        }
        private async Task<ObjectResponse> DisplayLocationsAndRegisters()
        {
            var token = GetData(CacheKey.BearerToken).ToString();

            var builder = new StringBuilder();
            builder.Append(ClientEndPoints.BaseSassylUrl);
            builder.Append(ClientEndPoints.InventoryService);
            builder.Append(ClientEndPoints.Api);
            builder.Append(ClientEndPoints.Configurations);

            var formattedEndpoint = builder.ToString();

            Webhook.BearerToken = token;

            var responseData = await Webhook.SendAsync<Connect360Response, Connect360Response>(formattedEndpoint, "application/json", AddHeaders()).ConfigureAwait(false);

            var responseObj = JsonConvert.DeserializeObject<Connect360Response>(JsonConvert.SerializeObject(responseData));

            if (responseObj.Success)
            {
                if(responseObj.Data != null)
                {
                    ConfigurationData = JsonConvert.DeserializeObject<ObjectResponse>(JsonConvert.SerializeObject(responseObj.Data));
                }
                if (ConfigurationData != null)
                {
                    int loc = 1;
                    int reg = 1;
                    Console.WriteLine("--------------Available Locations----------------");
                    
                    Stack.Insert(CacheKey.RegisterList, ConfigurationData.Registers);
                    Stack.Insert(CacheKey.LocationList, ConfigurationData.Locations);
                    
                    foreach (var location in ConfigurationData.Locations)
                    {
                        Console.WriteLine($"Location {loc++}: {location.LocationName}");
                    }
                    Console.WriteLine("--------------Available Registers----------------");
                    foreach (var register in ConfigurationData.Registers)
                    {
                        Console.WriteLine($"Register {reg++}: {register.RegisterName}");
                    }
                }
            }

            return ConfigurationData;
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
                if(item.ItemSKUs.FirstOrDefault().DefaultPrice == DefaultPrice.SellPriceA)
                {
                    data.Price = item.ItemSKUs.FirstOrDefault().SalePriceA;
                    SumTotal(data.Price);
                }
                if(item.ItemSKUs.FirstOrDefault().DefaultPrice == DefaultPrice.SellPriceB)
                {
                    data.Price = item.ItemSKUs.FirstOrDefault().SalePriceB;
                    SumTotal(data.Price);
                }
                if(item.ItemSKUs.FirstOrDefault().DefaultPrice == DefaultPrice.SellPriceC)
                {
                    data.Price = item.ItemSKUs.FirstOrDefault().SalePriceC;
                    SumTotal(data.Price);
                }
                data.Price = item.ItemSKUs.FirstOrDefault().SalePriceA;
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
        private async Task<bool> BeginThread(SaleInvoiceDto invoiceData, int indexer)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Your Task is started take a while we complete......");
            var sw = Stopwatch.StartNew();
            Console.WriteLine($"Started at: {DateTime.Now}");

            for (int i = 0; i < indexer; i++)
            {
                Console.WriteLine("\rGenerating invoices... ({0}/{1})", i + 1, indexer);
                invoiceData.InvoiceNumber = RandomInvoiceNo();
                var isSaved = await StartInvoiceTaskAsync(invoiceData).ConfigureAwait(false);

                if (!isSaved)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invoice No {invoiceData.InvoiceNumber} failed to saved.");
                    Console.ForegroundColor = ConsoleColor.White;
                    string strtAgain = Input("\rDo you want to start again?:");
                    if (strtAgain == "yes" || strtAgain == "y")
                    {
                        return false;
                    }
                }
            }

            sw.Stop();
            Console.ForegroundColor = ConsoleColor.White;
            DisplayCompletionTime(sw.Elapsed);
            return true;
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
