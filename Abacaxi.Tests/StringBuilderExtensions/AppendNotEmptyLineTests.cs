﻿/* Copyright 2017-2018 by Alexandru Ciobanu (alex+git@ciobanu.org)
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

namespace Abacaxi.Tests.StringBuilderExtensions
{
    using System;
    using NUnit.Framework;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    [TestFixture]
    public sealed class AppendNotEmptyLineTests
    {
        [Test, SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void AppendNotEmptyLine_ThrowsException_IfStringBuilderIsNull1()
        {
            Assert.Throws<ArgumentNullException>(() => ((StringBuilder)null).AppendNotEmptyLine(null));
        }

        [Test]
        public void AppendNotEmptyLine_DoesNotAppendTheValueIfItIsNull()
        {
            var result = new StringBuilder("-->").AppendNotEmptyLine(null).Append("<--").ToString();
            Assert.AreEqual("--><--", result);
        }

        [Test]
        public void AppendNotEmptyLine_DoesNotAppendTheValueIfItIsEmpty()
        {
            var result = new StringBuilder("-->").AppendNotEmptyLine(string.Empty).Append("<--").ToString();
            Assert.AreEqual("--><--", result);
        }

        [Test]
        public void AppendNotEmptyLine_AppendsTheValueIfItIsNotEmpty()
        {
            var result = new StringBuilder("-->").AppendNotEmptyLine("Hello").Append("<--").ToString();
            Assert.AreEqual("-->Hello\r\n<--", result);
        }
    }
}