using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawings
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 25;
            
            // Drawing 01
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    Console.Write(
                        // i > j ? "# " : ". "
                        //i % (j + 1) > 5 ? "# " : ". "
                        //i * j % 7 == 2 ? "# " : ". "

                        ((i + j) % 2 == 0 && !(i % 2 == 0 && (i + j) % 4 == 0)) ? "# " : ". ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();

            // Drawing 02
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    Console.Write(
                        (i + j) % 3 == 0  ? "# " : ". ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();

            // Drawing 03
            int[] hashMask = new int[] { 0, 2, 3, 6, 7, 8,
                12, 13, 14, 15, 20, 21, 22, 23, 24};
            int idx = -1;

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    idx = Array.IndexOf(hashMask, j);
                    Console.Write(
                        idx >= 0 ? "# " : ". ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();

            // Drawing 04
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    Console.Write(
                        (i == 0) || (i == size - 1) ||
                        (j == 0) || (j == size - 1) ||
                        (i == j) || (i == (size - j)) ? "# " : ". ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
