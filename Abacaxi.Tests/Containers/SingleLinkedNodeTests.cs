﻿/* Copyright 2017 by Alexandru Ciobanu (alex+git@ciobanu.org)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation 
 * files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, 
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software 
 * is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
 * FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
 * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

namespace Abacaxi.Tests.Containers
{
    using NUnit.Framework;
    using Abacaxi.Containers;

    [TestFixture]
    public class SingleLinkedNodeTests
    {
        [Test]
        public void Ctor_StoresTheValue()
        {
            var node = new SingleLinkedNode<int>(99);

            Assert.AreEqual(99, node.Value);
        }

        [Test]
        public void Next_CanBeAssigned()
        {
            var node = new SingleLinkedNode<int>(0);

            node.Next = node;
            Assert.AreSame(node, node.Next);
        }

        [Test]
        public void Next_CanBeSetToNull()
        {
            var node = new SingleLinkedNode<int>(0);
            node.Next = node;
            node.Next = null;

            Assert.IsNull(node.Next);
        }

        [Test]
        public void Create_ReturnsNull_ForEmptySequence()
        {
            var head = SingleLinkedNode<int>.Create(new int[] { });

            Assert.IsNull(head);
        }
        
        [Test]
        public void Create_ReturnsOneValidNode_ForSequenceOfOne()
        {
            var head = SingleLinkedNode<int>.Create(new[] { 1 });

            Assert.NotNull(head);
            Assert.AreEqual(1, head.Value);
            Assert.IsNull(head.Next);
        }

        [Test]
        public void Create_ReturnsTwoValidNodes_ForSequenceOfTwo()
        {
            var head = SingleLinkedNode<int>.Create(new[] { 1, 2 });

            Assert.NotNull(head);
            Assert.AreEqual(1, head.Value);
            Assert.IsNotNull(head.Next);
            Assert.AreEqual(2, head.Next.Value);
        }

        [Test]
        public void CheckIsKnotted_ReturnsFalse_ForSingleUnknottedNode()
        {
            var head = SingleLinkedNode<int>.Create(new[] { 1 });

            var check = head.CheckIsKnotted();
            Assert.IsFalse(check);
        }

        [Test]
        public void CheckIsKnotted_ReturnsTrue_ForSingleKnottedNode()
        {
            var head = SingleLinkedNode<int>.Create(new[] { 1 });
            head.Next = head;

            var check = head.CheckIsKnotted();
            Assert.IsTrue(check);
        }

        [Test]
        public void CheckIsKnotted_ReturnsTrue_ForDoubleKnottedNode()
        {
            var head = SingleLinkedNode<int>.Create(new[] { 1, 2 });
            head.Next.Next = head;

            var check = head.CheckIsKnotted();
            Assert.IsTrue(check);
        }

        [Test]
        public void CheckIsKnotted_ReturnsTrue_ForTripleKnottedNode()
        {
            var head = SingleLinkedNode<int>.Create(new[] { 1, 2, 3 });
            head.Next.Next.Next = head.Next;

            var check = head.CheckIsKnotted();
            Assert.IsTrue(check);
        }

        [Test]
        public void GetMiddleNode_ReturnsFirst_ForSingleNodeList()
        {
            var head = SingleLinkedNode<int>.Create(new[] { 1 });

            var node = head.GetMiddleNode();
            Assert.AreSame(head, node);
        }

        [Test]
        public void GetMiddleNode_ReturnsFirst_ForTwoNodeList()
        {
            var head = SingleLinkedNode<int>.Create(new[] { 1, 2 });

            var node = head.GetMiddleNode();
            Assert.AreSame(head, node);
        }

        [Test]
        public void GetMiddleNode_ReturnsSecond_ForThreeNodeList()
        {
            var head = SingleLinkedNode<int>.Create(new[] { 1, 2, 3 });

            var node = head.GetMiddleNode();
            Assert.AreSame(head.Next, node);
        }

        [Test]
        public void Reverse_DoesNothing_ForSingleNode()
        {
            var head = SingleLinkedNode<int>.Create(new[] { 1 });

            var newHead = head.Reverse();

            Assert.AreSame(head, newHead);
            Assert.IsNull(newHead.Next);
        }

        [Test]
        public void Reverse_Reverses_AListOfTwo()
        {
            var e1 = SingleLinkedNode<int>.Create(new[] { 1, 2 });
            var e2 = e1.Next;

            var newHead = e1.Reverse();

            Assert.AreSame(e2, newHead);
            Assert.AreSame(e2.Next, e1);
            Assert.IsNull(e1.Next);
        }


        [Test]
        public void Reverse_Reverses_AListOfThree()
        {
            var e1 = SingleLinkedNode<int>.Create(new[] { 1, 2, 3 });
            var e2 = e1.Next;
            var e3 = e2.Next;

            var newHead = e1.Reverse();

            Assert.AreSame(e3, newHead);
            Assert.AreSame(e3.Next, e2);
            Assert.AreSame(e2.Next, e1);
            Assert.IsNull(e1.Next);
        }
    }
}