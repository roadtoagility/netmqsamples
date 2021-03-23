using Shared;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var files = Directory.GetFiles($"{AppDomain.CurrentDomain.BaseDirectory}\\Data");
            var count = 0;

            foreach(var item in files)
            {
                var content = File.ReadAllText(item);
                var data = new StringContent(content, Encoding.UTF8, "application/json");
                var url = "https://localhost:44380/api/register";
                using var client = new HttpClient();
                var response = await client.PostAsync(url, data);
                count++;
                string result = response.Content.ReadAsStringAsync().Result;
                var culture = CultureInfo.CreateSpecificCulture("en-US");
            }

            Console.WriteLine($"Sended: {count}");
            Console.ReadLine();
        }
    }
}
