using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using sassy.bulk.DBContext;
using sassy.bulk.RequestDto;
using sassy.bulk.ResponseDto;

namespace sassy.bulk.UIUtil.Abstract
{
    public abstract class Base
    {
        /// <summary>
        /// Starts the main screen of the application.
        /// </summary>
        public abstract void StartScreen();
        /// <summary>
        /// Clears the console screen.
        /// </summary>
        public void Clear() => Console.Clear();

        /// <summary>
        /// Exits the application gracefully.
        /// </summary>
        public void KillApplication() => Environment.Exit(0);

        /// <summary>
        /// Restarts the application by clearing the console and exiting.
        /// </summary>
        public void RestartApplication()
        {
            Console.Clear();
            Environment.Exit(0);
        }
        /// <summary>
        /// Prompts the user for an integer input within a specified range.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <param name="minValue">The minimum allowed value.</param>
        /// <param name="maxValue">The maximum allowed value.</param>
        /// <returns>The valid integer input provided by the user.</returns>
        public int Input(string prompt, int minValue, int maxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= minValue && choice <= maxValue)
                {
                    return choice;
                }

                Console.WriteLine("Invalid input. Please enter a number between " + minValue + " and " + maxValue + ".");
            }
        }
        /// <summary>
        /// Prompts the user for an integer input within a specified range.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <returns>The valid integer input provided by the user.</returns>
        public int InputInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    return choice;
                }

                Console.WriteLine("Invalid input. Please enter a valid number");
            }
        }
        /// <summary>
        /// Prompts the user for a yes/no input.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <returns>The user's input, either "yes", "no", "y", or "n".</returns>
        public string Input(string prompt)
        {
            const string choice1 = "yes";
            const string charC = "y";
            const string choice2 = "no";
            const string charC2 = "n";

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                input.ToLower();

                if (input == choice1 || input== choice2 || input == charC2 || input == charC)
                {
                    return input;
                }

                Console.WriteLine("Invalid input.");
            }
        }
        /// <summary>
        /// Displays a progress bar indicating the completion percentage of a task.
        /// </summary>
        /// <param name="currentIteration">The current iteration number.</param>
        /// <param name="totalIterations">The total number of iterations.</param>
        public void ProgressBar(int currentIteration, int totalIterations)
        {
            double percentage = (double)currentIteration / totalIterations * 100;
            Math.Round(percentage);
            Console.Write("\r");
            Console.Write($"Loading... {percentage}%");
        }

        /// <summary>
        /// Prompts the user for input and validates it.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <returns>The validated user input.</returns>
        public string TakeInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (IsValidInput(input))
                {
                    return input;
                }

                Console.WriteLine("Invalid input. Please try again.");
            }
        }
        /// <summary>
        /// Deserializes the provided JSON data into a Connect360Response object and prints its Data property.
        /// </summary>
        /// <param name="data">The JSON data string.</param>
        public void Print(string data)
        {
            var result = JsonConvert.DeserializeObject<Connect360Response>(data);
            Console.WriteLine($"The Status is => {result.Success}");
            Console.WriteLine($"Message received => {result.Message}");
            Console.WriteLine($"Data Received => {JsonConvert.SerializeObject(result.Data)}");
        }
        /// <summary>
        /// Logs out the current user by disposing the underlying Stack object.
        /// </summary>
        public void Logout() => Stack.Dispose();
        /// <summary>
        /// Retrieves data from the cache using the specified key.
        /// </summary>
        /// <param name="key">The key of the cached data.</param>
        /// <returns>The cached data, or null if not found.</returns>
        public object GetData(string key) => Stack.Get(key);
        /// <summary>
        /// Prints the given data to the console.
        /// </summary>
        /// <param name="data">The data to print.</param>
        public void PrintData(string data)
        {
            Console.WriteLine(data);
        }
        /// <summary>
        /// Creates a list of key-value pairs representing HTTP headers.
        /// </summary>
        /// <returns>A list of key-value pairs containing the HTTP headers.</returns>
        public List<KeyValuePair<string,string>> AddHeaders()
        {
            var headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("Referer", RequestHeaders.Referer));
            headers.Add(new KeyValuePair<string, string>("Wlid", RequestHeaders.Wlid));
            return headers;
        }
        /// <summary>
        /// Navigates back to the main screen.
        /// </summary>
        public void Back()
        {
            var main = new MainScreen();
            main.StartScreen();
        }
        /// <summary>
        /// Validates whether the given username and password are not empty.
        /// </summary>
        /// <param name="userName">The username to validate.</param>
        /// <param name="password">The password to validate.</param>
        /// <returns>True if both username and password are not empty, otherwise false.</returns>
        public bool IsValidInput(string userName, string password) => !string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password);
        private bool IsValidInput(string input) => !string.IsNullOrEmpty(input);
        
    }
}
