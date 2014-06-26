using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace Courses.Linq
{
	[TestFixture]
	public class SelectManyExercise
	{
		/*

		##Задача: SelectMany

		Вам дан список всех классов в школе. Нужно получить спиок всех учащихся всех классов.
		
		Учебный класс определен так:
		*/

		public class Classroom
		{
			public List<string> Students = new List<string>();
		}

		/*

		Без использования Linq, решение могло бы выглядеть так:
		*/

		[Test]
		public string[] GetAllStudents_NoLinq(Classroom[] classes)
		{
			var allStudents = new List<string>();
			foreach (var classroom in classes)
			{
				foreach (var student in classroom.Students)
				{
					allStudents.Add(student);
				}
			}
			return allStudents.ToArray();
		}

		/*
		Напишите решение этой задачи с помощью Linq в одно выражение.

		### Краткая справка
		  * `IEnumerable<R> SelectMany(this IEnumerable<T> items, Func<T, IEnumerable<R>> f)`
		  * `T[] ToArray(this IEnumerable<T> items)`
		
		*/

		public string[] GetAllStudents(Classroom[] classes)
		{
			throw new NotImplementedException();
		}

		[Test]
		public void Test()
		{
			Classroom[] classes =
			{
				new Classroom {Students = {"Pavel", "Ivan", "Petr"},},
				new Classroom {Students = {"Anna", "Ilya", "Vladimir"},},
				new Classroom {Students = {"Bulat", "Alex", "Galina"},}
			};

			var allStudents = GetAllStudents(classes);

			Assert.That(
				allStudents,
				Is.EquivalentTo(new[]
				{
					"Pavel", "Ivan", "Petr", "Anna", "Ilya",
					"Vladimir", "Bulat", "Alex", "Galina"
				}));
		}
	}
}