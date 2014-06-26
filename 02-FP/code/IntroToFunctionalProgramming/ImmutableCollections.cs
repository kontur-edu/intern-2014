using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using NUnit.Framework;

namespace IntroToFunctionalProgramming
{
    static class ImmutableCollections
    {
        public static ImmutableSortedSet<int> SetExample(int iterations)
        {
            var gen = new Random();
            return Enumerable.Range(1, iterations).Aggregate(ImmutableSortedSet<int>.Empty, (set, idx) => set.Add(gen.Next(10)));
        }
    }

    [TestFixture]
    internal class ImmutableCollectionsTests {

    }
}
