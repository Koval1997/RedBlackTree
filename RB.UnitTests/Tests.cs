using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RB;

namespace RB.UnitTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void AddingOneMillionNumbers()
        {
            var tree = new RedBlackTree<int>();

            for (int i = 0; i < 1000000; i++)
            {
                tree.Insert(i);
            }

            Assert.IsTrue(tree.Count == 1000000);
        }

        [TestMethod]
        public void BalancingCheckWithRootRotation()
        {
            var tree = new RedBlackTree<int>();

            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
                       
            Assert.IsTrue(tree.RootNode == 2&& tree.RootNode.RightNode(1)&& tree.RootNode.LeftNode(3));

        }

        [TestMethod]
        public void FindMaxAndMinValue()
        {
            var tree = new RedBlackTree<int>();

            for (int i = 0; i < 1000000; i++)
            {
                tree.Insert(i);
            }

            Assert.IsTrue(tree.MaxValue == 0 & tree.MaxValue == 999999);
        }
    }
}
