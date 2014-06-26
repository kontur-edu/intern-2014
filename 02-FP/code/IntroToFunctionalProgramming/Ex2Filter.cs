using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace IntroToFunctionalProgramming
{
	public static class HigherOrderFunctions
	{
		public static IEnumerable<U> Map<T, U>(Func<T, U> f, IEnumerable<T> collection)
		{
			foreach (var x in collection)
			{
				yield return f(x);
			}
		}
	}

	[TestFixture]
	internal class HigherOrderFunctionsTests
	{
		[Test]
		public void MapTest()
		{
			Assert.AreEqual(new[] {1, 4, 9}, HigherOrderFunctions.Map(x => x*x, new[] {1, 2, 3}));
			Assert.AreEqual(new[] {-1, -2, -3}, HigherOrderFunctions.Map(x => -x, new[] {1, 2, 3}));
			//or, you can use LINQ:
			Assert.AreEqual(new[] {-1, -2, -3}, new[] {1, 2, 3}.Select(x => -x));
		}

		//[Test]
		//public void FilterTest()
		//{
		//    Assert.AreEqual(new[] { 2, 4, 6, 8 }, HoFs.Filter(x => x % 2 == 0, Enumerable.Range(1, 9).ToArray()));
		//    Assert.AreEqual(new [] {"voiddiov", "kayak", "radar"}, HoFs.Filter(x => x == new string(x.Reverse().ToArray()),
		//        new[] { "voiddiov", "doivvoi", "kayak", "cat", "hat", "radar" }));
		//}
	}
}