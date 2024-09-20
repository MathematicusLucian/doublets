using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordsPath
{
    class Program
    {
        static void Main(string[] args)
        {
            string dictionaryFile = args[0];
            string startWord = args[1].ToLower();
            string endWord = args[2].ToLower();
            string resultFile = args[3];

            // Load the dictionary of four-letter words
            HashSet<string> dictionary = DictionaryUtils.LoadDictionary(dictionaryFile);

            // Find the shortest transformation sequence
            List<string> result = BreadthFirstSearch.FindTransformationUsingBFS(dictionary, startWord, endWord);

            if (result == null)
            {
                Console.WriteLine("No valid transformation sequence found.");
            }
            else
            {
                // Write the result to the ResultFile
                File.WriteAllLines(resultFile, result);
                Console.WriteLine("Transformation sequence written to " + resultFile);
            }
        }
    }
}