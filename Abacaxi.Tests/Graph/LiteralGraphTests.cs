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

// ReSharper disable ObjectCreationAsStatement

namespace Abacaxi.Tests.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Graphs;
    using NUnit.Framework;

    [TestFixture]
    public class LiteralGraphTests
    {
        [Test]
        public void Ctor_ThrowsException_ForNullRelationships()
        {
            Assert.Throws<ArgumentNullException>(() => new LiteralGraph(null, true));
        }

        [TestCase(".")]
        [TestCase("A.")]
        [TestCase("AA-B")]
        [TestCase("A-")]
        [TestCase("A-BA")]
        public void Ctor_ThrowsException_ForInvalidFormat(string relationships)
        {
            Assert.Throws<FormatException>(() => new LiteralGraph(relationships, false));
        }

        [TestCase("A>B")]
        [TestCase("A<B")]
        public void Ctor_ThrowsException_ForInvalidEdgesInUndirectedGraph(string relationships)
        {
            Assert.Throws<FormatException>(() => new LiteralGraph(relationships, false));
        }

        [Test]
        public void Ctor_IgnoresLastComma_InRelationships()
        {
            Assert.DoesNotThrow(() => new LiteralGraph("A-B,", true));
        }

        [Test]
        public void Ctor_IgnoresWhitespaces_InRelationships()
        {
            Assert.DoesNotThrow(() => new LiteralGraph("  A -     B   ,      ", true));
        }

        [Test]
        public void Ctor_AcceptsASingleUnconnectedVertex()
        {
            Assert.DoesNotThrow(() => new LiteralGraph("A", true));
        }

        [Test]
        public void Ctor_AcceptsUnconnectedVertices()
        {
            var graph = new LiteralGraph("A,B,C-D", true);

            TestHelper.AssertSequence(graph.GetEdges('A'));
            TestHelper.AssertSequence(graph.GetEdges('B'));
        }

        [Test]
        public void Ctor_AcceptsEmptyRelationships()
        {
            var graph = new LiteralGraph("", false);
            TestHelper.AssertSequence(graph);
        }

        [Test]
        public void Ctor_AcceptsLettersAndDigits()
        {
            Assert.DoesNotThrow(() => new LiteralGraph("a-B,B-0", true));
        }

        [Test]
        public void IsDirected_IsTrue_IfTrueSpecifiedAtConstruction()
        {
            var graph = new LiteralGraph("A", true);
            Assert.IsTrue(graph.IsDirected);
        }

        [Test]
        public void IsDirected_IsFalse_IfFalseSpecifiedAtConstruction()
        {
            var graph = new LiteralGraph("A", false);
            Assert.IsFalse(graph.IsDirected);
        }

        [Test]
        public void IsReadOnly_ReturnsTrue()
        {
            var graph = new LiteralGraph("A,B", false);

            Assert.IsTrue(graph.IsReadOnly);
        }

        [Test]
        public void Enumeration_ReturnsAllVertices()
        {
            var graph = new LiteralGraph("A>B,B-Z,K<T", true);

            var v = graph.ToArray();
            v.QuickSort(0, v.Length, Comparer<char>.Default);

            TestHelper.AssertSequence(v,
                'A','B','K','T','Z');
        }

        [TestCase('A', "A>B")]
        [TestCase('B', "B>Z,B>T,B>A")]
        [TestCase('K', "")]
        [TestCase('T', "T>K,T>A")]
        [TestCase('Z', "Z>B")]
        public void GetEdges_ReturnsAllEdges(char vertex, string expected)
        {
            var graph = new LiteralGraph("A>B,B-Z,K<T,B>T,B>A,T>A", true);

            var v = graph.GetEdges(vertex).Select(s => s.FromVertex + ">" + s.ToVertex).ToArray();
            var result = string.Join(",", v);

            Assert.AreEqual(expected, result);
        }
    }
}
