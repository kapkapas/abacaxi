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
    using System.Linq;
    using Abacaxi.Graphs;
    using NUnit.Framework;

    [TestFixture]
    public class MatrixGraphTests
    {
        private int[,] M = new int[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 },
        };

        private void CheckConnections(CellCoordinates from, params CellCoordinates[] to)
        {
            var matrix = new MatrixGraph<int>(M);
            var set = new HashSet<CellCoordinates>();
            foreach (var connection in matrix.GetNodeConnections(from))
            {
                set.Add(connection);
            }

            Assert.AreEqual(to.Length, set.Count);

            foreach (var expected in to)
            {
                Assert.IsTrue(set.Contains(expected));
            }
        }

        [Test]
        public void Ctor_ThrowsException_ForNullMatrix()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new MatrixGraph<int>(null));
        }

        [Test]
        public void GetNodeValue_ThrowsException_ForOutOfBoundsX()
        {
            var array = new MatrixGraph<int>(new int[1, 1] { { 1 } });

            Assert.Throws<ArgumentOutOfRangeException>(() => array.GetNodeValue(new CellCoordinates(1, 0)));
        }

        public void GetNodeValue_ThrowsException_ForOutOfBoundsY()
        {
            var array = new MatrixGraph<int>(new int[1, 1] { { 1 } });

            Assert.Throws<ArgumentOutOfRangeException>(() => array.GetNodeValue(new CellCoordinates(0, 1)));
        }

        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        public void GetNodeValue_Returns_ValidValue(int x, int y)
        {
            var array = new int[2, 2]
            {
                { 1, 2 },
                { 3, 4 }
            };

            var matrix = new MatrixGraph<int>(array);

            Assert.AreEqual(array[x, y], matrix.GetNodeValue(new CellCoordinates(x, y)));
        }

        [Test]
        public void SetNodeValue_ThrowsException_ForOutOfBoundsX()
        {
            var array = new MatrixGraph<int>(new int[1, 1] { { 1 } });

            Assert.Throws<ArgumentOutOfRangeException>(() => array.SetNodeValue(new CellCoordinates(1, 0), 0));
        }

        public void SetNodeValue_ThrowsException_ForOutOfBoundsY()
        {
            var array = new MatrixGraph<int>(new int[1, 1] { { 1 } });

            Assert.Throws<ArgumentOutOfRangeException>(() => array.SetNodeValue(new CellCoordinates(0, 1), 0));
        }

        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        public void SetNodeValue_ModifiesTheArray(int x, int y)
        {
            var array = new int[2, 2]
            {
                { 1, 2 },
                { 3, 4 }
            };

            var matrix = new MatrixGraph<int>(array);
            matrix.SetNodeValue(new CellCoordinates(x, y), 100);

            Assert.AreEqual(100, array[x, y]);
        }

        [Test]
        public void GetNodeConnections_ThrowsException_ForOutOfBoundsX()
        {
            var array = new MatrixGraph<int>(new int[1, 1] { { 1 } });

            Assert.Throws<ArgumentOutOfRangeException>(() => array.GetNodeConnections(new CellCoordinates(1, 0)).ToArray());
        }

        public void GetNodeConnections_ThrowsException_ForOutOfBoundsY()
        {
            var array = new MatrixGraph<int>(new int[1, 1] { { 1 } });

            Assert.Throws<ArgumentOutOfRangeException>(() => array.GetNodeConnections(new CellCoordinates(0, 1)).ToArray());
        }

        [Test]
        public void GetNodeConnections_Returns24_For1()
        {
            CheckConnections(
                new CellCoordinates(0, 0),
                new CellCoordinates(1, 0),
                new CellCoordinates(1, 0));
        }

        [Test]
        public void GetNodeConnections_Returns135_For2()
        {
            CheckConnections(
                new CellCoordinates(1, 0),
                new CellCoordinates(0, 0),
                new CellCoordinates(2, 0),
                new CellCoordinates(1, 1));
        }

        [Test]
        public void GetNodeConnections_Returns26_For3()
        {
            CheckConnections(
                new CellCoordinates(2, 0),
                new CellCoordinates(1, 0),
                new CellCoordinates(2, 1));
        }

        [Test]
        public void GetNodeConnections_Returns157_For4()
        {
            CheckConnections(
                new CellCoordinates(0, 1),
                new CellCoordinates(0, 0),
                new CellCoordinates(1, 1),
                new CellCoordinates(0, 2));
        }

        [Test]
        public void GetNodeConnections_Returns2468_For5()
        {
            CheckConnections(
                new CellCoordinates(1, 1),
                new CellCoordinates(0, 1),
                new CellCoordinates(1, 0),
                new CellCoordinates(2, 1),
                new CellCoordinates(1, 2));
        }

        [Test]
        public void GetNodeConnections_Returns359_For6()
        {
            CheckConnections(
                new CellCoordinates(2, 1),
                new CellCoordinates(2, 0),
                new CellCoordinates(1, 1),
                new CellCoordinates(2, 2));
        }

        [Test]
        public void GetNodeConnections_Returns28_For7()
        {
            CheckConnections(
                new CellCoordinates(0, 2),
                new CellCoordinates(0, 1),
                new CellCoordinates(1, 2));
        }

        [Test]
        public void GetNodeConnections_Returns759_For8()
        {
            CheckConnections(
                new CellCoordinates(1, 2),
                new CellCoordinates(0, 2),
                new CellCoordinates(1, 1),
                new CellCoordinates(2, 2));
        }

        [Test]
        public void GetNodeConnections_Returns86_For9()
        {
            CheckConnections(
                new CellCoordinates(2, 2),
                new CellCoordinates(1, 2),
                new CellCoordinates(2, 1));
        }
    }
}