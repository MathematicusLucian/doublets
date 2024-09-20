using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Doublets;

public class ASearch
{
    public static List<string> FindShortestTransformationUsingAStar(HashSet<string> dictionary, string startWord, string endWord)
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

        // Priority queue for the open set, using f(n) to prioritize nodes
        var openSet = new SortedList<double, string>();
        var gCosts = new Dictionary<string, int>(); // g(n) cost map
        var cameFrom = new Dictionary<string, string>(); // Track the best parent word

        openSet.Add(0, startWord); // f(n) = 0 for start
        gCosts[startWord] = 0;

        var closedSet = new HashSet<string>();

        while (openSet.Count > 0)
        {
            // Get the word with the lowest f(n) value
            var currentWord = openSet.First().Value;
            openSet.RemoveAt(0);

            // If we've reached the end word, reconstruct the path
            if (currentWord == endWord)
            {
                return DictionaryUtils.ReconstructPath(cameFrom, currentWord);
            }

            closedSet.Add(currentWord);

            // Explore neighbors
            foreach (var neighbor in DictionaryUtils.GetValidNeighbors(currentWord, dictionary))
            {
                if (closedSet.Contains(neighbor)) continue;

                var tentativeGCost = gCosts[currentWord] + 1; // g(n) = g(current) + 1

                if (!gCosts.ContainsKey(neighbor) || tentativeGCost < gCosts[neighbor])
                {
                    // Update the cost and the path
                    cameFrom[neighbor] = currentWord;
                    gCosts[neighbor] = tentativeGCost;

                    // Calculate f(n) = g(n) + h(n) and add to the open set
                    double fCost = tentativeGCost + Heuristic(neighbor, endWord);
                    openSet.Add(fCost, neighbor);
                }
            }
        }

        // No path found
        return new List<string>();
    }

    private static double Heuristic(string word, string endWord)
    {
        // Hamming distance: the number of different letters between the words
        int hammingDistance = 0;
        for (int i = 0; i < word.Length; i++)
        {
            if (word[i] != endWord[i])
            {
                hammingDistance++;
            }
        }
        return hammingDistance;
    }
}