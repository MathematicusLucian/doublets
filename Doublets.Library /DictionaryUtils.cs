using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Doublets.Library;

public class DictionaryUtils
{
    public static HashSet<string> LoadDictionary(string dictionaryFile)
    {
        return new HashSet<string>(
            File.ReadAllLines(dictionaryFile)
            .Where(word => word.Length == 4)
            .Select(word => word.ToLower())
        );
    }

    private static List<string> GetValidNeighbors(string word, HashSet<string> dictionary)
    {
        var neighbors = new List<string>();
        var wordArray = word.ToCharArray();

        for (int i = 0; i < wordArray.Length; i++)
        {
            char originalChar = wordArray[i];

            for (char c = 'a'; c <= 'z'; c++)
            {
                if (c == originalChar) continue;

                wordArray[i] = c;
                var newWord = new string(wordArray);

                if (dictionary.Contains(newWord))
                {
                    neighbors.Add(newWord);
                }
            }

            wordArray[i] = originalChar; // Restore the original word
        }

        return neighbors;
    }

    private static List<string> ConstructPath(string meetingWordFromStart, string meetingWordFromEnd,
                                              Dictionary<string, string> visitedFromStart,
                                              Dictionary<string, string> visitedFromEnd)
    {
        // Reconstruct the path from start to meeting point
        var path = new List<string>();

        string currentWord = meetingWordFromStart;
        while (currentWord != null)
        {
            path.Add(currentWord);
            currentWord = visitedFromStart[currentWord];
        }
        path.Reverse(); // Since we built it backwards

        // Add the path from meeting point to the end
        currentWord = meetingWordFromEnd;
        while (currentWord != null)
        {
            path.Add(currentWord);
            currentWord = visitedFromEnd[currentWord];
        }

        return path;
    }
}