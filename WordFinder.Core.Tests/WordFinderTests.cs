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
    public void WordFinder_ShouldThrowException_WhenMatrixIsNullOrEmpty()
    {
        // Arrange
        IEnumerable<string> nullMatrix = null;
        var emptyMatrix = Enumerable.Empty<string>();

        // Act & Assert
        var ex1 = Assert.Throws<ArgumentException>(() => new WordFinder(nullMatrix));
        Assert.That(ex1.Message, Is.EqualTo("The matrix cannot be null or empty."));

        var ex2 = Assert.Throws<ArgumentException>(() => new WordFinder(emptyMatrix));
        Assert.That(ex2.Message, Is.EqualTo("The matrix cannot be null or empty."));
    }

    [Test]
    public void WordFinder_ShouldThrowException_WhenMatrixExceedsMaxRows()
    {
        // Arrange
        var largeMatrix = Enumerable.Range(1, 65).Select(_ => new string('a', 5)); // 65 rows, each of 5 characters.

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new WordFinder(largeMatrix));
        Assert.That(exception.Message, Is.EqualTo("The matrix cannot have more than 64 rows."));
    }

    [Test]
    public void WordFinder_ShouldThrowArgumentException_WhenMatrixHasMoreThan64Columns()
    {
        // Arrange
        var invalidMatrix = Enumerable.Range(1, 5).Select(_ => new string('a', 65)); // 5 rows, each of 65 characters.

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new WordFinder(invalidMatrix));
        Assert.That(exception.Message, Is.EqualTo("The matrix cannot have more than 64 columns."));
    }

    [Test]
    public void WordFinder_ShouldThrowArgumentException_WhenRowsHaveDifferentLengths()
    {
        // Arrange
        var invalidMatrix = new List<string>
        {
            "abcd",
            "efghij",
            "ijkl"
        };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new WordFinder(invalidMatrix));
        Assert.That(exception.Message, Is.EqualTo("All rows in the matrix must have the same length."));
    }

    [Test]
    public void WordFinder_ShouldInitializeMatrixWithCorrectValues()
    {
        // Arrange
        var inputMatrix = new List<string>
        {
            "abcd",
            "efgh",
            "ijkl",
            "mnop"
        };

        // Act
        var wordFinder = new WordFinder(inputMatrix);

        // Assert
        Assert.That(wordFinder.Rows, Is.EqualTo(4), "Number of rows should be 4.");
        Assert.That(wordFinder.Columns, Is.EqualTo(4), "Number of columns should be 4.");

        var expectedMatrix = new char[,]
        {
            { 'a', 'b', 'c', 'd' },
            { 'e', 'f', 'g', 'h' },
            { 'i', 'j', 'k', 'l' },
            { 'm', 'n', 'o', 'p' }
        };

        CollectionAssert.AreEqual(expectedMatrix, wordFinder.Matrix, "The 2D matrix content is not as expected.");
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