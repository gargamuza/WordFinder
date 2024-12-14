using BenchmarkDotNet.Attributes;
using WordFinder.Tools;


namespace WordCountBenchmark
{
    public class WordFinderBenchmark
    {
        private IEnumerable<string> wordstream;
        private IEnumerable<string> matrix;

        public WordFinderBenchmark()
        {
            var matrixGenerator = new MatrixGenerator();
            wordstream = matrixGenerator.ValidWords;
            matrix = matrixGenerator.Generate(rows: 64, columns: 64, maxInsertions: 10);
        }

        [Benchmark]
        public void TestFind()
        {           
            var wordFinder = new WordFinder.Core.WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
        }
    }
}
