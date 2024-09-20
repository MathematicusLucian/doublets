using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Doublets.Library;

public class BidirectionalBFS : IDoubletsSearch
{
    public static List<string> FindPath(HashSet<string> dictionary, string startWord, string endWord)
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

            foreach (var neighbor in DictionaryUtils.GetValidNeighbors(currentWord, dictionary))
            {
                if (visitedFromThisSide.ContainsKey(neighbor))
                {
                    continue; // Already visited from this side
                }

                if (visitedFromOtherSide.ContainsKey(neighbor))
                {
                    // Found a meeting point, reconstruct the path
                    result = DictionaryUtils.ConstructPath(currentWord, neighbor, visitedFromThisSide, visitedFromOtherSide);
                    return true;
                }

                visitedFromThisSide[neighbor] = currentWord; // Track the parent word
                queue.Enqueue(neighbor);
            }
        }

        return false;
    }
}