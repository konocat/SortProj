using System;
using System.Linq;

namespace SortProj.MVVM.Model
{
    public class RandomizeModel
    {
        private readonly Random _random;

        public RandomizeModel()
        {
            _random = new Random();
        }

        public int[] GenerateRandomArray(int size)
        {
            int[] array = Enumerable.Range(0, size).OrderBy(x => _random.Next()).Take(size).ToArray();
            return array;
        }
    }
}
