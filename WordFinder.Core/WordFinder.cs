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
            throw new NotImplementedException();
        }
    }
}
