using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Question1
{
    class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string responseBody = await client.GetStringAsync("https://api.jcdecaux.com/vls/v3/contracts?apiKey=API_KEY_HERE");

                //Display raw response body
                //Console.WriteLine(responseBody);

                Contract[] contracts = JsonSerializer.Deserialize<Contract[]>(responseBody);

                //Debug to check if serialization is good
                //Console.WriteLine(contracts.Length);

                foreach (Contract c in contracts)
                {
                    Console.WriteLine("Contract name : " + c.name);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }

    class Contract
    {
        public string name { get; set; }
        public string commercial_name { get; set; }
        public string[] cities { get; set; }
    }
}
