using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Courses.Linq
{
	public class ReadNumbersExcercise
	{
		/*

		##Задача: Чтение из файла

		Linq удобно использовать для чтения из файла и разбора простых текстовых формат. Особенно удобно сочетать Linq с методом `File.ReadLines(filename)`.

		Предположим в файле filename в каждая строка либо пустая, либо содержит одно целое число. 
		Нужно прочитать числа из файла в массив. Решите задачу в один LINQ-запрос.

		###Пример файла
		    1
		    
		    -3
		    0

		Решение этой задачи будет использоваться следующим образом:
		*/

		public void ParseNumber_Sample()
		{
			int[] numbers = ParseNumbers(File.ReadLines("numbers.txt"));
			//...
		}


		/*
		### Краткая справка
		  * `items.Where(item => isGoodItem(item))` — фильтрация последовательности
		  * `items.Select(item => convert(item))` — поэлементное преобразование последовательности
		  * `items.ToArray()` — преобразование последовательности в массив
		*/

		public int[] ParseNumbers(IEnumerable<string> lines)
		{
			throw new NotImplementedException();
			/*
			return lines
				.Where(...)
				.Select(...)
				...
			*/
		}

		public void Test_ParseNumbers()
		{
			int[] actualNumbers = ParseNumbers(new[] {"", "1", "2", "", "42"});

			Assert.AreEqual(
				actualNumbers,
				new[] {1, 2, 42});
		}


	}
}