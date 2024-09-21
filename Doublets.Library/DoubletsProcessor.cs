using System;
using System.Collections.Generic;
using System.Linq;

namespace Doublets.Library;

public class DoubletsProcessor
{
    private DoubletsProcessorConfiguration _doubletsProcessorConfiguration;
    private readonly IResultsOutput _resultsOutput;
    public List<string> Results { get; private set; }

    public DoubletsProcessor(DoubletsProcessorConfiguration doubletsProcessorConfiguration, IResultsOutput resultsOutput)
    {
        _doubletsProcessorConfiguration = doubletsProcessorConfiguration;
        _resultsOutput = resultsOutput;
    }

    public DoubletsProcessor SearchForPath()
    {
        // Validate the StartWord and EndWord
        if (!_doubletsProcessorConfiguration.dictionary.Contains(_doubletsProcessorConfiguration.startWord) || !_doubletsProcessorConfiguration.dictionary.Contains(_doubletsProcessorConfiguration.endWord))
        {
            throw new Exception("StartWord or EndWord not found in the dictionary.");
        }

        // Edge case: startWord is the same as endWord
        if (_doubletsProcessorConfiguration.startWord == _doubletsProcessorConfiguration.endWord)
        {
            throw new Exception("StartWord is the same as EndWord.");
        }

        // Find the shortest transformation sequence
        if(_doubletsProcessorConfiguration.algoritmn == "BFS") {
            Results = BreadthFirstSearch
                .FindPath(_doubletsProcessorConfiguration.dictionary, _doubletsProcessorConfiguration.startWord, _doubletsProcessorConfiguration.endWord);
        }
        
        return this;
    }

    public DoubletsProcessor SaveResults()
    {
        _resultsOutput.SaveResults(Results);
        return this;
    }
}