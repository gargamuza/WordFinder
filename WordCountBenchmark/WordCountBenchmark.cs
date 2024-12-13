using BenchmarkDotNet.Attributes;


namespace WordCountBenchmark
{
    public class WordCountBenchmark
    {
        private IEnumerable<string> wordstream;

        public WordCountBenchmark()
        {
            wordstream = WordGenerator.validWords;
        }

        [Benchmark]
        public void TestFind()
        {
            var matrix = WordGenerator.GenerateWords(rows: 15, columns: 40, maxInsertions: 10);
            var wordFinder = new WordFinder.Core.WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
        }
    }
}
