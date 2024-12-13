namespace WordFinder.Core
{
    internal static class MatrixHelper
    {
        internal static int CountOccurrences(char[,] matrix, string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return 0;
            }

            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            int count = 0;

            for (int row = 0; row < rows; row++)
            {
                if (SearchInRow(matrix, row, word))
                {
                    count++;
                }
            }

            for (int col = 0; col < columns; col++)
            {
                if (SearchInColumn(matrix, col, word))
                {
                    count++;
                }
            }

            return count;
        }

        internal static bool SearchInRow(char[,] matrix, int row, string word)
        {
            int columns = matrix.GetLength(1);
            for (int col = 0; col <= columns - word.Length; col++)
            {
                if (MatchInMatrix(matrix, row, col, word, isHorizontal: true))
                {
                    return true;
                }
            }
            return false;
        }

        internal static bool SearchInColumn(char[,] matrix, int col, string word)
        {
            int rows = matrix.GetLength(0);
            for (int row = 0; row <= rows - word.Length; row++)
            {
                if (MatchInMatrix(matrix, row, col, word, isHorizontal: false))
                {
                    return true;
                }
            }
            return false;
        }

        internal static bool MatchInMatrix(char[,] matrix, int row, int col, string word, bool isHorizontal)
        {
            for (int i = 0; i < word.Length; i++)
            {
                char current = isHorizontal ? matrix[row, col + i] : matrix[row + i, col];
                if (current != word[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
