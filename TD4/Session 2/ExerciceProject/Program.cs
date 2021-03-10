using System;
using ServiceReference1;

namespace ExerciceProject
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculatorSoapClient client = new CalculatorSoapClient(CalculatorSoapClient.EndpointConfiguration.CalculatorSoap);

            String res = client.AddAsync(1, 2).Result.ToString();

            Console.WriteLine(res);
        }
    }
}
