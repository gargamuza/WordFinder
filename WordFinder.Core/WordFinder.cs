namespace WordFinder.Core
{
    public class WordFinder
    {
        private readonly char[,] _matrix;
        internal int Rows { get; }
        internal int Columns { get; }
        internal char[,] Matrix => (char[,])_matrix.Clone();

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
                int count = MatrixHelper.CountOccurrences(_matrix, word);
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
    }
}
