using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Doublets
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Usage: <DictionaryFile> <StartWord> <EndWord> <ResultFile>");
                return;
            }

            string dictionaryFile = args[0];
            string startWord = args[1].ToLower();
            string endWord = args[2].ToLower();
            string resultFile = args[3];

            try
            {
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
            catch (FileNotFoundException)
            {
                Console.WriteLine("Dictionary file not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}