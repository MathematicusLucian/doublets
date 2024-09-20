using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text.RegularExpressions;

namespace Doublets.Library;

public class DoubletsProcessorConfiguration
{
    public virtual Options argsParsed { get; }
    public virtual string dictionaryFile { get; }
    public virtual HashSet<string>? dictionary { get; }
    public virtual string resultFile { get; }
    public virtual string startWord { get; }
    public virtual string endWord { get; }
    public virtual string algoritmn { get; }

    public DoubletsProcessorConfiguration(Options argsParsed, string algoritmnSelected)
    {
        argsParsed = argsParsed;
        dictionaryFile = argsParsed.DictionaryFile;
        resultFile = argsParsed.ResultFile;
        startWord = argsParsed.StartWord.ToLower();
        endWord = argsParsed.EndWord.ToLower();

        // Load the dictionary of four-letter words
        dictionary = DictionaryUtils.LoadDictionary(dictionaryFile);
        if (dictionary.Count < 1) throw new Exception("Dictionary is empty");

        algoritmn = algoritmnSelected;
    }
}