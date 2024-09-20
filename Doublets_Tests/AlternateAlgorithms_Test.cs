using NUnit.Framework;
using System.Collections.Generic;

namespace Doublets;

[TestFixture]
public class AlternateAlgorithms_Test
{
    [SetUp]
    public void Setup()
    {
    }

    // Unit Tests for Bidirectional BFS
    // --------------------------------

    // Test 1: Verify a valid shortest transformation sequence exists
    [Test]
    public void Test_BidirectionalBFS_ValidTransformationPath()
    {
        // Arrange
        var dictionary = new HashSet<string> { "spin", "spit", "spat", "spot", "span" };
        string startWord = "spin";
        string endWord = "spot";

        // Act
        List<string> result = BreadthFirstSearch.FindTransformationUsingBidirectionalBFS(dictionary, startWord, endWord);

        // Assert
        Assert.That(result.Count, Is.GreaterThan(0));
        CollectionAssert.AreNotEqual(new List<string> { "spin", "spit", "spot" }, result);
        CollectionAssert.AreEqual(new List<string> { "spot", "spit", "spin" }, result);
    }

    // Test 2: Verify no valid transformation path exists
    [Test]
    public void Test_BidirectionalBFS_NoValidTransformationPath()
    {
        // Arrange
        var dictionary = new HashSet<string> { "spin", "span", "spot" };
        string startWord = "spin";
        string endWord = "spot";

        // Act
        List<string> result = BreadthFirstSearch.FindTransformationUsingBidirectionalBFS(dictionary, startWord, endWord);

        // Assert
        Assert.IsEmpty(result); // No valid path from spin to spot
    }

    // Test 3: Verify the start word is not in the dictionary
    [Test]
    public void Test_BidirectionalBFS_StartWordNotInDictionary()
    {
        // Arrange
        var dictionary = new HashSet<string> { "spit", "spot", "span" };
        string startWord = "spin";
        string endWord = "spot";

        // Act & Assert
        Assert.IsEmpty(BreadthFirstSearch.FindTransformationUsingBidirectionalBFS(dictionary, startWord, endWord)); // startWord not in dictionary
    }

    // Test 4: Verify the end word is not in the dictionary
    [Test]
    public void Test_BidirectionalBFS_EndWordNotInDictionary()
    {
        // Arrange
        var dictionary = new HashSet<string> { "spin", "spit", "span" };
        string startWord = "spin";
        string endWord = "spot";

        // Act & Assert
        Assert.IsEmpty(BreadthFirstSearch.FindTransformationUsingBidirectionalBFS(dictionary, startWord, endWord)); // endWord not in dictionary
    }

    // Test 5: Verify when dictionary is empty
    [Test]
    public void Test_BidirectionalBFS_EmptyDictionary()
    {
        // Arrange
        var dictionary = new HashSet<string>(); // Empty dictionary
        string startWord = "spin";
        string endWord = "spot";

        // Act
        List<string> result = BreadthFirstSearch.FindTransformationUsingBidirectionalBFS(dictionary, startWord, endWord);

        // Assert
        Assert.IsEmpty(result); // No transformation is possible with an empty dictionary
    }

    // Test 6: Verify when startWord and endWord are the same
    [Test]
    public void Test_BidirectionalBFS_StartWordEqualsEndWord()
    {
        // Arrange
        var dictionary = new HashSet<string> { "spin", "spit", "spot" };
        string startWord = "spin";
        string endWord = "spin";

        // Act
        List<string> result = BreadthFirstSearch.FindTransformationUsingBidirectionalBFS(dictionary, startWord, endWord);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Count, Is.GreaterThan(0));
        CollectionAssert.AreEqual(new List<string> { "spin" }, result); // Should return just the start word
    }
}