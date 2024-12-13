using BenchmarkDotNet.Running;

namespace WordCountBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {         
            BenchmarkRunner.Run<WordCountBenchmark>();
        }
    }
}
