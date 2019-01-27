using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventSourceSample
{
    public class SimpleEventSourceSample
    {
        private static EventSource sampleEventSource = new EventSource("Roka-Event-Source");

        static void Main()
        {
            Task.Run(async () => await MainAsync()).Wait();
        }

        static async Task MainAsync()
        {
            Console.WriteLine($"Log Guid: {sampleEventSource.Guid}");
            Console.WriteLine($"Log Name: {sampleEventSource.Name}");

            sampleEventSource.Write("Startup", new { Info = "Roka app started" });
            await NetworkRequestSampleAsync();
            sampleEventSource.Dispose();
        }

        private static async Task NetworkRequestSampleAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://www.cninnovation.com";
                    sampleEventSource.Write("Network", new { Info = $"calling {url}" });

                    string result = await client.GetStringAsync(url);
                    sampleEventSource.Write("Network", new { Info = $"completed call to {url}, result string length: {result.Length}" });
                }
                Console.WriteLine("Complete.................");
            }
            catch (Exception ex)
            {
                sampleEventSource.Write("Network Error", new EventSourceOptions { Level = EventLevel.Error }, new { Message = ex.Message, Result = ex.HResult });
                Console.WriteLine(ex.Message);
            }
        }
    }
}
