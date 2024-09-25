using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using sassy.bulk.BotHelper;
using sassy.bulk.Cache;
using sassy.bulk.DBContext;
using sassy.bulk.Endpoints;
using sassy.bulk.Enums;
using sassy.bulk.RequestDto;
using sassy.bulk.ResponseDto;
using sassy.bulk.TokenUtil;
using sassy.bulk.UIUtil.Abstract;
using sassy.bulk.Webhooks;

namespace sassy.bulk.UIUtil
{
    internal class LoginScreen : Base
    {
        public override void StartScreen()
        {
            Clear();
            Console.WriteLine($"{Bot.Welcome}");
            Console.WriteLine("------------------");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");
            Console.WriteLine();
            int choice = Input(">>>>> ", 1, 2);
            switch (choice)
            {
                case 1:
                    Login();
                    break;
                    case 2:
                    KillApplication();
                    break;
            }
        }
        private void Login()
        {
            string userName = "";
            string password = "";
            Clear();
            Console.WriteLine($"{Bot.Welcome}");
            Console.WriteLine("Enter your credentials:");
            Console.WriteLine("-----------------------");
            bool isLoggedIn = true;

            while (isLoggedIn)
            {
                userName = TakeInput("Username: ");
                password = TakeInput("Password: ");
                if (IsValidInput(userName, password))
                {
                    isLoggedIn = CheckClientCredentails(userName, password).GetAwaiter().GetResult();
                    if (!isLoggedIn) { break; }
                    
                }
            }
            if (!isLoggedIn)
            {
                var main = new MainScreen();
                main.StartScreen();
            }
        }
        private async Task<bool> CheckClientCredentails(string userName, string password)
        {
            var requestBody = new SIgninRequestDto();
            var builder = new StringBuilder();
            builder.Append(ClientEndPoints.BaseSassylUrl);
            builder.Append(ClientEndPoints.AuthService);
            builder.Append(ClientEndPoints.SignIn);
            string formattedEndpoint = builder.ToString();

            requestBody.UserName = userName;
            requestBody.Password = password;

            Console.WriteLine("Verifying account.......");

            var result = await Webhook.SendAsync(formattedEndpoint, requestBody, "application/json", AddHeaders()).ConfigureAwait(false);

            var stringContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            var responseObj = JsonConvert.DeserializeObject<Connect360Response>(stringContent);
            if (responseObj.Success)
            {
                var token = JsonConvert.DeserializeObject<TokenData>(JsonConvert.SerializeObject(responseObj.Data));
                if (token.AccountStatus != AccountStatus.DeploymentCompleted)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid Credentails");
                    Console.WriteLine($"{token.Error}");
                    Console.ForegroundColor = ConsoleColor.White;
                    return true;
                }

                var busineeName = Decrypt.FindBusinness(token.Token);
                var userId = Decrypt.FindUserId(token.Token);

                Stack.Insert(CacheKey.BearerToken, token.Token);
                Stack.Insert(CacheKey.UserName, userName);
                Stack.Insert(CacheKey.Password, password);
                Stack.Insert(CacheKey.BusinessName, busineeName);
                Stack.Insert(CacheKey.UserId, userId);
            }
            return false;
        }
    }
}
