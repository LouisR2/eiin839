using System;
using ServiceReference1;

namespace MathsLibraryClient
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Test client");

            MathsOperationsClient client = new MathsOperationsClient();
            int res = await client.AddAsync(1, 2);

            Console.WriteLine(res);
        }
    }
}
