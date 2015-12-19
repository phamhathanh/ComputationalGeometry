using System;
using ComputationalGeometry.Common;
using ComputationalGeometry.TrapezoidalMap;

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
