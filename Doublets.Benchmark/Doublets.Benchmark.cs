using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using Doublets.Library;

namespace Doublets.Tests;

[TestFixture]
public class Benchmark
{
    private Stopwatch stopwatch;

    [SetUp]
    public void Setup()
    {
        // Create stopwatch - to time the execution speed
        stopwatch = new Stopwatch();
        Console.WriteLine("| Method  | Mean        |");
        Console.WriteLine("|---------|-------------|");
    }

    [TearDown] 
    public void Cleanup()
    { /* ... */ }

    // Benchmark Tests for Breadth-First Search
    // ----------------------------------------

    // Test 1: Verify a valid shortest transformation sequence exists
    [Test]
    public void Test_BreadthFirstSearch_1()
    {
        // Arrange
        var dictionary = new HashSet<string> { "spin", "spit", "spat", "spot", "span" }; // Not in alphabetical order
        string startWord = "spin";
        string endWord = "spot";

        // Act
        stopwatch.Start();
        List<string> result = BreadthFirstSearch.FindPath(dictionary, startWord, endWord);
        stopwatch.Stop();

        // Assert
        Assert.That(result.Count, Is.GreaterThan(0));
        CollectionAssert.AreEqual(new List<string> { "spin", "spit", "spot" }, result);
        // Write the speed of run to console
        TimeSpan timeElapsed = stopwatch.Elapsed;
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            timeElapsed.Hours, timeElapsed.Minutes, timeElapsed.Seconds,
            timeElapsed.Milliseconds / 10);
        Console.WriteLine($"| 1       | {elapsedTime} |");
        stopwatch.Restart();
    } 
}