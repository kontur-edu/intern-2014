using System;
using System.Collections.Generic;
using System.Linq;

namespace IntroToFunctionalProgramming
{
	internal static class Ex4FoldSolution
	{
		public static IEnumerable<IEnumerable<T>> PartitionBy<T>(Func<T, T, bool> criterion, IEnumerable<T> collection)
		{
			Func<IEnumerable<IEnumerable<T>>> makeEmptyRes = Enumerable.Empty<IEnumerable<T>>;

			var initAcc = Tuple.Create(new List<T>(), makeEmptyRes());
			var preRes = collection.Aggregate(initAcc, (acc, elem) =>
			{
				//if start position
				if (!acc.Item1.Any())
					return Tuple.Create(new List<T> {elem}, makeEmptyRes());
				//if group continues
				if (criterion(acc.Item1.Last(), elem))
				{
					acc.Item1.Add(elem);
					return Tuple.Create(acc.Item1, acc.Item2);
				}
				return Tuple.Create(new List<T> {elem}, acc.Item2.Concat(new[] {acc.Item1}));
			});
			return preRes.Item2.Concat(new[] {preRes.Item1});
		}
	}
}