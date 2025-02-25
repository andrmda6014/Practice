using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait
{
    internal class Program
    {
        static async Task Main()
        {
            var urls = new[]
            {
            "https://google.com",
            "https://example.com",
            "https://ya.ru"
            };

            var tasks = new Task<string>[urls.Length];

            for (int i = 0; i < urls.Length; i++)
            {
                tasks[i] = LoadPageAsync(urls[i]);
            }

            try
            {
                var results = await Task.WhenAll(tasks);

                for (int i = 0; i < results.Length; i++)
                {
                    Console.WriteLine($"Длина содержимого страницы {urls[i]}: {results[i].Length} символов");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
        static async Task<string> LoadPageAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }
    }
}
