using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventSourceSampleInheritance
{
    public class Program
    {
        static void Main()
        {
            Task.Run(async () => await MainAsync()).Wait();
        }

        static async Task MainAsync()
        {
            SampleEventSource.Logger.Startup();
            Console.WriteLine($"Log Guid: {SampleEventSource.Logger.Guid}");
            Console.WriteLine($"Log Name: {SampleEventSource.Logger.Name}");
            await NetworkRequestSampleAsync();
        }

        private static async Task NetworkRequestSampleAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://www.cninnovation.com";
                    SampleEventSource.Logger.CallService(url);

                    string result = await client.GetStringAsync(url);
                    SampleEventSource.Logger.CalledService(url, result.Length);
                }
                Console.WriteLine("Complete.................");
            }
            catch (Exception ex)
            {
                SampleEventSource.Logger.ServiceError(ex.Message, ex.HResult);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
