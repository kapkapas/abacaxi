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

using System.Linq;

namespace Abacaxi
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class implements multiple algorithms that deals with combinatorial problems.
    /// </summary>
    public static class Combinatorics
    {
        private struct Step
        {
            public int ItemIndex { get; }
            public int SetIndex { get; }

            public Step(int itemIndex, int setIndex)
            {
                ItemIndex = itemIndex;
                SetIndex = setIndex;
            }
        }

        /// <summary>
        /// Partitions a given integer into all possible combinations of smaller integers.
        /// </summary>
        /// <param name="number">The input number.</param>
        /// <returns>A sequence of combinations.</returns>
        public static IEnumerable<int[]> PartitionInteger(this int number)
        {
            if (number != 0)
            {
                var selection = new Stack<int>();
                var numbers = new Stack<int>();
                var i = 0;
                var dontBreak = false;
                var sign = Math.Sign(number);

                for (;;)
                {
                    if (i == 0)
                    {
                        selection.Push(number);
                        yield return selection.ToArray();
                        selection.Pop();

                        i = sign;
                    }
                    else if (i * sign > number / 2 * sign || dontBreak)
                    {
                        if (selection.Count == 0)
                            break;

                        number = numbers.Pop();
                        i = selection.Pop() + sign;

                        dontBreak = false;
                    }
                    else
                    {
                        selection.Push(i);
                        numbers.Push(number);

                        dontBreak = i * 2 == number;

                        number = number - i;
                        i = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the count of partitions that a <paramref name="number"/> can be split into.
        /// </summary>
        /// <param name="number">The number to split.</param>
        /// <returns>The partition count.</returns>
        public static int EvaluateIntegerPartitionCombinations(this int number)
        {
            number = Math.Abs(number);

            var solutions = new int[number + 1, number + 1];

            for (var m = 0; m <= number; m++)
            {
                for (var n = 0; n <= number; n++)
                {
                    if (m == 0)
                    {
                        solutions[n, m] = 0;
                    }
                    else if (n == 0)
                    {
                        solutions[n, m] = 1;
                    }
                    else if (n - m < 0)
                    {
                        solutions[n, m] = solutions[n, m - 1];
                    }
                    else
                    {
                        solutions[n, m] = solutions[n - m, m] + solutions[n, m - 1];
                    }
                }
            }

            return solutions[number, number];
        }

        /// <summary>
        /// Evaluates all combinations of items in <paramref name="sequence"/> divided into <paramref name="subsets"/>.
        /// </summary>
        /// <typeparam name="T">The type of items in the <paramref name="sequence"/></typeparam>
        /// <param name="sequence">The sequence of elements.</param>
        /// <param name="subsets">Number of sub-sets.</param>
        /// <returns>All the combinations of subsets.</returns>
        public static IEnumerable<T[][]> EvaluateAllSubsetCombinations<T>(this IList<T> sequence, int subsets)
        {
            Validate.ArgumentNotNull(nameof(sequence), sequence);
            Validate.ArgumentGreaterThanZero(nameof(subsets), subsets);

            if (sequence.Count == 0)
            {
                yield break;
            }

            var resultSets = new List<T>[subsets];
            for (var i = 0; i < resultSets.Length; i++)
            {
                resultSets[i] = new List<T>();
            }

            var stack = new Stack<Step>();
            stack.Push(new Step(0, 0));

            while (stack.Count > 0)
            {
                var step = stack.Pop();

                if (step.SetIndex > 0)
                {
                    resultSets[step.SetIndex - 1].RemoveAt(resultSets[step.SetIndex - 1].Count - 1);
                }
                if (step.SetIndex < subsets)
                {
                    resultSets[step.SetIndex].Add(sequence[step.ItemIndex]);

                    stack.Push(new Step(step.ItemIndex, step.SetIndex + 1));

                    if (step.ItemIndex == sequence.Count - 1)
                    {
                        yield return resultSets.Select(s => s.ToArray()).ToArray();
                    }
                    else
                    {
                        stack.Push(new Step(step.ItemIndex + 1, 0));
                    }
                }
            }
        }

        /// <summary>
        /// Finds the subsets with equal aggregate value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="sequence"/>.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="aggregator">The aggregator function.</param>
        /// <param name="comparer">The comparer.</param>
        /// <param name="subsets">The number of subsets to split into.</param>
        /// <returns>The first sequence of subsets that have the same aggregated value.</returns>
        public static T[][] FindSubsetsWithEqualAggregateValue<T>(
            this IList<T> sequence, 
            Aggregator<T> aggregator, 
            IComparer<T> comparer, 
            int subsets)
        {
            Validate.ArgumentNotNull(nameof(sequence), sequence);
            Validate.ArgumentNotNull(nameof(aggregator), aggregator);
            Validate.ArgumentNotNull(nameof(comparer), comparer);
            Validate.ArgumentGreaterThanZero(nameof(subsets), subsets);

            foreach (var combo in EvaluateAllSubsetCombinations(sequence, subsets))
            {
                var firstSum = default(T);
                var allEqual = true;
                for (var i = 0; i < subsets; i++)
                {
                    var subsetSum = combo[i].Length == 0 ? default(T) : combo[i].Aggregate((current, item) => aggregator(current, item));
                    if (i == 0)
                    {
                        firstSum = subsetSum;
                    }
                    else
                    {
                        if (comparer.Compare(firstSum, subsetSum) != 0)
                        {
                            allEqual = false;
                            break;
                        }
                    }
                }

                if (allEqual)
                {
                    return combo;
                }
            }

            return new T[][] {};
        }
    }
}