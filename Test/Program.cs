using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGeometry.Common;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            CommonTest();

            Console.ReadLine();
        }

        private static void CommonTest()
        {
            for (int i = 0; i < 10; i++)
            {
                foreach (int index in Utilities.RandomPermutation(5))
                {
                    Console.Write(index);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}
