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

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
namespace Abacaxi.Tests.Sequence
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public class SequenceInterleaveTests
    {
        [Test]
        public void Interleave_ThrowsException_WhenComparerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                Abacaxi.Sequence.Interleave(null, new int[] { }).ToArray());
        }

        [Test]
        public void Interleave_ThrowsException_WhenSequenceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                Abacaxi.Sequence.Interleave(Comparer<int>.Default, null, new int[] { }).ToArray());
        }

        [Test]
        public void Interleave_ThrowsException_WhenParamsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                Abacaxi.Sequence.Interleave(Comparer<int>.Default, new int[] { }, null).ToArray());
        }

        [Test]
        public void Interleave_ThrowsException_WhenStreamIsUnsorted()
        {
            Assert.Throws<InvalidOperationException>(() =>
                Abacaxi.Sequence.Interleave(Comparer<int>.Default, new[] { 10, 11 }).ToArray());
        }

        [Test]
        public void Interleave_ReturnsOriginalSequence_IfOnlyOne()
        {
            TestHelper.AssertSequence(Abacaxi.Sequence.Interleave(Comparer<int>.Default, new[] { 10, 9, 8, 7 }),
                10, 9, 8, 7);
        }

        [Test]
        public void Interleave_ReturnsNothingIfSequencesAreEmpty()
        {
            TestHelper.AssertSequence(Abacaxi.Sequence.Interleave(Comparer<int>.Default, new int[] { }, new int[] { }, new int[] { }));
        }

        [Test]
        public void Interleave_InterleavesThreeStreams()
        {
            TestHelper.AssertSequence(
                Abacaxi.Sequence.Interleave(Comparer<int>.Default, new[] { 20, 17 }, new[] { 19, 16, 14 }, new[] { 18, 15, 13, 12 }),
                20, 19, 18, 17, 16, 15, 14, 13, 12);
        }
    }
}