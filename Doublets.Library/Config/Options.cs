using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text.RegularExpressions;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace Doublets.Library;

public class Options
{
    public void ValidateArgs()
    {
        if (StartWord.Length < 4) throw new ArgumentException($"StartWord length should be 4 characters");
        if (EndWord.Length < 4) throw new ArgumentException($"EndWord length should be 4 characters");
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