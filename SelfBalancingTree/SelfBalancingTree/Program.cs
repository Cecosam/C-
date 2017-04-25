using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SelfBalancingTree
{
    public class Program
    {        
        static void Main(string[] args)
        {

            //var list = new List<int>() { 2, 1, 0, 3, 4, 10 };
            //var myArrowTree = new Tree<int>();
            //var myArrowTree = new Tree<int>();
            var myArrowTree = new Tree<int>();
            var count = 2000;

            var newArray = new int[10];

            var dic = new SortedSet<int>() { 33, 2, 3, 1 };
            //Console.WriteLine(string.Join(" ", dic));

            var rnd = new Random();
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                var current = rnd.Next(-10000, 10000);
                //myArrowTree.Add(i);
                myArrowTree.Add(current);
            }
            Console.WriteLine(sw.Elapsed);
            //myArrowTree.EnableSelfBalancing();
            Console.WriteLine(myArrowTree.IsPerfectlyBalancedAVLTree());
            //Console.WriteLine(myArrowTree.IsProperSubsetOf(list));

            //Console.WriteLine(string.Join(" ", myArrowTree));

            //Console.WriteLine(myArrowTree.Contains(333));
            //Console.WriteLine(myArrowTree.Count);
            sw.Reset();
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                var current = rnd.Next(-10000, 10000);
                //myArrowTree.Remove(i);
                myArrowTree.Remove(current);
            }
            Console.WriteLine(sw.Elapsed);
            //foreach (var item in myArrowTree)
            //{
            //    Console.Write("{0} ", item);
            //}

            //Console.WriteLine(myArrowTree.MaxValue);
            //Console.WriteLine(myArrowTree.MinValue);
            //Console.WriteLine(myArrowTree.SetEquals(list));
            //Console.WriteLine(myArrowTree.Overlaps(list));
            //Console.WriteLine(myArrowTree.IsProperSubsetOf(list));
            //Console.WriteLine(myArrowTree.IsProperSupersetOf(list));
            //Console.WriteLine(myArrowTree.IsSupersetOf(list));
            //Console.WriteLine(dic.IsSubsetOf(list));
            //myArrowTree.SymmetricExceptWith(list);
            //myArrowTree.ExceptWith(list);
            //myArrowTree.IntersectWith(list);
            //myArrowTree.UnionWith(list);
            //myArrowTree.CopyTo(newArray, 2);
            //myArrowTree.CopyTo(newArray);
            //Console.WriteLine(string.Join(" ", newArray));
            //Console.WriteLine(myArrowTree.ToString());
            Console.WriteLine(string.Join(" ", myArrowTree.GetTopLevels()));
            Console.WriteLine(myArrowTree.Count);
            //Console.WriteLine(myArrowTree.ToHashSet().Count);
            //Console.WriteLine(string.Join(" ", myArrowTree.ToList()));
            //Console.WriteLine(myArrowTree.ToList().Count);
            //myArrowTree.PrintElements();

            //var reversedList = myArrowTree.Reverse();
            //Console.WriteLine(string.Join(" ", reversedList));

            //var newTree = myArrowTree.DeepCopyOfThisTree();
            //for (int i = 0; i < 5; i++)
            //{
            //    newTree.Remove(i);
            //}
            //Console.WriteLine(myArrowTree.ToString());
            //Console.WriteLine(newTree.ToString());
        }
    }
}
