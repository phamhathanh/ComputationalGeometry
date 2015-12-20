using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry.Common
{
    public class RandomPermutation : IEnumerable<int>
    {
        private readonly List<int> permutation;

        // DO NOT USE THIS INSIDE A LOOP
        public RandomPermutation(int n)
        {
            var original = new List<int>(n);
            for (int i = 0; i < n; i++)
                original.Add(i);

            int randomIndex;
            Random generator = new Random();

            this.permutation = new List<int>(n);
            for (int i = n - 1; i > -1; i--)
            {
                randomIndex = generator.Next(0, i + 1);
                var randomItem = original[randomIndex];
                permutation.Add(randomItem);
                original.RemoveAt(randomIndex);
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            foreach (var item in permutation)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
