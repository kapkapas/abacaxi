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

namespace Abacaxi.Threading
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;
    using Internal;
    using JetBrains.Annotations;

    /// <summary>
    ///     Container that caches objects for a specified duration of time. This cache implementation is very simple
    ///     and avoid threading at all costs.
    /// </summary>
    /// <typeparam name="TKey">The type of the key used to index the values.</typeparam>
    /// <typeparam name="TValue">The type of the value that is stored.</typeparam>
    [PublicAPI]
    public sealed class NanoCache<TKey, TValue>
    {
        [NotNull] private ConcurrentDictionary<TKey, Tuple<TValue, long>> _dictionary;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NanoCache{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="equalityComparer">The equality comparer used for th item's keys.</param>
        public NanoCache([NotNull] IEqualityComparer<TKey> equalityComparer)
        {
            Validate.ArgumentNotNull(nameof(equalityComparer), equalityComparer);

            _dictionary = new ConcurrentDictionary<TKey, Tuple<TValue, long>>(equalityComparer);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="NanoCache{TKey, TValue}" /> class.
        /// </summary>
        public NanoCache() : this(EqualityComparer<TKey>.Default)
        {
        }

        /// <summary>
        ///     Caches an item of gets the cached value of an item using its key.
        /// </summary>
        /// <value>
        ///     The value stored in the cache.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns>
        ///     The value stored in the cache or the default value if nothing is cached.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="key" /> is <c>null</c>.</exception>
        [CanBeNull]
        public TValue this[[NotNull] TKey key]
        {
            get => TryGetValue(key, out var result) ? result : default(TValue);
            set => Set(key, value);
        }

        /// <summary>
        ///     Gets the count of items stored in the cache (expired included).
        /// </summary>
        /// <value>
        ///     The count of items in the cache.
        /// </value>
        public int Count => _dictionary.Count;

        /// <summary>
        ///     Tries the get the value of a cached item.
        /// </summary>
        /// <param name="key">The key of the item.</param>
        /// <param name="value">The value of the item, if was cached.</param>
        /// <returns><c>true</c> if the item is found; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="key" /> is null.</exception>
        public bool TryGetValue([NotNull] TKey key, [CanBeNull] out TValue value)
        {
            Validate.ArgumentNotNull(nameof(key), key);

            value = default(TValue);
            if (!_dictionary.TryGetValue(key, out var tuple))
            {
                return false;
            }

            if (tuple.Item2 < DateTime.UtcNow.Ticks)
            {
                if (_dictionary.TryRemove(key, out var removed) &&
                    !ReferenceEquals(tuple, removed))
                {
                    _dictionary.TryAdd(key, removed);
                }

                return false;
            }

            value = tuple.Item1;
            return true;
        }

        /// <summary>
        ///     Removes the specified item from the cache.
        /// </summary>
        /// <param name="key">The key representing the item to remove.</param>
        /// <returns><c>true</c> if the item was removed; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="key" /> is <c>null</c>.</exception>
        public bool Remove([NotNull] TKey key)
        {
            Validate.ArgumentNotNull(nameof(key), key);
            return _dictionary.TryRemove(key, out var removed) && removed.Item2 >= DateTime.UtcNow.Ticks;
        }

        /// <summary>
        ///     Caches a given item using an optional <paramref name="ttl" />
        /// </summary>
        /// <param name="key">The key of the item.</param>
        /// <param name="value">The value to cache.</param>
        /// <param name="ttl">The TTL of the value.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="key" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="ttl" /> is less than <c>-1</c>.</exception>
        public void Set([NotNull] TKey key, [CanBeNull] TValue value, int ttl = Timeout.Infinite)
        {
            Validate.ArgumentNotNull(nameof(key), key);

            if (Equals(value, default(TValue)))
            {
                _dictionary.TryRemove(key, out _);
            }
            else
            {
                Validate.ArgumentGreaterThanOrEqualTo(nameof(ttl), ttl, Timeout.Infinite);

                var nextExpiry = ttl != Timeout.Infinite
                    ? DateTime.UtcNow.AddMilliseconds(ttl).Ticks
                    : long.MaxValue;

                _dictionary[key] = Tuple.Create(value, nextExpiry);
            }
        }

        /// <summary>
        ///     Flushes all expired items from this cache instance.
        /// </summary>
        public void Flush()
        {
            var snapshot = _dictionary.ToArray();
            var now = DateTime.UtcNow.Ticks;
            foreach (var item in snapshot)
            {
                if (item.Value.Item2 < now)
                {
                    if (_dictionary.TryRemove(item.Key, out var removed) &&
                        !ReferenceEquals(item.Value, removed))
                    {
                        _dictionary.TryAdd(item.Key, item.Value);
                    }
                }
            }
        }
    }
}