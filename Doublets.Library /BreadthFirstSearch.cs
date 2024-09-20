using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Doublets.Library;

public class BreadthFirstSearch
{
    // BFS to find the shortest transformation sequence
    public static List<string> FindTransformationUsingBFS(HashSet<string> dictionary, string startWord, string endWord)
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

    public static List<string> FindTransformationUsingBidirectionalBFS(HashSet<string> dictionary, string startWord, string endWord)
    {
        // Edge case: startWord is the same as endWord
        if (startWord == endWord)
        {
            return new List<string> { startWord };
        }

        // Bidirectional BFS setup
        var startQueue = new Queue<string>();
        var endQueue = new Queue<string>();

        var startVisited = new Dictionary<string, string> { { startWord, null } }; // word -> parent
        var endVisited = new Dictionary<string, string> { { endWord, null } }; // word -> parent

        startQueue.Enqueue(startWord);
        endQueue.Enqueue(endWord);

        while (startQueue.Count > 0 && endQueue.Count > 0)
        {
            // Expand from the start side
            if (ExpandLayer(startQueue, startVisited, endVisited, dictionary, out var result))
            {
                return result;
            }

            // Expand from the end side
            if (ExpandLayer(endQueue, endVisited, startVisited, dictionary, out result))
            {
                return result;
            }
        }

        // No path found
        return null;
    }

    private static bool ExpandLayer(Queue<string> queue, Dictionary<string, string> visitedFromThisSide,
                                    Dictionary<string, string> visitedFromOtherSide, HashSet<string> dictionary,
                                    out List<string> result)
    {
        result = null;

        int currentLevelSize = queue.Count;

        for (int i = 0; i < currentLevelSize; i++)
        {
            var currentWord = queue.Dequeue();

            foreach (var neighbor in GetValidNeighbors(currentWord, dictionary))
            {
                if (visitedFromThisSide.ContainsKey(neighbor))
                {
                    continue; // Already visited from this side
                }

                if (visitedFromOtherSide.ContainsKey(neighbor))
                {
                    // Found a meeting point, reconstruct the path
                    result = ConstructPath(currentWord, neighbor, visitedFromThisSide, visitedFromOtherSide);
                    return true;
                }

                visitedFromThisSide[neighbor] = currentWord; // Track the parent word
                queue.Enqueue(neighbor);
            }
        }

        return false;
    }
}