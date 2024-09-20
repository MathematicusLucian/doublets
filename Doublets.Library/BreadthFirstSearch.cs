using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Doublets.Library;

public class BreadthFirstSearch : IDoubletsSearch
{
    // BFS to find the shortest transformation sequence
    public static List<string> FindPath(HashSet<string> dictionary, string startWord, string endWord)
    {
        // Validate the StartWord and EndWord
        if (!dictionary.Contains(startWord) || !dictionary.Contains(endWord))
        {
            Console.WriteLine("StartWord or EndWord not found in the dictionary.");
            return new List<string>();
        }

        // Edge case: startWord is the same as endWord
        if (startWord == endWord)
        {
            return new List<string> { startWord };
        }

        // BFS queue stores the word and the path leading to it
        Queue<(string word, List<string> path)> queue = new Queue<(string word, List<string> path)>();
        queue.Enqueue((startWord, new List<string> { startWord }));

        // Visited set to avoid revisiting words
        HashSet<string> visited = new HashSet<string> { startWord };

        while (queue.Count > 0)
        {
            var (currentWord, currentPath) = queue.Dequeue();

            // Check all possible single-letter transformations
            for (int i = 0; i < 4; i++)
            {
                for (char c = 'a'; c <= 'z'; c++)
                {
                    if (c == currentWord[i]) continue; // Skip same letter

                    string newWord = currentWord.Substring(0, i) + c + currentWord.Substring(i + 1);

                    // If it's the end word, return the path
                    if (newWord == endWord)
                    {
                        currentPath.Add(endWord);
                        return currentPath;
                    }

                    // If the word is valid and not visited
                    if (dictionary.Contains(newWord) && !visited.Contains(newWord))
                    {
                        visited.Add(newWord);
                        List<string> newPath = new List<string>(currentPath) { newWord };
                        queue.Enqueue((newWord, newPath));
                    }
                }
            }
        }

        // If no path found, return null
        return new List<string>();
    }
}