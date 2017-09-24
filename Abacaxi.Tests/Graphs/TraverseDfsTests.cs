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

namespace Abacaxi.Tests.Graphs
{
    using System;
    using System.Collections.Generic;
    using Abacaxi.Graphs;
    using NUnit.Framework;
    using System.Diagnostics.CodeAnalysis;

    [TestFixture]
    public class TraverseDfsTests
    {
        private static bool True<T>(Graph<T>.IDfsNode node)
        {
            return true;
        }

        [Test]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void TraverseDfs_ThrowsException_ForNullVisitationHandler()
        {
            var graph = new LiteralGraph("A>1>B", true);

            Assert.Throws<ArgumentNullException>(() => graph.TraverseDfs('A', null, True,  (node, dfsNode) => true));
        }

        [Test]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void TraverseDfs_ThrowsException_ForNullCompletionHandler()
        {
            var graph = new LiteralGraph("A>1>B", true);

            Assert.Throws<ArgumentNullException>(() => graph.TraverseDfs('A', True, null, (node, dfsNode) => true));
        }

        [Test]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void TraverseDfs_ThrowsException_ForNullCycleHandler()
        {
            var graph = new LiteralGraph("A>1>B", true);

            Assert.Throws<ArgumentNullException>(() => graph.TraverseDfs('A', True, True, null));
        }

        [Test]
        public void TraverseDfs_ThrowsException_ForInvalidVertex()
        {
            var graph = new LiteralGraph("A>1>B", true);

            Assert.Throws<ArgumentException>(() => graph.TraverseDfs('Z', True, True, (node, dfsNode) => true));
        }

        [TestCase("A-1-B,A-1-C", "A>B,A>C,>A")]
        [TestCase("A-1-B,C-1-D", "A>B,>A")]
        [TestCase("A-1-B,A-1-D,B-1-C,C-1-D,C-1-E", "C>D,C>E,B>C,A>B,>A")]
        [TestCase("A-1-B,A-1-C,A-1-D,B-1-E,B-1-F,E-1-G", "E>G,B>E,B>F,A>B,A>C,A>D,>A")]
        public void TraverseDfs_ReturnsProperSequence_ForUndirectedGraph(string relationships, string expected)
        {
            var graph = new LiteralGraph(relationships, true);
            var result = new List<string>();

            graph.TraverseDfs('A', True, node =>
            {
                Assert.IsNotNull(node);
                result.Add($"{node.Parent?.Vertex}>{node.Vertex}");
                return true;
            }, (from, to) => true);

            Assert.AreEqual(expected, string.Join(",", result));
        }

        [TestCase("A>1>A", ">A")]
        [TestCase("A>1>B,B>1>C,C>1>A", "B>C,A>B,>A")]
        [TestCase("A>1>B,C>1>A,C>1>B", "A>B,>A")]
        [TestCase("A>1>B,B>1>C,C>1>A,D>1>B,C>1>D", "C>D,B>C,A>B,>A")]
        public void TraverseDfs_ReturnsProperSequence_ForDirectedGraph(string relationships, string expected)
        {
            var graph = new LiteralGraph(relationships, true);
            var result = new List<string>();

            graph.TraverseDfs('A', True, node =>
            {
                Assert.IsNotNull(node);
                result.Add($"{node.Parent?.Vertex}>{node.Vertex}");
                return true;
            }, (from, to) => true);

            Assert.AreEqual(expected, string.Join(",", result));
        }

        [TestCase("A-1-B,A-1-C,A-1-D,B-1-E,B-1-F,E-1-G", 'A', "E>G,B>E,B>F,A>B,A>C,A>D,>A")]
        [TestCase("A-1-B,A-1-C,A-1-D,B-1-E,B-1-F,E-1-G", 'D', "E>G,B>E,B>F,A>B,A>C,A>D,>A")]
        [TestCase("A-1-B,A-1-C,A-1-D,B-1-E,B-1-F,E-1-G", 'E', "E>G,B>E,A>B,>A")]
        public void TraverseDfs_ReturnsProperSequence_IfInterrupted(string relationships, char killVertex, string expected)
        {
            var graph = new LiteralGraph(relationships, true);
            var result = new List<string>();

            graph.TraverseDfs('A', True, node =>
            {
                Assert.IsNotNull(node);
                result.Add($"{node.Parent?.Vertex}>{node.Vertex}");
                return node.Vertex != killVertex;
            }, (from, to) => true);

            Assert.AreEqual(expected, string.Join(",", result));
        }

        [TestCase("A-1-B,A-1-C", "B~A,C~A")]
        [TestCase("A-1-B,C-1-D", "B~A")]
        [TestCase("A-1-B,A-1-D,B-1-C,C-1-D,C-1-E", "B~A,C~B,D~A,D~C,E~C")]
        [TestCase("A-1-B,A-1-C,A-1-D,B-1-E,B-1-F,E-1-G", "B~A,E~B,G~E,F~B,C~A,D~A")]
        [TestCase("A>1>A", "A~A")]
        [TestCase("A>1>B,B>1>C,C>1>A", "C~A")]
        [TestCase("A>1>B,C>1>A,C>1>B", "")]
        [TestCase("A>1>B,B>1>C,C>1>A,D>1>B,C>1>D", "C~A,D~B")]
        [TestCase("A-1-B,B-1-C,C-1-D,D-1-A,B>1>D", "B~A,C~B,D~C,D~A")]
        public void TraverseDfs_ReportsTheCycles_InDirectedGraphs(string relationships, string expected)
        {
            var graph = new LiteralGraph(relationships, true);
            var result = new List<string>();

            graph.TraverseDfs('A', True, True, (from, to) =>
            {
                result.Add($"{from.Vertex}~{to.Vertex}");
                return true;
            });

            Assert.AreEqual(expected, string.Join(",", result));
        }

        [TestCase("A", "")]
        [TestCase("A,B,C", "")]
        [TestCase("A-1-B,A-1-C", "")]
        [TestCase("A-1-B,C-1-D", "")]
        [TestCase("A-1-B,A-1-D,B-1-C,C-1-D,C-1-E", "D~A")]
        [TestCase("A-1-B,A-1-C,A-1-D,B-1-E,B-1-F,E-1-G", "")]
        public void TraverseDfs_ReportsTheCycles_InUndirectedGraphs(string relationships, string expected)
        {
            var graph = new LiteralGraph(relationships, false);
            var result = new List<string>();

            graph.TraverseDfs('A', True, True, (from, to) =>
            {
                result.Add($"{from.Vertex}~{to.Vertex}");
                return true;
            });

            Assert.AreEqual(expected, string.Join(",", result));
        }

        [TestCase("A>1>A", 'A', ">A")]
        [TestCase("A>1>B,B>1>C,C>1>A", 'A', "B>C,A>B,>A")]
        [TestCase("A>1>B,B>1>C,C>1>A,D>1>B,C>1>D", 'A', "B>C,A>B,>A")]
        [TestCase("A-1-B,B-1-C,C-1-D,D-1-A,B>1>D", 'B', "B>C,A>B,>A")]
        public void TraverseDfs_InterruptsOnCycle(string relationships, char killVertex, string expected)
        {
            var graph = new LiteralGraph(relationships, true);
            var result = new List<string>();

            graph.TraverseDfs('A', True, node =>
            {
                Assert.IsNotNull(node);
                result.Add($"{node.Parent?.Vertex}>{node.Vertex}");
                return true;
            }, 
            (from, to) => to.Vertex != killVertex);

            Assert.AreEqual(expected, string.Join(",", result));
        }


        [TestCase("A-1-B,B-1-D,D-1-F,F-1-A,F-1-Z,A-1-C,C-1-E,E-1-A,E-1-G,G-1-H,H-1-E", "Z4,F3,D2,B1,H12,G11,E10,C9,A0")]
        public void TraverseDfs_MarksNodesWithCorrectEntryTimes(string relationships, string expected)
        {
            var graph = new LiteralGraph(relationships, true);
            var result = new List<string>();

            graph.TraverseDfs('A', True, node =>
            {
                result.Add($"{node.Vertex}{node.EntryTime}");
                return true;
            },
            (from, to) => true);

            Assert.AreEqual(expected, string.Join(",", result));
        }

        [TestCase("A-1-B,B-1-D,D-1-F,F-1-A,F-1-Z,A-1-C,C-1-E,E-1-A,E-1-G,G-1-H,H-1-E", "Z5,F6,D7,B8,H13,G14,E15,C16,A17")]
        public void TraverseDfs_MarksNodesWithCorrectExitTimes(string relationships, string expected)
        {
            var graph = new LiteralGraph(relationships, true);
            var result = new List<string>();

            graph.TraverseDfs('A', True, node =>
            {
                result.Add($"{node.Vertex}{node.ExitTime}");
                return true;
            },
            (from, to) => true);

            Assert.AreEqual(expected, string.Join(",", result));
        }
    }
}