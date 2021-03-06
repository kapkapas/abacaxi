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

namespace Abacaxi.Tests.ZArray
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using JetBrains.Annotations;
    using NUnit.Framework;
    using ZArray = Abacaxi.ZArray;

    [TestFixture]
    public sealed class ConstructTests
    {
        [TestCase("0123456789", 0, 10), TestCase("0", 0, 1), TestCase("abc", 1, 2)]
        public void Construct_ReturnsAnArrayOfTheSameLengthAsInput([NotNull] string s, int start, int length)
        {
            var sequence = s.AsList();
            var z = ZArray.Construct(sequence, start, length, EqualityComparer<char>.Default);

            Assert.AreEqual(z.Length, length);
        }

        [TestCase("0123456789", 0, 10), TestCase("0", 0, 1), TestCase("abc", 1, 2)]
        public void Construct_ReturnsTheLengthOfArrayAsElementZero([NotNull] string s, int start, int length)
        {
            var sequence = s.AsList();
            var z = ZArray.Construct(sequence, start, length, EqualityComparer<char>.Default);

            Assert.AreEqual(length, z[0]);
        }

        [Test]
        public void Construct_DoesNothing_ForEmptyArray()
        {
            var array = new int[] { };
            ZArray.Construct(array, 0, 0, EqualityComparer<int>.Default);

            Assert.AreEqual(new int[] { }, array);
        }

        [Test]
        public void Construct_DoesNothing_ForZeroLength()
        {
            var array = new[] {1, 2};
            ZArray.Construct(array, 1, 0, EqualityComparer<int>.Default);

            Assert.AreEqual(new[] {1, 2}, array);
        }

        [Test]
        public void Construct_ReturnsADecreasingSequence_ForArrayOfSameChars()
        {
            // ReSharper disable once StringLiteralTypo
            var sequence = "aaaaaaaa".AsList();
            var z = ZArray.Construct(sequence, 0, sequence.Count, EqualityComparer<char>.Default);

            TestHelper.AssertSequence(z,
                8, 7, 6, 5, 4, 3, 2, 1);
        }

        [Test]
        public void Construct_ReturnsAWellFormedZArray_WhenIndexAndLengthAreNotTouched()
        {
            // ReSharper disable once StringLiteralTypo
            var sequence = "abcaababc".AsList();
            var z = ZArray.Construct(sequence, 0, sequence.Count, EqualityComparer<char>.Default);

            TestHelper.AssertSequence(z,
                9, 0, 0, 1, 2, 0, 3, 0, 0);
        }

        [Test]
        public void Construct_ReturnsAWellFormedZArray_WhenIndexIsTouched()
        {
            // ReSharper disable once StringLiteralTypo
            var sequence = "abcaababc".AsList();
            var z = ZArray.Construct(sequence, 1, sequence.Count - 1, EqualityComparer<char>.Default);

            TestHelper.AssertSequence(z,
                8, 0, 0, 0, 1, 0, 2, 0);
        }

        [Test]
        public void Construct_ReturnsAWellFormedZArray_WhenLengthIsTouched()
        {
            // ReSharper disable once StringLiteralTypo
            var sequence = "abcaababc".AsList();
            var z = ZArray.Construct(sequence, 0, sequence.Count - 1, EqualityComparer<char>.Default);

            TestHelper.AssertSequence(z,
                8, 0, 0, 1, 2, 0, 2, 0);
        }

        [Test]
        public void Construct_ThrowsException_ForNegativeLength()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                ZArray.Construct(new[] {1}, 0, -1, EqualityComparer<int>.Default));
        }

        [Test]
        public void Construct_ThrowsException_ForNegativeStartIndex()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                ZArray.Construct(new[] {1}, -1, 1, EqualityComparer<int>.Default));
        }

        [Test, SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void Construct_ThrowsException_ForNullArray()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ZArray.Construct(null, 1, 1, EqualityComparer<int>.Default));
        }

        [Test, SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void Construct_ThrowsException_ForNullComparer()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ZArray.Construct(new[] {1}, 0, 1, null));
        }

        [Test]
        public void Construct_ThrowsException_ForOutOfBounds1()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                ZArray.Construct(new[] {1}, 0, 2, EqualityComparer<int>.Default));
        }

        [Test]
        public void Construct_ThrowsException_ForOutOfBounds2()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                ZArray.Construct(new[] {1}, 1, 1, EqualityComparer<int>.Default));
        }
    }
}