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
            public void ValidateArgs()
            {
                if (!File.Exists(DictionaryFile)) throw new ArgumentException($"DictionaryFile file not found: {DictionaryFile}");
            }

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
            try
            {
                Parser.Default.ParseArguments<Options>(args)
                    .WithParsed<Options>(argsParsed =>
                    {
                        argsParsed.ValidateArgs();

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
                                Console.WriteLine("No valid doublets path found.");
                            }
                            else
                            {
                                // Write the result to the ResultFile
                                File.WriteAllLines(resultFile, result);
                                Console.WriteLine("Doublets - Path: {0} | Length: {1}",result,result.Count);
                                Console.WriteLine("Doublets path written to " + resultFile);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred: " + ex.Message);
                        }
                    }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("Usage: -d <DictionaryFile> -o <ResultFile> -s <StartWord> -e <EndWord>");
            }
        }
    }
}