using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Courses.Linq
{
	[TestFixture]
	public class SortTuplesExercise
	{
		/*
		##Сравнение кортежей

		Ещё одно полезное свойство кортежей — по умолчанию они сравниваются поэлементно.
		Используя этот факт, решите следующую задачу:

		##Задача: Словарь текста 2

		Дан текст, нужно составить список всех встречающихся в тексте слов, 
		упорядоченный сначала по длинне слова, а потом лексикографически.

		Запрещено использовать `ThenBy` и `ThenByDescending`.
		*/

		public List<string> GetSortedWords(string text)
		{
			throw new NotImplementedException();
		}

		[Test]
		public void Test()
		{
			var words = GetSortedWords("you?! or not you... who knows?");
			Assert.That(words, Is.EqualTo(new[] {"or", "not", "who", "you", "knows"}));
		}
	}
}