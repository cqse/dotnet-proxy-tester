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

            // disable SSL validation
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;

            var defaultProxy = WebRequest.DefaultWebProxy;
            string usedProxy = defaultProxy?.GetProxy(new Uri(url))?.ToString();
            if (usedProxy == url)
            {
                usedProxy = null;
            }

            Console.WriteLine("Used Proxy: " + (usedProxy ?? "null"));
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