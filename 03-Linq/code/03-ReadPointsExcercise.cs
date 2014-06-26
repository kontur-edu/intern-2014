using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Courses.Linq
{
	[TestFixture]
	public class ReadPointsExcercise
	{
		/*

		##Задача: Чтение из файла 2

		В файле filename в каждой строке написаны две координаты точки, разделенные пробелом.
		Прочитайте файл в массив точек.
		Решите задачу в один LINQ-запрос. Постарайтесь не использовать функцию преобразования строки в число более одного раза.

		###Пример файла
		    1 -2
		    -3 4
		    0 2
		
		Написанная вами функция в дальнейшем может быть использована, например, вот так:
		*/


		public void ParseNumber_Sample()
		{
			List<Point> points = ParsePoints(File.ReadLines("points.txt"));
			//...
		}


		/*
		### Краткая справка
		  * `IEnumerable<R> Select(this IEnumerable<T> items, Func<T, R> map)`
		  * `List<T> ToList(this IEnumerable<T> items)`
		*/

		public List<Point> ParsePoints(IEnumerable<string> lines)
		{
			throw new NotImplementedException();
			/*
			return lines
				.Select(...)
				...
			*/
		}

		[Test]
		public void Test_ReadPoints()
		{
			List<Point> actualPoints = ParsePoints(new[] {"1 -2", "-3 4"});
			Assert.AreEqual(
				actualPoints,
				new[] {new Point(1, -2), new Point(-3, 4)});
		}


	}
}