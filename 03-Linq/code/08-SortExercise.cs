using System.Linq;
using System.Text.RegularExpressions;
using System;
using NUnit.Framework;

namespace Courses.Linq
{
	[TestFixture]
	public class SortExercise
	{
		/*

		##Задача: Словарь текста

		Дан текст, нужно составить лексикографически упорядоченный список всех слов, которые встречаются в этом тексте.

		Слова выводить в нижнем регистре.

		### Краткая справка
		  * `IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> items, Func<T, K> keySelector)`
		  * `IOrderedEnumerable<T> OrderByDescending<T>(this IEnumerable<T> items, Func<T, K> keySelector)`
		  * `IOrderedEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> items, Func<T, K> keySelector)`
		  * `IOrderedEnumerable<T> ThenByDescending<T>(this IOrderedEnumerable<T> items, Func<T, K> keySelector)`
		*/

		public string[] GetSortedWords(params string[] textLines)
		{
			throw new NotImplementedException();
		}

		[Test]
		public void Test()
		{
			var words = GetSortedWords(
				"Hello, hello, hello, how low",
				"",
				"With the lights out, it's less dangerous",
				"Here we are now; entertain us",
				"I feel stupid and contagious",
				"Here we are now; entertain us",
				"A mulatto, an albino, a mosquito, my libido...",
				"Yeah, hey");
			Assert.That(words,
				Is.EqualTo(new[]
				{
					"a", "albino", "an", "and", "are", "contagious", "dangerous",
					"entertain", "feel", "hello", "here", "hey", "how",
					"i", "it", "less", "libido", "lights", "low",
					"mosquito", "mulatto", "my", "now", "out", "s", "stupid",
					"the", "us", "we", "with", "yeah"
				}));
		}
	}
}