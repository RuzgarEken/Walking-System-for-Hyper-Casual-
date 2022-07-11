using System;
using System.Collections.Generic;

namespace Essentials.Extensions
{

    public static class ListExtensions
    {

        public static T GetRandomElement<T>(this IList<T> list, int seed)
        {
            if(list == null || list.Count == 0)
            {
                throw new ArgumentException();
            }

            System.Random rand = new System.Random(seed);

            var result = rand.Next(0, list.Count);
            return list[result];
        }
        
        public static T GetRandomElement<T>(this IList<T> list)
        {
            if(list == null || list.Count == 0)
            {
                throw new ArgumentException();
            }

            var result = UnityEngine.Random.Range(0, list.Count);
            return list[result];
        }

        public static T GetRandomElement<T>(this IList<T> list, Predicate<T> predicator)
        {
            int tryCap = 100;
            int tryCounter = 0;
            T element;
            
            do
            {
                element = list.GetRandomElement();
                tryCounter++;
            } 
            while (!predicator.Invoke(element) && tryCounter < tryCap);

            return element;
        }
        
        public static T GetRandomElement<T>(this IList<T> list, Predicate<T> predicator, int seed)
        {
            Random rand = new Random(seed);
            int tryCap = 100;
            int tryCounter = 0;
            T element;
            
            do
            {
                element = list[rand.Next(0, list.Count)];
                tryCounter++;
            } 
            while (!predicator.Invoke(element) && tryCounter < tryCap);

            return element;
        }

        public static T GetRandomElement<T>(this IList<T> list, Predicate<T> predicator, System.Random rand)
        {
            int tryCap = 100;
            int tryCounter = 0;
            T element;

            do
            {
                element = list[rand.Next(0, list.Count)];
                tryCounter++;
            }
            while (!predicator.Invoke(element) && tryCounter < tryCap);

            return element;
        }

        private static Random rng = new Random();
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        public static IList<T> Invoke<T>(this IList<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action.Invoke(item);
            }

            return list;
        }

    }

}