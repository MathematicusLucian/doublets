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
        static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<Options>(args)
                    .WithParsed<Options>(argsParsed =>
                    {
                        argsParsed.ValidateArgs();

                        try
                        {
                            var doubletsProcessor = new DoubletsProcessor(
                                new DoubletsProcessorConfiguration(argsParsed, "BFS"),
                                new ResultsOutput(argsParsed.ResultFile)
                            )
                            .SearchForPath()
                            .SaveResults(); 
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred: {0}",  ex.Message);
                        }
                    }
                );
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Usage: -d <DictionaryFile> -o <ResultFile> -s <StartWord> -e <EndWord>");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex);
            }
        }
    }

    public class ResultsOutput : IResultsOutput
    {
        private readonly string _resultFile;
        public ResultsOutput(string resultFile)
        {
            _resultFile = resultFile;
        }

        public void SaveResults(List<string> result)
        {
            if (result == null)
            {
                Console.WriteLine("No valid doublets path found.");
            }
            else
            {
                // Write the result to the ResultFile
                File.WriteAllLines(_resultFile, result);
                Console.WriteLine("Doublets - Path: {0} | Length: {1}",String.Join(",",result),result.Count);
                Console.WriteLine("Doublets path written to " + _resultFile);
            }
        }
    }
}