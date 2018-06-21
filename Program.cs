using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB
{
    class Program
    {
        static void Main(string[] args)
        {
            //        var tree = new RedBlackTree<int>();

            //          tree.Insert(10);
            //          tree.Insert(25);
            //          tree.Insert(7);
            //          tree.Insert(3);
            //          tree.Insert(19);
            //          tree.Insert(42);
            //          tree.Insert(1);
            //          tree.Insert(14);
            //          tree.Insert(31);
            //          tree.Insert(13);
            //          tree.Insert(2);
            //          tree.Insert(9);
            //          tree.Insert(17);
            //          tree.Insert(6);
            //          tree.Insert(11);
            //          tree.Insert(18);
            //          tree.Insert(26);
            //          tree.Insert(16);
            //          tree.Insert(27);

            //Console.WriteLine();
            //          Console.WriteLine("Testing :");
            //          foreach (int val in tree)
            //                 Console.WriteLine(val.ToString());
            var tree = new RedBlackTree<int>();

            for (int i = 0; i < 1000000; i++)
            {
                tree.Insert(i);
            }
            Console.WriteLine(tree.MinValue);
            Console.WriteLine(tree.MaxValue);
            Console.ReadKey();
        }
    }
}
