namespace WordFinder.Core.Tests;

[TestFixture]
public class WordFinderTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Constructor_ShouldInitializeProperly_WhenMatrixIsValid()
    {
        // Arrange
        var matrix = new List<string> { "abcd", "efgh", "ijkl", "mnop" };

        // Act
        var wordFinder = new WordFinder(matrix);

        // Assert
        Assert.IsNotNull(wordFinder, "WordFinder instance should not be null.");
    }

    [Test]
    public void Find_ShouldThrowNotImplementedException_WhenCalled()
    {
        // Arrange
        var matrix = new List<string> { "abcd", "efgh", "ijkl", "mnop" };
        var wordFinder = new WordFinder(matrix);
        var wordstream = new List<string> { "word1", "word2" };

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => wordFinder.Find(wordstream),
            "Find should throw a NotImplementedException.");
    }
}