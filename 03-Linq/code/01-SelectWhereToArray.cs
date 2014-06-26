using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Courses.Linq
{
	public class SelectWhereToArray
	{
		/*
		##Основы Linq

		В основе Linq лежит интерфейс __последовательности__ `IEnumerable<T>`. 
		Последовательность — это абстракция чего-то, что можно начать перечислять, 
		и переходить от текущего элемента к следующему пока последовательность не закончится.

		Массивы, `List`, `Dictionary`, `HashSet` — все эти коллекции реализуют интерфейс последовательности.

		Для всех `IEnumerable` в пространстве имен `System.Linq` определено множество полезных extension-методов. 

		Начнем знакомство с самыми основными. (если вы с ними уже знакомы — сразу переходите к упражнению)

		## Фильтрация и преобразование

		`Where` используется для фильтрации перечисляемого. Он принимает в качестве параметра функцию-предикат 
		и возвращает новое перечисляемое, состоящее только из тех элементов исходного перечисляемого, на которых предикат вернул true.
		
		Вот его полная сигнатура:

		`IEnumerable<T> Where(this IEnumerable<T> items, Func<T, bool> predicate)`

		`Select` используется для поэлементного преобразования перечисляемого. Он принимает в качестве параметра преобразующую функцию 
		и возвращает новое перечисляемое, полученное применением этой функции к каждому элементу исходного перечисляемого.
		
		`IEnumerable<R> Select(this IEnumerable<T> items, Func<T, R> map)`

		`Take` обрезает последовательность, после указанного количества элементов.
		
		`IEnumerable<T> Take(this IEnumerable<T> items, int count)`

		`Skip` обрезает последовательность, пропуская указанное количество элементов сначала.
		
		`IEnumerable<T> Skip(this IEnumerable<T> items, int count)`

		`ToArray` и `ToList` используются для преобразования `IEnumerable<T>` в массив `T[]` или в `List<T>`, соответственно.
		
		Ещё раз отметим, что все эти методы не меняют исходную коллекцию, а возвращают новую последовательность.

		*/
		[Test]
		public void BasicFunctions()
		{
			int[] numbers = {1, 2, 3, 4, 5, 6, 7, 8, 9};

			IEnumerable<int> even = numbers.Where(x => x%2 == 0);
			IEnumerable<int> squares = even.Select(x => x*x);
			IEnumerable<int> squaresWithoutOne = squares.Skip(1);
			IEnumerable<int> secondAndThirdSquares = squaresWithoutOne.Take(2);
			int[] result = secondAndThirdSquares.ToArray();

			Assert.AreEqual(result, new[] {16, 36});
		}

		/*
		Несколько последовательных действий можно объединять в одну цепочку вызовов. 
		Такой прием называется [Method Chaining](http://en.wikipedia.org/wiki/Method_chaining).
		Для улучшения читаемости вашего кода лучше каждый вызов метода помещать в отдельную строку.
		*/

		[Test]
		public void MethodChaining()
		{
			Assert.AreEqual(
				new[] {1, 2, 3, 4, 5, 6, 7, 8, 9}
					.Where(x => x%2 == 0)
					.Select(x => x*x)
					.Skip(1)
					.Take(2)
					.ToArray(),
				new[] {16, 36});
		}

		/*
		Method chaining делает код компактнее, но скрывает информацию о типах и семантике промежуточных значений. 
		Иногда всё же стоит оставлять вспомогательные переменные, чтобы сделать код более читаемым.
		
		В коде ниже имя переменной `girls` помогает понять неочевидную задумку автора, который (наивно) считает, 
		что все женские имена заканчиваются на 'a':
		*/

		[Test]
		public void MoreReadable()
		{
			var people = new[] {"Pavel", "", "Yuriy", null, "Michail", "Aleksey", "Dasha", "Irina"}
				.Where(name => !string.IsNullOrEmpty(name));
			var girls = people
				.Where(name => name[name.Length - 1] == 'a');

			Assert.AreEqual(girls, new[] {"Dasha", "Irina"});
		}
	}
}