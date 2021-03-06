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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Internal;
    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    ///     Class implements the binary search tree and serves as a base class for other balanced search trees.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    [PublicAPI]
    public class BinarySearchTree<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>
    {
        private int _ver;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BinarySearchTree{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="comparer">The key comparer used.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="comparer" /> is <c>null</c>.</exception>
        public BinarySearchTree([NotNull] IComparer<TKey> comparer)
        {
            Validate.ArgumentNotNull(nameof(comparer), comparer);

            Comparer = comparer;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Abacaxi.Trees.BinarySearchTree`2" /> class using the default
        ///     <typeparamref name="TKey" /> comparer.
        /// </summary>
        public BinarySearchTree() : this(Comparer<TKey>.Default)
        {
        }

        /// <summary>
        ///     Gets the comparer used for ordering tree nodes' keys.
        /// </summary>
        /// <value>
        ///     The comparer.
        /// </value>
        [NotNull]
        protected IComparer<TKey> Comparer { get; }

        /// <summary>
        ///     Gets or sets the root tree node.
        /// </summary>
        /// <value>
        ///     The root tree node.
        /// </value>
        [CanBeNull]
        protected BinaryTreeNode<TKey, TValue> Root { get; set; }

        /// <summary>
        ///     Gets or sets the value of a node identified by <paramref name="key" />.
        /// </summary>
        /// <value>
        ///     The value of the node.
        /// </value>
        /// <param name="key">The key of the node.</param>
        /// <returns>The value of the node identified by the <paramref name="key" />.</returns>
        /// <exception cref="ArgumentException">
        ///     Thrown if the tree does not contain any node identified by the
        ///     <paramref name="key" />.
        /// </exception>
        [CanBeNull]
        public TValue this[TKey key]
        {
            get
            {
                if (!TryGetValue(key, out var value))
                {
                    ThrowKeyNotFound(nameof(key));
                }

                return value;
            }
            set => AddOrUpdate(key, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Adds the specified key/value node to the tree.
        /// </summary>
        /// <param name="item">The node's key/value pair.</param>
        /// <exception cref="T:System.ArgumentException">Thrown if a node with the same key is already present in the tree.</exception>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Removes the node identified by the key and value of the given <paramref name="item" />.
        /// </summary>
        /// <remarks>
        ///     This method is provided for compatibility with <see cref="T:System.Collections.Generic.ICollection`1" />. It is not
        ///     recommended for normal use.
        ///     The values of nodes are compared using the default equality comparer for that type.
        /// </remarks>
        /// <param name="item">The key/value pair to remove from the tree.</param>
        /// <returns>
        ///     <c>true</c> if the node was successfully removed; otherwise, false.
        /// </returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return TryGetValue(item.Key, out var value) &&
                   EqualityComparer<TValue>.Default.Equals(value,
                       item.Value) &&
                   Remove(item.Key);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Determines whether the tree contains the given key/value node.
        /// </summary>
        /// <param name="item">The key/value pair to search for.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="item" /> is found in the tree; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        ///     This method is provided for compatibility with <see cref="T:System.Collections.Generic.ICollection`1" />. It is not
        ///     recommended for normal use.
        ///     The values of nodes are compared using the default equality comparer for that type.
        /// </remarks>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return TryGetValue(item.Key, out var value) &&
                   EqualityComparer<TValue>.Default.Equals(value,
                       item.Value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Clears this tree.
        /// </summary>
        public void Clear()
        {
            NotifyTreeChanged(0);

            Root = null;
            Count = 0;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Copies the elements of the tree to an <see cref="T:System.Array" />, starting at a particular
        ///     <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied
        ///     from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have
        ///     zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Validate.ArgumentNotNull(nameof(array), array);
            Validate.ArgumentGreaterThanOrEqualToZero(nameof(arrayIndex), arrayIndex);
            Validate.ArgumentLessThanOrEqualTo(nameof(arrayIndex), Count, array.Length - arrayIndex);

            using (var enumerator = GetEnumerator())
            {
                var index = arrayIndex;
                while (enumerator.MoveNext())
                {
                    array[index++] = enumerator.Current;
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the count of nodes in this tree.
        /// </summary>
        /// <value>
        ///     The total count of nodes.
        /// </value>
        public int Count { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets a value indicating whether the tree is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        ///     Returns an enumerator that iterates through the tree in-order.
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the tree.
        /// </returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return GetEnumerator(TreeTraversalMode.InOrder);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Returns an enumerator that iterates through the tree in-order.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the tree.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Throws the standard "key not found" exception.
        /// </summary>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="ArgumentException">The tree does not contain a node with the given key.</exception>
        [ContractAnnotation("=> halt")]
        protected static void ThrowKeyNotFound([NotNull] string argumentName)
        {
            Assert.Condition(!string.IsNullOrEmpty(argumentName));
            throw new ArgumentException("The tree does not contain a node with the given key.", argumentName);
        }

        /// <summary>
        ///     Throws the standard "duplicate key found" exception.
        /// </summary>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="ArgumentException">The tree already contains a node with the given key.</exception>
        [ContractAnnotation("=> halt")]
        protected static void ThrowDuplicateKeyFound([NotNull] string argumentName)
        {
            Assert.Condition(!string.IsNullOrEmpty(argumentName));
            throw new ArgumentException("The tree already contains a node with the given key.", argumentName);
        }

        private void CheckVersion(int version)
        {
            if (version != _ver)
            {
                throw new InvalidOperationException("The tree has been modified during the enumeration process.");
            }
        }

        [NotNull]
        private BinaryTreeNode<TKey, TValue> InsertRecursive(
            [CanBeNull] BinaryTreeNode<TKey, TValue> root,
            [CanBeNull] TKey key,
            [CanBeNull] TValue value,
            bool allowUpdate)
        {
            if (root == null)
            {
                NotifyTreeChanged(1);
                return new BinaryTreeNode<TKey, TValue>
                {
                    Key = key,
                    Value = value
                };
            }

            var comparison = Comparer.Compare(key, root.Key);

            if (comparison < 0)
            {
                root.LeftChild = InsertRecursive(root.LeftChild, key, value, allowUpdate);
            }
            else if (comparison > 0)
            {
                root.RightChild = InsertRecursive(root.RightChild, key, value, allowUpdate);
            }
            else if (allowUpdate)
            {
                NotifyTreeChanged(0);
                root.Value = value;
            }
            else
            {
                ThrowDuplicateKeyFound(nameof(key));
            }

            return root;
        }

        [CanBeNull]
        private BinaryTreeNode<TKey, TValue> DeleteNodeRecursive(
            [CanBeNull] BinaryTreeNode<TKey, TValue> root, [CanBeNull] TKey key, ref bool deleted)
        {
            if (root == null)
            {
                return null;
            }

            var comparison = Comparer.Compare(key, root.Key);
            if (comparison < 0)
            {
                root.LeftChild = DeleteNodeRecursive(root.LeftChild, key, ref deleted);
            }
            else if (comparison > 0)
            {
                root.RightChild = DeleteNodeRecursive(root.RightChild, key, ref deleted);
            }
            else
            {
                if (!deleted)
                {
                    NotifyTreeChanged(-1);
                    deleted = true;
                }

                if (root.LeftChild == null)
                {
                    return root.RightChild;
                }

                if (root.RightChild == null)
                {
                    return root.LeftChild;
                }

                var successor = root.RightChild;
                while (successor.LeftChild != null)
                {
                    successor = successor.LeftChild;
                }

                root.Key = successor.Key;
                root.Value = successor.Value;

                root.RightChild = DeleteNodeRecursive(root.RightChild, successor.Key, ref deleted);
            }

            return root;
        }

        [NotNull]
        private IEnumerator<KeyValuePair<TKey, TValue>> GetInOrderEnumerator(
            [CanBeNull] BinaryTreeNode<TKey, TValue> current)
        {
            var stack = new Stack<BinaryTreeNode<TKey, TValue>>();
            var ver = _ver;
            do
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.LeftChild;
                }

                if (stack.Count <= 0)
                {
                    continue;
                }

                current = stack.Pop();
                CheckVersion(ver);

                yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);

                current = current.RightChild;
            } while (stack.Count > 0 ||
                     current != null);

            CheckVersion(ver);
        }

        [NotNull]
        private IEnumerator<KeyValuePair<TKey, TValue>> GetPreOrderEnumerator(
            [CanBeNull] BinaryTreeNode<TKey, TValue> current)
        {
            var stack = new Stack<BinaryTreeNode<TKey, TValue>>();
            var ver = _ver;
            do
            {
                while (current != null)
                {
                    stack.Push(current);
                    CheckVersion(ver);

                    yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);

                    current = current.LeftChild;
                }

                if (stack.Count <= 0)
                {
                    continue;
                }

                current = stack.Pop();
                current = current.RightChild;
            } while (stack.Count > 0 ||
                     current != null);

            CheckVersion(ver);
        }

        [NotNull]
        private IEnumerator<KeyValuePair<TKey, TValue>> GetPostOrderEnumerator(
            [CanBeNull] BinaryTreeNode<TKey, TValue> current)
        {
            var stack = new Stack<BinaryTreeNode<TKey, TValue>>();
            var ver = _ver;
            BinaryTreeNode<TKey, TValue> previous = null;
            do
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.LeftChild;
                }

                while (current == null &&
                       stack.Count > 0)
                {
                    current = stack.Peek();
                    if (current.RightChild == null ||
                        current.RightChild == previous)
                    {
                        CheckVersion(ver);

                        yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);

                        stack.Pop();
                        previous = current;
                        current = null;
                    }
                    else
                    {
                        current = current.RightChild;
                    }
                }
            } while (stack.Count > 0);

            CheckVersion(ver);
        }

        /// <summary>
        ///     Increases the version of the tree. The version must be increased on each modification.
        /// </summary>
        protected void NotifyTreeChanged(int countDelta)
        {
            Count += countDelta;
            _ver++;
        }

        /// <summary>
        ///     Looks up the node key the given <paramref name="key" />.
        /// </summary>
        /// <param name="key">The key of the node.</param>
        /// <returns>The node, if found; otherwise, <c>null</c>.</returns>
        [CanBeNull]
        public BinaryTreeNode<TKey, TValue> LookupNode([CanBeNull] TKey key)
        {
            var node = Root;
            while (node != null)
            {
                var comparison = Comparer.Compare(key, node.Key);
                if (comparison < 0)
                {
                    node = node.LeftChild;
                }
                else if (comparison > 0)
                {
                    node = node.RightChild;
                }
                else
                {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        ///     Adds the specified key/value node to the tree.
        /// </summary>
        /// <param name="key">The node's key.</param>
        /// <param name="value">The node's value.</param>
        /// <exception cref="ArgumentException">
        ///     Thrown if a node with the same <paramref name="key" /> is already present in the
        ///     tree.
        /// </exception>
        public virtual void Add([CanBeNull] TKey key, [CanBeNull] TValue value)
        {
            Root = InsertRecursive(Root, key, value, false);
        }

        /// <summary>
        ///     Updated the value of a node having a given key.
        /// </summary>
        /// <param name="key">The node's key.</param>
        /// <param name="value">The node's new value.</param>
        /// <exception cref="ArgumentException">Thrown if no node found with the given <paramref name="key" />.</exception>
        public void Update([CanBeNull] TKey key, [CanBeNull] TValue value)
        {
            var node = LookupNode(key);
            if (node == null)
            {
                ThrowKeyNotFound(nameof(key));
            }

            NotifyTreeChanged(0);
            node.Value = value;
        }

        /// <summary>
        ///     Adds or updates a tree node that has a given key and value.
        /// </summary>
        /// <param name="key">The node's key.</param>
        /// <param name="value">The node's new value.</param>
        public virtual void AddOrUpdate([CanBeNull] TKey key, [CanBeNull] TValue value)
        {
            Root = InsertRecursive(Root, key, value, true);
        }

        /// <summary>
        ///     Removes the node from the tree that has a specified key.
        /// </summary>
        /// <param name="key">The node's key.</param>
        /// <returns><c>true</c> if the node was removed; otherwise, <c>false</c>.</returns>
        public virtual bool Remove([CanBeNull] TKey key)
        {
            var deleted = false;
            Root = DeleteNodeRecursive(Root, key, ref deleted);

            return deleted;
        }

        /// <summary>
        ///     Tries the get value of the node identified by the given <paramref name="key" />.
        /// </summary>
        /// <param name="key">The key of the node.</param>
        /// <param name="value">The value of the node (if found).</param>
        /// <returns><c>true</c> if the node was found; otherwise, <c>false</c>.</returns>
        public bool TryGetValue([CanBeNull] TKey key, [CanBeNull] out TValue value)
        {
            var node = LookupNode(key);
            if (node != null)
            {
                value = node.Value;
                return true;
            }

            value = default(TValue);
            return false;
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the tree.
        /// </summary>
        /// <param name="mode">The traversal mode.</param>
        /// <returns>
        ///     An enumerator that can be used to iterate through the tree.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="mode" /> is invalid.</exception>
        [NotNull]
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator(TreeTraversalMode mode)
        {
            switch (mode)
            {
                case TreeTraversalMode.InOrder:
                    return GetInOrderEnumerator(Root);
                case TreeTraversalMode.PreOrder:
                    return GetPreOrderEnumerator(Root);
                case TreeTraversalMode.PostOrder:
                    return GetPostOrderEnumerator(Root);
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "Invalid enum value.");
            }
        }
    }
}