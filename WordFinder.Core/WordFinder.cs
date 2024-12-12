namespace WordFinder.Core
{
    public class WordFinder
    {
        private readonly char[,] _matrix;
        public int Rows { get; }
        public int Columns { get; }
        public char[,] Matrix => (char[,])_matrix.Clone();

        public WordFinder(IEnumerable<string> matrix)
        {
            if (matrix == null || !matrix.Any())
            {
                throw new ArgumentException("The matrix cannot be null or empty.");
            }

            Rows = matrix.Count();
            if (Rows > 64)
            {
                throw new ArgumentException("The matrix cannot have more than 64 rows.");
            }

            Columns = matrix.First().Length;
            if (Columns > 64)
            {
                throw new ArgumentException("The matrix cannot have more than 64 columns.");
            }

            if (matrix.Any(row => row.Length != Columns))
            {
                throw new ArgumentException("All rows in the matrix must have the same length.");
            }

            // Initialize and populate the 2D matrix
            _matrix = new char[Rows, Columns];
            int rowIndex = 0;
            foreach (var row in matrix)
            {
                for (int colIndex = 0; colIndex < Columns; colIndex++)
                {
                    _matrix[rowIndex, colIndex] = row[colIndex];
                }
                rowIndex++;
            }
        }
        
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            //Dictionary to store the occurrences of each word
            var wordCounts = new Dictionary<string, int>();

            foreach (var word in wordstream)
            {
                int count = CountOccurrences(word);
                if (count > 0) // We only save words with occurrences
                {
                    wordCounts[word] = count;
                }
            }

            // Sort by the number of occurrences in descending order and take the first 10            
            return wordCounts
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key)
                .Take(10)
                .Select(kvp => kvp.Key); 
        }

        private int CountOccurrences(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return 0;
            }

            int count = 0;

            // Horizontal Search (rows)
            for (int row = 0; row < Rows; row++)
            {
                if (SearchInRow(row, word))
                {
                    count++;
                }
            }

            // Vertical Search (columns)
            for (int col = 0; col < Columns; col++)
            {
                if (SearchInColumn(col, word))
                {
                    count++;
                }
            }

            return count;
        }

        private bool SearchInRow(int row, string word)
        {
            for (int col = 0; col <= Columns - word.Length; col++)
            {
                if (MatchInMatrix(row, col, word, isHorizontal: true))
                {
                    return true; // Count only once per row
                }
            }
            return false;
        }

        private bool SearchInColumn(int col, string word)
        {
            for (int row = 0; row <= Rows - word.Length; row++)
            {
                if (MatchInMatrix(row, col, word, isHorizontal: false))
                {
                    return true; // Count only once per column
                }
            }
            return false;
        }

        private bool MatchInMatrix(int row, int col, string word, bool isHorizontal)
        {
            for (int i = 0; i < word.Length; i++)
            {
                char current = isHorizontal ? _matrix[row, col + i] : _matrix[row + i, col];
                if (current != word[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
