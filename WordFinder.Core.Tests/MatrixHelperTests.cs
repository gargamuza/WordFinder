namespace WordFinder.Core.Tests
{
    [TestFixture]
    public class MatrixHelperTests
    {
        #region CountOccurrences

        [Test]
        public void CountOccurrences_ShouldReturnZero_WhenWordIsNullOrEmpty()
        {
            var matrix = new char[,]
            {
                { 'A', 'B', 'C' },
                { 'D', 'E', 'F' },
                { 'G', 'H', 'I' }
            };

            Assert.That(MatrixHelper.CountOccurrences(matrix, null), Is.EqualTo(0));
            Assert.That(MatrixHelper.CountOccurrences(matrix, ""), Is.EqualTo(0));
        }

        [Test]
        public void CountOccurrences_ShouldReturnZero_WhenWordNotFound()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'D', 'O', 'G' },
                { 'F', 'O', 'X' }
            };

            Assert.That(MatrixHelper.CountOccurrences(matrix, "ELEPHANT"), Is.EqualTo(0));
        }

        [Test]
        public void CountOccurrences_ShouldReturnCorrectCount_ForHorizontalOccurrences()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'D', 'O', 'G' },
                { 'F', 'O', 'X' }
            };

            Assert.That(MatrixHelper.CountOccurrences(matrix, "CAT"), Is.EqualTo(1));
            Assert.That(MatrixHelper.CountOccurrences(matrix, "DOG"), Is.EqualTo(1));
            Assert.That(MatrixHelper.CountOccurrences(matrix, "FOX"), Is.EqualTo(1));
        }


        [Test]
        public void CountOccurrences_ShouldReturnCorrectCount_ForVerticalOccurrences()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'D', 'O', 'G' },
                { 'F', 'O', 'X' }
            };

            Assert.That(MatrixHelper.CountOccurrences(matrix, "CDF"), Is.EqualTo(1));
            Assert.That(MatrixHelper.CountOccurrences(matrix, "AOO"), Is.EqualTo(1));
            Assert.That(MatrixHelper.CountOccurrences(matrix, "TGX"), Is.EqualTo(1));
        }

        [Test]
        public void CountOccurrences_ShouldHandleSingleCharacterWord()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'D', 'O', 'G' },
                { 'F', 'O', 'X' }
            };

            Assert.That(MatrixHelper.CountOccurrences(matrix, "C"), Is.EqualTo(2));
            Assert.That(MatrixHelper.CountOccurrences(matrix, "O"), Is.EqualTo(3));
            Assert.That(MatrixHelper.CountOccurrences(matrix, "X"), Is.EqualTo(2));
        }

        [Test]
        public void CountOccurrences_ShouldReturnCorrectCount_ForRepeatedWords()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'C', 'A', 'T' },
                { 'C', 'A', 'T' }
            };

            Assert.That(MatrixHelper.CountOccurrences(matrix, "CAT"), Is.EqualTo(3));
            Assert.That(MatrixHelper.CountOccurrences(matrix, "CCC"), Is.EqualTo(1));
        }

        #endregion

        #region SearchInRow

        [Test]
        public void SearchInRow_ShouldReturnTrue_WhenWordExists()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'D', 'O', 'G' },
                { 'F', 'O', 'X' }
            };

            Assert.IsTrue(MatrixHelper.SearchInRow(matrix, 0, "CAT"));
            Assert.IsTrue(MatrixHelper.SearchInRow(matrix, 1, "DOG"));
            Assert.IsTrue(MatrixHelper.SearchInRow(matrix, 2, "FOX"));
        }

        [Test]
        public void SearchInRow_ShouldReturnFalse_WhenWordDoesNotExist()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'D', 'O', 'G' },
                { 'F', 'O', 'X' }
            };

            Assert.IsFalse(MatrixHelper.SearchInRow(matrix, 0, "DOG"));
            Assert.IsFalse(MatrixHelper.SearchInRow(matrix, 1, "CAT"));
            Assert.IsFalse(MatrixHelper.SearchInRow(matrix, 2, "DOG"));
        }

        [Test]
        public void SearchInRow_ShouldHandlePartialMatches()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T', 'D', 'O', 'G' }
            };

            Assert.IsTrue(MatrixHelper.SearchInRow(matrix, 0, "DOG"));
            Assert.IsTrue(MatrixHelper.SearchInRow(matrix, 0, "CAT"));
            Assert.IsFalse(MatrixHelper.SearchInRow(matrix, 0, "FOO"));
        }

        #endregion

        #region SearchInColumn

        [Test]
        public void SearchInColumn_ShouldReturnTrue_WhenWordExistsInColumn()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'C', 'A', 'T' },
                { 'C', 'A', 'T' }
            };
           
            Assert.IsTrue(MatrixHelper.SearchInColumn(matrix, 0, "CCC"));
            Assert.IsTrue(MatrixHelper.SearchInColumn(matrix, 1, "AAA"));
            Assert.IsTrue(MatrixHelper.SearchInColumn(matrix, 2, "TTT"));
        }

        [Test]
        public void SearchInColumn_ShouldReturnFalse_WhenWordDoesNotExist()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'D', 'O', 'G' },
                { 'F', 'O', 'X' }
            };

            Assert.IsFalse(MatrixHelper.SearchInColumn(matrix, 0, "DOG"));
            Assert.IsFalse(MatrixHelper.SearchInColumn(matrix, 1, "CAT"));
            Assert.IsFalse(MatrixHelper.SearchInColumn(matrix, 2, "FOO"));
        }

        [Test]
        public void SearchInColumn_ShouldReturnTrue_WhenWordExistsAtStartOfColumn()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'D', 'A', 'T' },
                { 'F', 'A', 'T' }
            };

            Assert.IsTrue(MatrixHelper.SearchInColumn(matrix, 0, "CDF"));
            Assert.IsFalse(MatrixHelper.SearchInColumn(matrix, 1, "DOG"));
        }

        [Test]
        public void SearchInColumn_ShouldHandleSingleCharacterWord()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'D', 'O', 'G' },
                { 'F', 'O', 'X' }
            };
           
            Assert.IsTrue(MatrixHelper.SearchInColumn(matrix, 0, "C"));
            Assert.IsTrue(MatrixHelper.SearchInColumn(matrix, 1, "O"));
            Assert.IsTrue(MatrixHelper.SearchInColumn(matrix, 2, "X"));
        }

        [Test]
        public void SearchInColumn_ShouldReturnFalse_WhenColumnIsShorterThanWord()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'D', 'O', 'G' }
            };

            Assert.IsFalse(MatrixHelper.SearchInColumn(matrix, 0, "DOGS"));
        }

        #endregion

        #region MatchInMatrix

        [Test]
        public void MatchInMatrix_ShouldReturnTrue_WhenWordMatchesHorizontally()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T', 'D' },
                { 'D', 'O', 'G', 'S' },
                { 'F', 'O', 'X', 'E' }
            };
          
            Assert.IsTrue(MatrixHelper.MatchInMatrix(matrix, 0, 0, "CAT", true));
            Assert.IsTrue(MatrixHelper.MatchInMatrix(matrix, 1, 1, "OG", true));
        }

        [Test]
        public void MatchInMatrix_ShouldReturnTrue_WhenWordMatchesVertically()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'C', 'A', 'T' },
                { 'C', 'A', 'T' }
            };

            Assert.IsTrue(MatrixHelper.MatchInMatrix(matrix, 0, 0, "CCC", false));
            Assert.IsTrue(MatrixHelper.MatchInMatrix(matrix, 0, 1, "AAA", false));
        }

        [Test]
        public void MatchInMatrix_ShouldReturnFalse_WhenWordDoesNotMatchHorizontally()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'D', 'O', 'G' },
                { 'F', 'O', 'X' }
            };
          
            Assert.IsFalse(MatrixHelper.MatchInMatrix(matrix, 0, 0, "DOG", true));
            Assert.IsFalse(MatrixHelper.MatchInMatrix(matrix, 1, 0, "CAT", true));
        }

        [Test]
        public void MatchInMatrix_ShouldReturnFalse_WhenWordDoesNotMatchVertically()
        {
            var matrix = new char[,]
            {
                { 'C', 'A', 'T' },
                { 'D', 'O', 'G' },
                { 'F', 'O', 'X' }
            };
           
            Assert.IsFalse(MatrixHelper.MatchInMatrix(matrix, 0, 0, "COF", false));
            Assert.IsFalse(MatrixHelper.MatchInMatrix(matrix, 0, 1, "AOF", false));
        }

        #endregion
    }
}
