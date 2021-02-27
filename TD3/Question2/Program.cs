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
            if (args.Length >= 1)
            {

                // Call asynchronous network methods in a try/catch block to handle exceptions.
                try
                {
                    string responseBody = await client.GetStringAsync("https://api.jcdecaux.com/vls/v3/stations?contract="+args[0]+"&apiKey=APIKEY");

                    //Display raw response body
                    //Console.WriteLine(responseBody);

                    Station[] stations = JsonSerializer.Deserialize<Station[]>(responseBody);

                    foreach (Station s in stations)
                    {
                        Console.WriteLine("STATION: " + s.name);
                        Console.WriteLine("ADDRESS: " + s.address);
                        Console.WriteLine("STATUS: " + s.status);
                        Console.WriteLine("TOTAL CAPACITY: " + s.totalStands.capacity);
                        Console.WriteLine("==================");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
            else
            {
                Console.WriteLine("You need to give the contract name in argument.");
            }
        }
    }

    class Station
    {
        public string name { get; set; }
        public string address { get; set; }
        public string status { get; set; }
        public Stands totalStands { get; set; }
    }

    class Stands {
        public Object availabilities { get; set; }  //TODO continue to parse the entire station object & display it prettily
        public int capacity { get; set; }
    }
}
