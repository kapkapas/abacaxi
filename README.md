# Abacaxi [![Build status](https://ci.appveyor.com/api/projects/status/ckq7nanjy3nms8a7?svg=true)](https://ci.appveyor.com/project/pavkam/abacaxi)

*"No code library is complete without a binary search!"*

A library and repository of algorithms, data structures and helper methods that make one's daily programming life easier (in .NET that is).

Abacaxi is available on NuGet: https://www.nuget.org/packages/Abacaxi/ and is built against __.NET Standard 2.0__ and __.NET 4.5__.

### F# Support:

F# bindings are available in the **Abacaxi.FSharp** library (targets __.NET Standard 2.0__ only). 

Note that not all C# methods have F# bindings since F# core library already contains comparable implementations. Also, some methods in F# do not make sense in C# world as well.

### The list of core data structures:
| Data structure | Description |
| :--- | :--- |
| `Heap` | Implements the *heap* data structure (also known as *priority queue*). Related material: <https://en.wikipedia.org/wiki/Heap_(data_structure)> |
| `MeanHeap` | Helper class that uses two *heaps* to provide O(1) mean value access to a sequence. Related material: <https://en.wikipedia.org/wiki/Heap_(data_structure)> |
| `BitSet` | Implements the standard *ISet&lt;int&gt;* data structure in an optimized form (using bit masks). Related material: <https://en.wikipedia.org/wiki/Bit_array> |
| `DisjointSet` | Also known as *union-find* or *merge-find* data structure. Related material: <https://en.wikipedia.org/wiki/Disjoint-set_data_structure> |
| `LinkedLinkedNode` | Represents a node in a *singly-linked list*. All operations implemented by the node classes. Related material: <https://en.wikipedia.org/wiki/Linked_list> |
| `Trie` | A *trie* that implements the *IDictionary&lt;TKey, TValue&gt;* interface. Related material: <https://en.wikipedia.org/wiki/Trie> |
| `AvlTree` | The standard *AVL self-balancing tree*. Related material: <https://en.wikipedia.org/wiki/AVL_tree> |
| `BinarySearchTree` | The standard *binary search tree*. Related material: <https://en.wikipedia.org/wiki/Binary_search_tree> |
| `LeftLeaningRedBlackTree` | The simplified version of *Red/Black self-balancing tree*. Related material: <https://en.wikipedia.org/wiki/Left-leaning_red%E2%80%93black_tree> |
| `Graph` | A generic *(un)directional weighted graph* that implements many algorithms. Related material: <https://en.wikipedia.org/wiki/Graph> |
| `ChessHorsePathGraph` | A specialized graph implementation used to solve the *Knight's Tour* problem. Related material: <https://en.wikipedia.org/wiki/Knight%27s_tour> |
| `StringNeighborhoodGraph` | A specialized graph implementation used to solve the *Word Ladder* problem. Related material: <https://en.wikipedia.org/wiki/Word_ladder> |
| `MazeGraph` | A specialized base class for other graphs that use a two-dimensional integer board (think, a rat's maze). All standard graph algorithms can be applied to such a graph. |
| `LiteralGraph` | A specialized graph used mainly in testing the graph-related algorithms. |
| `Mash` | The Swiss army knife of collections. In essence, a tree-like dictionary which can have other sub-dictionaries as children and store a list of items as leaves. Employs some techniques to avoid wasting unnecessary memory. |

### The list of helper/additional classes:
| Class | Description |
| :--- | :--- |
| `ArrayEqualityComparer` | Implements an equality comparer that is able to check two array for equality. The class is useful when using dictionaries/sets whose keys are arrays. |
| `Temporary` | A class used to store a value for a specific amount of time. The value expires and has to be reloaded. **Multi-threaded** |
| `BitWriter` | A specialized I/O class that implements the *Stream* base class. Allows for writing to a stream with bit granularity. |
| `GlobPattern` | Simple class that allows checking if a string matches a glob-like pattern (e.g. _"some*.?xt"_) |
| `DependencySquid` | Class that helps represent the state of a dependancy tree (including conflicts). Can be used to validate selection viability (think package dependancies). |
| `Cached` | A simple wrapper that allows storing a value and considering it "valid" for a certain duration. Automatic value refresh is performed when value expires. |
| `NanoCache` | A very simple cache container. Very useful when quick-and-dirty caching is needed. |

### The list of implemented algorithms/helper methods:
| Algorithm/Method | Description |
| :--- | :--- |
| `LinkedListNode.TryGetMiddleAndTailNodes` | Finds the middlem, tail nodes and the length of the list in one pass. If the list is circular, the method returns `-1` See related material: <https://en.wikibooks.org/wiki/Data_Structures/LinkedLists> |
| `LinkedListNode.Reverse` | *Reverses* a singly-linked list using the recursive algorithm. See related material: <https://en.wikibooks.org/wiki/Data_Structures/LinkedLists>  |
| `LinkedListNode.GetIntersectionNode` | Find the node that is the *intersection* of two singly-linked. See related material: <https://en.wikibooks.org/wiki/Data_Structures/LinkedLists>  |
| `LinkedListNode.GetKnotNode` | Find the node that connects the tail of the list to another interior node (knot) in a singly-linked. See related material: <https://www.geeksforgeeks.org/detect-and-remove-loop-in-a-linked-list/>  |
| `Graph.TraverseBfs` | Traverses the vertices in a graph using the *breadth-first search*. See related material: <https://en.wikipedia.org/wiki/Breadth-first_search> |
| `Graph.TraverseDfs` | Traverses the vertices in a graph using the *depth-first search*. See related material: <https://en.wikipedia.org/wiki/Depth-first_search> |
| `Graph.FillWithOneColor` | *Fills* all vertices of a graph with a given "color". See related material: <https://en.wikipedia.org/wiki/Flood_fill> |
| `Graph.FindShortestPath` | Find the *shortest path* between two graph vertices. See related material: <https://en.wikipedia.org/wiki/Shortest_path_problem>  |
| `Graph.GetComponents` | Finds all distinct *connected components* of a graph. The method returns each component represented as another graph. See related material: <https://en.wikipedia.org/wiki/Connected_component_(graph_theory)> |
| `Graph.TopologicalSort` | Implements the *topological sorting* algorithm. See related material: <https://en.wikipedia.org/wiki/Topological_sorting> |
| `Graph.FindAllArticulationVertices` | Finds all *articulation points* in a graph. See related material: <https://en.wikipedia.org/wiki/Biconnected_component> |
| `Graph.IsBipartite` | Checks if a graph is *bipartite*. See related material: <https://en.wikipedia.org/wiki/Bipartite_graph> |
| `Graph.DescribeVertices` | Returns a *description* of all vertices in a graph, including in-degree, out-degree and component index. |
| `Graph.FindCheapestPath` | Finds the *cheapest path* between two vertices in a graph. See related material: <https://en.wikipedia.org/wiki/A*_search_algorithm> |
| `Graph.Colorize` | Finds the minimum number of colors and applies thm on graph verticesh. See related material: <https://en.wikipedia.org/wiki/Graph_coloring> |
| `FibonacciSequence.Enumerate` | Lists all *Fibonacci numbers* up to a given index in the series. See related material: <https://en.wikipedia.org/wiki/Fibonacci_number> |
| `FibonacciSequence.GetMember` | Returns the *Fibonacci number* at a given index in the series. See related material: <https://en.wikipedia.org/wiki/Fibonacci_number> |
| `Integer.DeconstructIntoPowersOfTwo` | *Deconstructs* an integer into a sum of powers of two. |
| `Integer.DeconstructIntoPrimeFactors` | *Deconstructs* an integer into a prime factors. |
| `Integer.Break` | *Deconstructs* an integer into repeatable components. See related material: <https://en.wikipedia.org/wiki/Change-making_problem> |
| `Integer.IsPrime` | Checks whether an integer is a *prime number*. |
| `Integer.Zip` | *Zips the digits* of two integers into a new integer. |
| `Integer.Divide` | A simple *division* algorithm that only uses addition. |
| `Integer.Swap` | Algorithm that performs a *swap* of two integers without an additional variable. |
| `Integer. GetCountOfTrailingZeroesInFactorial` | Find the number of *trailing zeroes* for a factorial number. See related material: <https://www.geeksforgeeks.org/count-trailing-zeroes-factorial-number/> |
| `Integer.Max` | Returns the *maximum of two integers* without using comparisons. |
| `Integer.Sum` | Returns the *sum of two integers* using only bitwise operations. |
| `IntegerPartitions.Enumerate` | Enumerates all *integer partitions* for a given number. |
| `IntegerPartitions.GetCount` | Calculates the number of *integer partitions* for a given number. |
| `Knapsack.Fill` | The generic *0/1 knapsack* algorithm. See related material: <https://en.wikipedia.org/wiki/Knapsack_problem> |
| `Interval.MergeOverlapping` | Merges a sequence of overlapping intervals (_or whatever intervals do overlap_) leaving the other ones untouched. |
| `Interval.ChoseBestNonOverlapping` | Selects the non-overlapping (and scored) intervals that yeild the best aggregate score. |
| `Pairing.GetPairsWithMinimumCost` | *Pairs the elements of a sequence* as to minimize the cost of each pair. |
| `Pairing. GetPairsWithApproximateMinimumCost` | *Pairs the elements of a sequence* as to approximate minimization of the cost of each pair.  |
| `Pairing.GetPairWithMaximumDifference` | Find the first pair in a sequence whose elements *yield the greatest difference*. |
| `Pairing. GetPairWithIncreasingOrderMaximumDifference` | Find the first pair in a sequence whose elements *yield the greatest difference* with the restriction that elements in eth pair are strictly increasing. |
| `Pairing.GetEqualizationPairs` | Finds all pairs of elements from both sequences whose values, if swapped, makes the sequences being equal in their sum. |
| `ZArray.Construct` | Constructs a *Z-array* from a given input sequence. Z-arrays are useful for string pattern matching. See related materials: <http://wittawat.com/assets/talks/z_algorithm.pdf>, <https://shiv4289.wordpress.com/2013/09/17/z-algorithm-for-pattern-matching/> |
| `RandomExtensions.Sample` | A *random sampling* algorithm for a sequence of objects. See related material: <https://en.wikipedia.org/wiki/Reservoir_sampling> |
| `RandomExtensions.NextBool` | An extension method that allows retrieving a *random boolean* value. |
| `RandomExtensions.NextItem` | An extension method that allows retrieving a *random item* from a sequence of objects. |
| `SequenceAlgorithms. FindLongestIncreasingSequence` | Finds the *longest increasing sequence* within a given sequence. |
| `SequenceAlgorithms. ContainsTwoElementsThatAggregateTo` | Determines whether the sequence contains two elements *that aggregate to a given target*. |
| `SequenceAlgorithms.FindDuplicates` | *Finds duplicates* in a sequence. A specialized and optimized version for integer sequences also provided. |
| `SequenceAlgorithms.FindUniques` | *Finds unique* elements in a sequence. |
| `SequenceAlgorithms.FindUniquesInOrder` | *Finds unique* elements in a sequence and retains the order of their appearance in the sequence. |
| `SequenceAlgorithms.ExtractNestedBlocks` | An algorithm to allow *extracting nested* sub-sequences from a sequence (e.g. _"(a(b))"_ would return _"(b)"_ then _"(a(b))"_). |
| `SequenceAlgorithms. GetSubsequencesOfAggregateValue` | Finds all *sub-sequences of a given aggregated value* in another sequence. |
| `SequenceAlgorithms.Interleave` | Creates a sequence which combines multiple *interleaved sequences* based on a given comparison. |
| `SequenceAlgorithms.Reverse` | *Reverses* a sequence in place. |
| `SequenceAlgorithms.Repeat` | Creates a sequence which is based on the original sequence *repeated a number of times*. |
| `SequenceAlgorithms.BinarySearch` | Implements the standard *binary search* algorithm. See related material: <https://en.wikipedia.org/wiki/Binary_search_algorithm> |
| `SequenceAlgorithms.BinaryLookup` | Implements a slightly modified *binary search* algorithm that returns the range of matching items or the position of immediatelly smaller item. See related material: <https://en.wikipedia.org/wiki/Binary_search_algorithm> |
| `SequenceAlgorithms.Diff` | Implements the generic *edit distance* algorithm. See related material: <https://en.wikipedia.org/wiki/Edit_distance> |
| `SequenceAlgorithms. GetLongestCommonSubSequence` | Finds the longest sub-sequence that is common to two distinct sequences. See related material: <https://en.wikipedia.org/wiki/Longest_common_subsequence_problem> |
| `SequenceAlgorithms.DeconstructIntoTerms` | Deconstructs a given sequence into a sequence of terms (sub-sequences) based on given term scoring (e.g. think of recognizing an English phrase for all lower-case text without whitespaces using a given dictionary of known words: "_ilovecookies_" will be split into "_i_", "_love_", "_cookies_"). |
| `SequenceAlgorithms.GetItemFrequencies` | Gets the *items and their frequencies* within a sequence. |
| `SequenceAlgorithms.IsPalindrome` | Checks if a given (sub)sequence is a palindrome. See related material: <https://en.wikipedia.org/wiki/Palindrome> |
| `SequenceAlgorithms. IsPermutationOfPalindrome` | Checks if a given (sub)sequence is a possible permutation of palindrome. See related material: <https://en.wikipedia.org/wiki/Palindrome> |
| `SequenceAlgorithms.IndexOfPermutationOf` | Finds the first occurence of sub-sequence (or any of its permutations) in the given sequence. |
| `SequenceAlgorithms. FindUnorderedSubsequenceRange` | Finds the *un-ordered sub-sequence* inside a given sequence. Once found sub-sequence is sorted, the whole sequence becomes ordered. |
| `SequenceAlgorithms. GetRangeWithGreatestAggregateValue` | Finds the sub-sequence range with the *greatest aggregate* value. |
| `SequenceExtensions.ToSet` | Helper methods to *convert a given sequence into a set*. |
| `SequenceExtensions.AsList` | Helper method that *interprets a given sequence as a list*. If the sequence is already a list/array then the original object is returned; otherwise, the sequence is converted to an array. **This method may or may not create a new object and does not guarantee mutability of the result.** |
| `SequenceExtensions.Copy` | Helper method to *copy a sub-sequence* into a new array. |
| `SequenceExtensions.AddOrUpdate` | Extends the dictionary classes with the ability to *add a new, or update an existing* key/pair. |
| `SequenceExtensions.Increment` | Extends the dictionary classes with the ability to *increment integer value* for a given key (treating non-existing keys as having value of zero). |
| `SequenceExtensions.Append` | A number of small utility methods used to *append items to arrays*. If the array is null, a new array is created. These methods return new arrays as their return values. |
| `SequenceExtensions.ToList` | Utility method that replaces a common _"Select(...).Tolist()"_ LINQ pattern. |
| `SequenceExtensions.Partition` | *Partitions* a given sequence into a batch of smaller partitions of a given size. |
| `SequenceExtensions.EmptyIfNull` | Method returns an *empty sequence* if the current sequence is null; otherwise it returns the sequence itself. |
| `SequenceExtensions.IsNullOrEmpty` | Mimics the **String.IsNullOrEmpty** method. |
| `SequenceExtensions.ToString` | Utility method that replaces a common _"String.Join(..., ...Select(...))"_ LINQ pattern. |
| `SequenceExtensions.Min` | Returns the element of a sequence with a given *selected minimum* (e.g. select the person object with the smallest age). |
| `SequenceExtensions.Max` | Returns the element of a sequence with a given *selected maximum* (e.g. select the person object with the greatest age). |
| `SequenceExtensions.Segment` | Returns a view of the original sequence bounded to a segment of the list. Useful when other methods do not allow specifying a start/length pair of arguments. |
| `SequenceExtensions.SelectValues` | Selects the *values of a sequence of nullables*. Nullable items that have no value are skipped. |
| `SequenceExtensions.Separate` | Separates items from a sequence into *two distinct arrays* based on a predicate. |
| `SequenceExtensions.Unzip` | Splits tuples (and key-value pairs) into separate array containing individual items. |
| `SequenceExtensions.IsValidAdjacency` | Checkes whether all sequence adjacent elements satisfy a common condition. |
| `SequenceExtensions.IsOrdered` | Checkes whether the elements of a given sequence are sorted in ascending order. |
| `SequenceExtensions.IsStrictlyOrdered` | Checkes whether the elements of a given sequence are strictly sorted in ascending order. |
| `SequenceExtensions.IsOrderedDescending` | Checkes whether the elements of a given sequence are sorted in descending order. |
| `SequenceExtensions. IsStrictlyOrderedDescending` | Checkes whether the elements of a given sequence are strictly sorted in descending order. |
| `SequenceExtensions.Fold` | *Folds a sequence* by merging consecutive appearances of a given item into one output item (think reduce). |
| `Set.EnumerateSubsetCombinations` | *Splits a set into subsets* of a given length and returns all such combinations. |
| `Set.SplitIntoSubsetsOfEqualValue` | Tries to find all *subsets with equal aggregate value*. See related material: <http://www.usaco.org/index.php?page=viewproblem2&cpid=139> |
| `Set.GetSubsetWithNearValue` | Extracts a subset of integers whose sum is equal or very *close to the target value*. |
| `Set.ContainsSubsetWithExactValue` | Checks whether there is a subset of integers whose sum is *equal to a target value*. |
| `Set.GetSubsetWithGreatestValue` | Finds the first *N* number of pairs with greatest sums. |
| `Set.GetPermutations` | Evaluates and returns all permutations of a given set. See related material: <https://en.wikipedia.org/wiki/Permutation> |
| `Sorting.BubbleSort` | Implements the standard *bubble sort* algorithm. See related material: <https://en.wikipedia.org/wiki/Bubble_sort> |
| `Sorting.CocktailShakerSort` | Implements the standard *cocktail shaker sort* algorithm. See related material: <https://en.wikipedia.org/wiki/Cocktail_shaker_sort> |
| `Sorting.CombSort` | Implements the standard *comb sort* algorithm. See related material: <https://en.wikipedia.org/wiki/Comb_sort> |
| `Sorting.GnomeSort` | Implements the standard *gnome sort* algorithm. See related material: <https://en.wikipedia.org/wiki/Gnome_sort> |
| `Sorting.HeapSort` | Implements the standard *heapsort* algorithm. See related material: <https://en.wikipedia.org/wiki/Heapsort> |
| `Sorting.InsertionSort` | Implements the standard *insertion sort* algorithm. See related material: <https://en.wikipedia.org/wiki/Insertion_sort> |
| `Sorting.MergeSort` | Implements the standard *merge sort* algorithm. See related material: <https://en.wikipedia.org/wiki/Merge_sort> |
| `Sorting.OddEvenSort` | Implements the standard *odd-even sort* algorithm. See related material: <https://en.wikipedia.org/wiki/Odd%E2%80%93even_sort> |
| `Sorting.QuickSort` | Implements the standard *quicksort* algorithm. See related material: <https://en.wikipedia.org/wiki/Quicksort> |
| `Sorting.ShellSort` | Implements the standard *shellsort* algorithm. See related material: <https://en.wikipedia.org/wiki/Shellsort> |
| `ObjectExtensions.IsAnyOf` | Helper methods that allows checking if an object is *equal to any other object* in a sequence (think of *_x IN (o1, o2, o3)*). |
| `ObjectExtensions.Inspect` | A simple helper method that allows *extracting fields/properties/methods* values from an object as a dictionary. |
| `ObjectExtensions.TryConvert` | Tries to *convert* a given object to a given type. Uses different techniques to achieve this goal. |
| `ObjectExtensions.As` | A simpler version of *TryConvert* that throws an exception if the conversion is not possible. |
| `StringExtensions.AsList` | Returns a *wrapper IList&lt;char&gt;* object. Useful when using other algorithms that expect a list. **The returned list is read-only for obvious reasons.** |
| `StringExtensions.Reverse` | *Reverses* a string and returns the reversed version. |
| `StringExtensions.Shorten` | *Shortens* a string to a given maximum length (considering Unicode surrogate-pairs, etc.). Allows for an optional "shortening indicator string" used at the end of the string (think *"This is a go..."*).  |
| `StringExtensions.Escape` | Escapes a string using the standard "C" escape sequences (e.g. _"\n"_ for new line). |
| `StringExtensions.Like` | A wrapper method on top of **GlobPattern** class. A convenient method to check if a *string matches a pattern*.  |
| `StringExtensions.FindDuplicates` | Finds *duplicate characters* in the string. Uses both a set and a small array for ASCII characters. |
| `StringExtensions.SplitIntoLines` | *Splits* a given string into its contituent lines. Treats both _"\n"_ and _"\r\n"_ as line breaks. |
| `StringExtensions.WordWrap` | *Word-wraps* a string to a given max line length. Uses white-spaces and puctuation characters as potential line breaks. |
| `StringExtensions.StripDiacritics` | *Strips the Unicode diacritics* from a text. Useful for text normalization in searches. |
| `StringBuilderExtensions. AppendNotEmptyLine` | *Appends a line to the string builder* if the line is not empty. |