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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using NUnit.Framework;

    [TestFixture]
    public sealed class IsStrictlyOrderedDescendingDescendingTests
    {
        [Test]
        public void IsStrictlyOrderedDescending1_ReturnsFalse_ForEqualElements()
        {
            var result = new[] {1, 1}.IsStrictlyOrderedDescending(Comparer<int>.Default);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsStrictlyOrderedDescending1_ReturnsFalse_IfThreeElementsAreNotOrderedDescending()
        {
            var result = new[] {1, 2, 3}.IsStrictlyOrderedDescending(Comparer<int>.Default);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsStrictlyOrderedDescending1_ReturnsFalse_IfTwoElementsAreNotOrderedDescending()
        {
            var result = new[] {1, 2}.IsStrictlyOrderedDescending(Comparer<int>.Default);

            Assert.IsFalse(result);
        }


        [Test]
        public void IsStrictlyOrderedDescending1_ReturnsTrue_ForEmptyCollection()
        {
            var isOrdered = new string[] { }.IsStrictlyOrderedDescending(StringComparer.Ordinal);
            Assert.IsTrue(isOrdered);
        }


        [Test]
        public void IsStrictlyOrderedDescending1_ReturnsTrue_ForOneElement()
        {
            var isOrdered = new[] {"A"}.IsStrictlyOrderedDescending(StringComparer.Ordinal);
            Assert.IsTrue(isOrdered);
        }

        [Test]
        public void IsStrictlyOrderedDescending1_ReturnsTrue_IfThreeElementsAreOrderedDescending()
        {
            var result = new[] {8, 5, 3}.IsStrictlyOrderedDescending(Comparer<int>.Default);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsStrictlyOrderedDescending1_ReturnsTrue_IfTwoElementsAreOrderedDescending()
        {
            var result = new[] {2, 1}.IsStrictlyOrderedDescending(Comparer<int>.Default);

            Assert.IsTrue(result);
        }


        [Test]
        public void IsStrictlyOrderedDescending1_TakesComparer_IntoAccount()
        {
            var result = new[] {"A", "a"}.IsStrictlyOrderedDescending(StringComparer.OrdinalIgnoreCase);
            Assert.IsFalse(result);
        }

        [Test, SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void IsStrictlyOrderedDescending1_ThrowsException_ForNullComparer()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new[] {"bb", "ccc", "a", "z"}.IsStrictlyOrderedDescending(null));
        }

        [Test, SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void IsStrictlyOrderedDescending1_ThrowsException_ForNullSequence()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ((string[]) null).IsStrictlyOrderedDescending(StringComparer.Ordinal));
        }

        [Test]
        public void IsStrictlyOrderedDescending2_ReturnsFalse_ForEqualElements()
        {
            var result = new[] {1, 1}.IsStrictlyOrderedDescending();

            Assert.IsFalse(result);
        }

        [Test]
        public void IsStrictlyOrderedDescending2_ReturnsFalse_IfThreeElementsAreNotOrderedDescending()
        {
            var result = new[] {1, 3, 2}.IsStrictlyOrderedDescending();

            Assert.IsFalse(result);
        }

        [Test]
        public void IsStrictlyOrderedDescending2_ReturnsFalse_IfTwoElementsAreNotOrderedDescending()
        {
            var result = new[] {1, 2}.IsStrictlyOrderedDescending();

            Assert.IsFalse(result);
        }

        [Test]
        public void IsStrictlyOrderedDescending2_ReturnsTrue_ForEmptyCollection()
        {
            var isOrdered = new string[] { }.IsStrictlyOrderedDescending();
            Assert.IsTrue(isOrdered);
        }

        [Test]
        public void IsStrictlyOrderedDescending2_ReturnsTrue_ForOneElement()
        {
            var isOrdered = new[] {"A"}.IsStrictlyOrderedDescending();
            Assert.IsTrue(isOrdered);
        }

        [Test]
        public void IsStrictlyOrderedDescending2_ReturnsTrue_IfThreeElementsAreOrderedDescending()
        {
            var result = new[] {8, 5, 3}.IsStrictlyOrderedDescending();

            Assert.IsTrue(result);
        }

        [Test]
        public void IsStrictlyOrderedDescending2_ReturnsTrue_IfTwoElementsAreOrderedDescending()
        {
            var result = new[] {2, 1}.IsStrictlyOrderedDescending();

            Assert.IsTrue(result);
        }

        [Test, SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void IsStrictlyOrderedDescending2_ThrowsException_ForNullSequence()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ((string[]) null).IsStrictlyOrderedDescending());
        }
    }
}