using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CLI
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter to continue");
            Console.ReadLine();
            DoIt();
            Console.ReadLine();
        }

        private static async void DoIt()
        {
            using (var stringContent = new StringContent("{ \"firstName\": \"Sandy\" }", System.Text.Encoding.UTF8,
               "application/json"))
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(
                            "Basic",
                            Convert.ToBase64String(
                                System.Text.Encoding.ASCII.GetBytes(
                                    string.Format("{0}:{1}", "Sandy", "Password"))));

                    // 1. Consume the POST command
                    var response = await client.PostAsync("https://localhost:44377/api/values/33", stringContent);
                    var result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Result from POST command: " + result);
                    }

                    // 2. Consume the GET command
                    response = await client.GetAsync("https://localhost:44377/api/values/33");
                    if (response.IsSuccessStatusCode)
                    {
                        var id = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Result from GET command: " + result);
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();

                }
            }
        }
    }
}