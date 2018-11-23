using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProxyTester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string url = "http://google.com";
            if (args.Length > 0)
            {
                url = args[0];
            }

            var proxy = WebRequest.DefaultWebProxy;
            Console.WriteLine("Used Proxy: " + proxy?.GetProxy(new Uri(url))?.ToString() ?? "null");
            Console.WriteLine("-----");

            FetchAsync(url).Wait();
        }

        private static async Task FetchAsync(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                string body = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Connected successfully\nStatus: {response.StatusCode}\n\nResponse: {body.Substring(0, 500)}...");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to connect\nException: {e.GetType()} - {e.Message}\n{e}");
            }
        }
    }
}