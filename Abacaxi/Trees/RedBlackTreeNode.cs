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

namespace Abacaxi.Trees
{
    using Internal;
    using JetBrains.Annotations;

    /// <summary>
    ///     Class represents a node in a red-black balanced search tree.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    [PublicAPI]
    public sealed class RedBlackTreeNode<TKey, TValue> : BinaryTreeNode<TKey, TValue>
    {
        /// <summary>
        ///     Gets the right child node.
        /// </summary>
        /// <value>
        ///     The right child node.
        /// </value>
        [CanBeNull]
        public new RedBlackTreeNode<TKey, TValue> RightChild
        {
            get
            {
                Assert.Condition(base.RightChild == null || base.RightChild is RedBlackTreeNode<TKey, TValue>);
                return (RedBlackTreeNode<TKey, TValue>) base.RightChild;
            }
            set => base.RightChild = value;
        }

        /// <summary>
        ///     Gets the left child node.
        /// </summary>
        /// <value>
        ///     The left child node.
        /// </value>
        [CanBeNull]
        public new RedBlackTreeNode<TKey, TValue> LeftChild
        {
            get
            {
                Assert.Condition(base.LeftChild == null || base.LeftChild is RedBlackTreeNode<TKey, TValue>);
                return (RedBlackTreeNode<TKey, TValue>) base.LeftChild;
            }
            set => base.LeftChild = value;
        }

        /// <summary>
        ///     Gets or sets the color of this node.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        public RedBlackTreeNodeColor Color { get; set; }
    }
}