using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Doublets.Library;

namespace Doublets.App
{
    class Program
    {
        public class Options
        {
            [Option('d', "dictionaryFile", Required = true, HelpText = "Requires dictionary filename")]
            public string DictionaryFile { get; set; } = default!;

            [Option('o', "resultFile", Required = false, HelpText = "Requires results filename.")]
            public string ResultFile { get; set; } = default!;

            [Option('s', "startWord", Required = true, HelpText = "Requires startWord.")]
            public string StartWord { get; set; } = default!;

            [Option('e', "endWord", Required = true, HelpText = "Requires endWord.")]
            public string EndWord { get; set; } = default!;
        }

        static void Main(string[] args)
        {
            if (args.Length != 8)
            {
                Console.WriteLine("Usage: -d <DictionaryFile> -o <ResultFile> -s <StartWord> -e <EndWord>");
                return;
            }

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(argsParsed =>
                {
                    string dictionaryFile = argsParsed.DictionaryFile;
                    string resultFile = argsParsed.ResultFile;
                    string startWord = argsParsed.StartWord.ToLower();
                    string endWord = argsParsed.EndWord.ToLower();

                    try
                    {
                        // Load the dictionary of four-letter words
                        HashSet<string> dictionary = DictionaryUtils.LoadDictionary(dictionaryFile);

                        // Find the shortest transformation sequence
                        List<string> result = BreadthFirstSearch.FindPath(dictionary, startWord, endWord);

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
            );
        }
    }
}