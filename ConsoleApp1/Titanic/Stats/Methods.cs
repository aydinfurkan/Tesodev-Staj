using System;
using System.Collections.Generic;
using System.Linq;

namespace Titanic.Stats
{
    public static class Methods
    {
        public static List<int> Group<T>(List<T> list,Func<T, int> index, int size)
        {
            var result = Enumerable.Repeat(0, size).ToList();
            foreach (var item in list)
            {
                result[index(item)] += 1;
            }

            return result;
        }

        public static List<double> Rate(List<int> survive, List<int> death, int size)
        {
            var result = Enumerable.Repeat(0d, size).ToList();

            List<double> s = survive.Select<int, double>(i => i).ToList();
            List<double> d = death.Select<int, double>(i => i).ToList();
            
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = s[i] / (s[i] + d[i]);
            }

            return result;
        }
    }
}