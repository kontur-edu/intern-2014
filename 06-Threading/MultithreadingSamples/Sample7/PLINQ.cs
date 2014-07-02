using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using NUnit.Framework;

namespace Sample7
{
    public class PLINQ
    {
        [Test]
        public void TestPLINQSimple()
        {
            var random = new Random(1);

            var data = new int[100000];
            for (int i = 0; i < data.Length; i++)
                data[i] = random.Next();

            var sw = Stopwatch.StartNew();
            data
                .Select(i =>
                {
                    Thread.SpinWait(10000);
                    return random.Next();
                })
                .ToArray();
            Console.WriteLine("LINQ: {0}", sw.Elapsed);

            sw.Restart();
            data
                .AsParallel()
                .AsOrdered()
                .WithDegreeOfParallelism(8)
                .Select(i =>
                        {
                            Thread.SpinWait(10000);
                            return random.Next();
                        })
                .AsSequential()
                .ToArray();
            Console.WriteLine("PLINQ: {0}", sw.Elapsed);
        }
    }
}
