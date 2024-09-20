using NUnit.Framework;
using System.Collections.Generic;
using Doublets.Library;

namespace Doublets.Tests;

[TestFixture]
public class Doublets_Test1
{
    [SetUp]
    public void Setup()
    {
    }

    // Unit Tests for Breadth-First Search
    // -----------------------------------

    // Test 1: Verify a valid shortest transformation sequence exists
    [Test]
    public void Test_BreadthFirstSearch_ValidTransformationPath()
    {
        // Arrange
        var dictionary = new HashSet<string> { "spin", "spit", "spat", "spot", "span" }; // Not in alphabetical order
        string startWord = "spin";
        string endWord = "spot";

        // Act
        List<string> result = BreadthFirstSearch.FindTransformationUsingBFS(dictionary, startWord, endWord);

        // Assert
        Assert.That(result.Count, Is.GreaterThan(0));
        CollectionAssert.AreEqual(new List<string> { "spin", "spit", "spot" }, result);
    }

    // Test 2: Verify no valid transformation path exists
    [Test]
    public void Test_BreadthFirstSearch_NoValidTransformationPath()
    {
        // Arrange
        var dictionary = new HashSet<string> { "spin", "span", "spot" };
        string startWord = "spin";
        string endWord = "spot";

        // Act
        List<string> result = BreadthFirstSearch.FindTransformationUsingBFS(dictionary, startWord, endWord);

        // Assert
        Assert.IsEmpty(result); // No valid path from spin to spot
    }

    // Test 3: Verify the start word is not in the dictionary
    [Test]
    public void Test_BreadthFirstSearch_StartWordNotInDictionary()
    {
        // Arrange
        var dictionary = new HashSet<string> { "spit", "spot", "span" };
        string startWord = "spin";
        string endWord = "spot";

        // Act & Assert
        Assert.IsEmpty(BreadthFirstSearch.FindTransformationUsingBFS(dictionary, startWord, endWord)); // startWord not in dictionary
    }

    // Test 4: Verify the end word is not in the dictionary
    [Test]
    public void Test_BreadthFirstSearch_EndWordNotInDictionary()
    {
        // Arrange
        var dictionary = new HashSet<string> { "spin", "spit", "span" };
        string startWord = "spin";
        string endWord = "spot";

        // Act & Assert
        Assert.IsEmpty(BreadthFirstSearch.FindTransformationUsingBFS(dictionary, startWord, endWord)); // endWord not in dictionary
    }

    // Test 5: Verify when dictionary is empty
    [Test]
    public void Test_BreadthFirstSearch_EmptyDictionary()
    {
        // Arrange
        var dictionary = new HashSet<string>(); // Empty dictionary
        string startWord = "spin";
        string endWord = "spot";

        // Act
        List<string> result = BreadthFirstSearch.FindTransformationUsingBFS(dictionary, startWord, endWord);

        // Assert
        Assert.IsEmpty(result); // No transformation is possible with an empty dictionary
    }

    // Test 6: Verify when startWord and endWord are the same
    [Test]
    public void Test_BreadthFirstSearch_StartWordEqualsEndWord()
    {
        // Arrange
        var dictionary = new HashSet<string> { "spin", "spit", "spot" };
        string startWord = "spin";
        string endWord = "spin";

        // Act
        List<string> result = BreadthFirstSearch.FindTransformationUsingBFS(dictionary, startWord, endWord);

        // Assert
        Assert.That(result.Count, Is.GreaterThan(0));
        CollectionAssert.AreEqual(new List<string> { "spin" }, result);
    }

    // Test 7: Verify transformation when no letter change is possible
    [Test]
    public void Test_BreadthFirstSearch_NoLetterChangePossible()
    {
        // Arrange
        var dictionary = new HashSet<string> { "spin", "spot" };
        string startWord = "spin";
        string endWord = "spot";

        // Act
        List<string> result = BreadthFirstSearch.FindTransformationUsingBFS(dictionary, startWord, endWord);

        // Assert
        Assert.IsEmpty(result); // No intermediate steps are available to transform spin to spot
    }

    // Test 8: Verify when multiple shortest paths exist
    [Test]
    public void Test_BreadthFirstSearch_MultipleShortestPaths()
    {
        // Arrange
        var dictionary = new HashSet<string> { "spin", "spit", "spot", "span", "spat" }; // Not in alphabetical order
        string startWord = "spin";
        string endWord = "spot";

        // Act
        List<string> result = BreadthFirstSearch.FindTransformationUsingBFS(dictionary, startWord, endWord);

        // Assert
        Assert.That(result.Count, Is.GreaterThan(0));
        // One of the possible valid shortest paths
        CollectionAssert.AreEqual(new List<string> { "spin", "spit", "spot" }, result);
    }
}