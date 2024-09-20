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