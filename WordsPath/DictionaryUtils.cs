using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordsPath;

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
}