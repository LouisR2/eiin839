using System;

    internal class Program
    {
        private static void Main(string[] args)
        {

            if (args.Length >= 2)
            {
                Console.WriteLine(mymethod(args[0], args[1]));
            }
            else
            {
                Console.WriteLine("Syntax error: the call must contain at least two argument");
            }
        }

        public static string mymethod(string param1, string param2)
        {
            return "<html><body><h1>Hello " + param1 + " et " + param2 + "</h1></body></html>";
        }

    }