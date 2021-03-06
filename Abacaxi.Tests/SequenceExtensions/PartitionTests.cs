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

namespace Abacaxi.Tests.SequenceExtensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using NUnit.Framework;

    [TestFixture]
    public sealed class PartitionTests
    {
        [Test]
        public void Partition_ReturnsFullArraysAndASpill_IfOneExists()
        {
            var actual = "Alex".Partition(3);

            TestHelper.AssertSequence(actual,
                new[] {'A', 'l', 'e'},
                new[] {'x'});
        }

        [Test]
        public void Partition_ReturnsNothing_ForEmptySequence()
        {
            var actual = new string[] { }.Partition(1);

            TestHelper.AssertSequence(actual);
        }

        [Test]
        public void Partition_ReturnsSingleArray_ForSizeOfOne()
        {
            var actual = "Alex".Partition(1);

            TestHelper.AssertSequence(actual,
                new[] {'A'},
                new[] {'l'},
                new[] {'e'},
                new[] {'x'});
        }

        [Test, SuppressMessage("ReSharper", "IteratorMethodResultIsIgnored")]
        public void Partition_ThrowsException_ForSizeLessThanOne()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new[] {1}.Partition(0));
        }
    }
}