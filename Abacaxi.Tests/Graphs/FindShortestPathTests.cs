﻿/* Copyright 2017-2019 by Alexandru Ciobanu (alex+git@ciobanu.org)
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
    using System.Diagnostics.CodeAnalysis;
    using Abacaxi.Graphs;
    using JetBrains.Annotations;
    using NUnit.Framework;

    [TestFixture]
    public sealed class FindShortestPathTests
    {
        [TestCase("A-1-B,A-1-C", 'A', 'B', "A,B"), TestCase("A-1-B,B-1-C,C-1-D,D-1-A,B>1>D", 'D', 'B', "D,C,B"),
         TestCase("A-1-B,B-1-C,C-1-D,D-1-A,B>1>D", 'B', 'D', "B,D"),
         TestCase("A>1>B,A>1>C,C<1<F,F-1-E,E-1-D,D>1>B,D>1>C", 'A', 'E', ""),
         TestCase("A>1>B,A>1>C,C<1<F,F-1-E,E-1-D,D>1>B,D>1>C", 'E', 'Z', "")]
        public void FindShortestPath_FillsExpectedVertices(
            [NotNull] string relationships, char startVertex, char endVertex, string expected)
        {
            var graph = new LiteralGraph(relationships, true);
            var seq = graph.FindShortestPath(startVertex, endVertex);

            Assert.AreEqual(expected, string.Join(",", seq));
        }

        [Test, SuppressMessage("ReSharper", "IteratorMethodResultIsIgnored")]
        public void FindShortestPath_ThrowsException_ForInvalidStartVertex()
        {
            var graph = new LiteralGraph("A>1>B", true);
            Assert.Throws<ArgumentException>(() => graph.FindShortestPath('Z', 'A'));
        }
    }
}