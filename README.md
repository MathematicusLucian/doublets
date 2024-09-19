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

6. **Performance:** I will use a hash set for fast lookups when checking if a word exists in the dictionary or if a word has been visited. The BFS will stop early if we find the `EndWord`.

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
