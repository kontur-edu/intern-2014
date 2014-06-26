using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace Courses.Linq
{
	[TestFixture]
	public class CartesianProductExercise
	{
		/*

		##Задача: Соседи
		
		Одно из не совсем очевидных применений `SelectMany` — это вычисление декартова произвдеения двух множеств.
		Попробуйте применить это знание при решении следующей задачи:

		Дана точка. Нужно вернуть `HashSet<Point>`, содержащий всех соседей этой точки в смысле 8-связности.

		*/

		public HashSet<Point> GetNeighbours(Point p)
		{
			int[] d = {-1, 0, 1};
			throw new NotImplementedException();
		}

		[Test]
		public void Test()
		{
			var neighbours = GetNeighbours(new Point(1, 2));

			Assert.That(
				neighbours,
				Is.EquivalentTo(new[]
				{
					new Point(0, 1), new Point(0, 2), new Point(0, 3),
					new Point(1, 1), new Point(1, 3),
					new Point(2, 1), new Point(2, 2), new Point(2, 3),
				}));
		}
	}
}