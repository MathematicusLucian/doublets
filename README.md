# Doublets

A C#.NET console application to compute the shortest sequence of four-letter words from a `StartWord` to an `EndWord` by transforming one letter at a time. Each intermediate word must be present in the dictionary file, and the result should be written to the specified output file.

## Functionality

1. **Command-line Argument Parsing:** The program takes 4 arguments: `DictionaryFile`, `StartWord`, `EndWord`, and `ResultFile`. It ensures the correct number of arguments and handles the cases where the dictionary file is missing.

2. **Dictionary Parsing:** This dictionary file contains four-letter words, which we’ll use to find the transformation sequence. Load the dictionary from the `DictionaryFile`, filter out words that are not exactly four letters, and convert to lowercase for case-insensitive comparison.

3. **Breadth-First Search (BFS):** This essentially requires traversal of a tree. BFS is the best algorithm to find the shortest path (in terms of word transformations) from the `StartWord` to the `EndWord`, because it explores all possible transformations level by level. The BFS implementation will commence from the `StartWord` (initialize a BFS queue) and explore all one-letter transformations. For each word in the queue, generate all possible valid transformations (changing the `StartWord` one letter at a time.) For each transformation, the new word should exist in the dictionary and should not have been visited before (to avoid cycles). It will stop when the `EndWord` is found and return the shortest path.

4. **File Output:** If the `EndWord` is found during BFS, trace back the path and write it to the `ResultFile`. If no sequence is found, an appropriate message is displayed.

5. **Edge Cases:**

   - The `DictionaryFile` doesn’t exist.
   - The `StartWord` or `EndWord` is invalid (e.g., not in the dictionary).
   - No valid path exists between the `StartWord` and `EndWord`.

6. **Performance:**
   This approach is clean, maintainable, and can be easily extended with additional features like logging, user-friendly error handling, or more complex validation.

   - **Efficiency:** The BFS ensures that the shortest transformation path is found efficiently.
   - **Scalability:** Using a hash set (`HashSet<string>`) ensures that dictionary lookups are fast (O(1) average time complexity) when checking if a word exists in the dictionary or if a word has been visited. The BFS will stop early if we find the `EndWord`.
   - **Memory Usage:** The BFS queue and the visited set will both scale with the number of four-letter words in the dictionary.

## Alternate approaches

In addition to **Breadth-First Search (BFS)**, which is the most suitable algorithm for finding the shortest transformation sequence in this specific problem, there are other algorithms or approaches we could consider. Each of these has its own strengths and weaknesses depending on the problem's requirements, such as performance, memory usage, or complexity. Here's a breakdown of some alternatives:

### Summary of Algorithms

- **N** = Number of words in the dictionary.
- **M** = Length of each word (fixed at 4 here).

| Algorithm                | Optimal for Shortest Path? | Time Complexity | Space Complexity | Best Use Case                                                       |
| ------------------------ | -------------------------- | --------------- | ---------------- | ------------------------------------------------------------------- |
| **BFS**                  | Yes                        | O(N \* M^2)     | O(N)             | Best for shortest path with uniform cost.                           |
| **Bidirectional BFS**    | Yes                        | O((N/2) \* M^2) | O(N)             | Large dictionaries, where BFS would be slow.                        |
| **A\***                  | Yes (with good heuristic)  | O(N log N)      | O(N)             | If a good heuristic is available to estimate distance to `EndWord`. |
| **Dijkstra's Algorithm** | Yes                        | O(N log N)      | O(N)             | Weighted graphs, unnecessary for equal-cost steps.                  |
| **DFS**                  | No                         | O(N \* M^2)     | O(N)             | Finding any valid path, not necessarily shortest.                   |
| **Dynamic Programming**  | No                         | O(N^2)          | O(N)             | Optimization problems with overlapping subproblems.                 |
| **Genetic Algorithm**    | No                         | O(gen \* pop)   | O(pop)           | Approximating solutions in very large problem spaces.               |
| **Greedy Algorithm**     | No                         | O(N \* M)       | O(1)             | Quick, approximate solutions, but may miss the optimal solution.    |
| **Backtracking**         | No                         | O(N \* M!)      | O(N)             | Exhaustive search when problem space is small.                      |

- **BFS** remains the optimal choice for this specific problem because it guarantees the shortest path, is simple to implement, and performs well when all word transformations have the same cost.
- **Bidirectional BFS** or **A\*** could be useful for performance optimization in large datasets.
- Algorithms like **DFS**, **Greedy**, and **Genetic Algorithms** may be interesting but are not well-suited for ensuring the shortest path, which is required here.

### Implementions

Key Differences in Bidirectional BFS:

- **Two Queues**: We start BFS from both the `startWord` and `endWord` simultaneously.
- **Bidirectional BFS**: We now maintain two BFS queues (`startQueue` and `endQueue`) and search from both `startWord` and `endWord`.
- **Visited Dictionaries**: Two C# dictionaries, `startVisited` and `endVisited`, are used to store the visited words from both directions, along with the parent word used to reconstruct the path.
- **Early Termination**: The search terminates early when the two BFS searches meet in the middle.
- **Path Reconstruction**: When the two BFS searches meet, the full path is reconstructed by combining the paths from the `startWord` and `endWord`.

## Testing

### Unit tests ('red-green refactor'/TDD):

I will use the NUnit framework (you could use xUnit or MSTest if that is your preference).

- Test with valid transformation sequences where a path exists, e.g. normal cases with a valid transformation sequence.
- Test with no possible transformation path between `StartWord` and `EndWord`.
- Invalid start or end word.
- Test edge cases where the `DictionaryFile` is missing or contains invalid words.
- Cases with multiple shortest transformation paths.
- Test performance with a large dictionary file.

**Explanation of Unit Tests**

- **Test 1: Valid Path:** A simple test where there is a valid path ("spin" → "spit" → "spot"). It asserts that the path found matches the expected result.
- **Test 2: No Valid Path:** The dictionary contains "spin", "span", and "spot", but there’s no valid intermediate transformation for spot. This test checks that the method returns null.
- **Test 3: Start Word Not in Dictionary:** Tests the case where the startWord isn’t found in the dictionary, and it should return null.
- **Test 4: End Word Not in Dictionary:** Tests the case where the endWord is not in the dictionary, and it should return null.
- **Test 5: Empty Dictionary:** Tests the scenario when the dictionary is empty. In this case, it’s impossible to find any transformation, so the result should be null.
- **Test 6: Start Word Equals End Word:** When the startWord equals the endWord, the expected output should be the start word as the only element in the path.
- **Test 7: No Letter Change Possible:** Tests a case where no intermediate word exists that can change a letter between the start and end words. The result should be null.
- **Test 8: Multiple Shortest Paths:** If there are multiple valid shortest paths, we only check that one of the possible correct paths is returned.
