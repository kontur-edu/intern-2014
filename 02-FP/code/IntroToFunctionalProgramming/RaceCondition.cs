using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace IntroToFunctionalProgramming
{
	[TestFixture]
	public class RaceCondition
	{
		[Test]
		public void Race()
		{
			var set = new HashSet<Tuple<int, int>>();
			for (int i = 0; i < 1000000; i++)
				set.Add(RaceIteration());
			foreach (var tuple in set)
				Console.WriteLine(tuple);
		}

		private static Tuple<int, int> RaceIteration()
		{
			var x = Tuple.Create(0, 0);
			Action inc1 = () => x = Tuple.Create(x.Item1 + 1, x.Item2 + 1);
			Action inc2 = () => x = Tuple.Create(x.Item1 + 2, x.Item2 + 2);
			Parallel.ForEach(new[] {inc1, inc2}, action => action());
			return x;
		}
	}
}