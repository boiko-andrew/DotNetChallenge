using System;
using System.Security.Cryptography.X509Certificates;

namespace task_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("In this little program we want to test");
            Console.WriteLine("whether preincrement and postincrement");
            Console.WriteLine("are different or not");

            int i = 5;
            Console.WriteLine("i value is: {0}", i);

            int j = Preincrement(ref i);
            Console.WriteLine("j is the result of i preincrement");
            Console.WriteLine("i value is {0}", i);
            Console.WriteLine("j value is {0}", j);

            int k = Postincrement(ref i);
            Console.WriteLine("k is the result of i postincrement");
            Console.WriteLine("i value is {0}", i);
            Console.WriteLine("k value is {0}", k);

            Console.Write("Press any key to close the application: ");
            Console.ReadLine();
        }

        public static int Preincrement(ref int value)
        {
            ++value;
            return value;
        }

        public static int Postincrement(ref int value)
        {
            value++;
            return value;
        }
    }
}