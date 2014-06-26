using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace IntroToFunctionalProgramming
{
	internal static class Immutability
	{
		public static int RaceCondition()
		{
			var x = 1;
			var th1 = new Thread(() => x++);
			var th2 = new Thread(() => x = x + 2);
			var threads = new[] {th1, th2};
			Parallel.ForEach(threads, thread => thread.Start());
			return x;
		}

		public static int Immutable(int x)
		{
			var task1 = Task<int>.Factory.StartNew(() => x + 1);
			var task2 = Task<int>.Factory.StartNew(() => x + 2);
			return task1.Result + task2.Result - x; // x + 1 + x + 2 = 2x + 3
		}

		public static int smthng = 0;

		public static int DirtyFunction(int x)
		{
			smthng++;
			Console.WriteLine("I've been called!");
			return x + smthng;
		}

		public static int SimplePureFunction(int x)
		{
			return x*(x/10 + 1);
		}

		public static int PureFunction(int x)
		{
			var z = x/10;
			z++;
			return z*x;
		}
	}

	public class MutableCat
	{
		public bool Hungry { get; set; }

		public void Feed()
		{
			Hungry = false;
		}
	}

	public class ImmutableCat
	{
		public bool Hungry { get; private set; }

		public ImmutableCat(bool isHungry = true)
		{
			Hungry = isHungry;
		}

		public ImmutableCat Feed()
		{
			return new ImmutableCat(false);
		}
	}

	[TestFixture]
	internal class ImmutabilityTests
	{
		[Test]
		public void RaceConditionTest()
		{
			Assert.AreEqual(4, Immutability.RaceCondition());
		}

		[Test]
		public void ImmutableTest()
		{
			Assert.AreEqual(5, Immutability.Immutable(2));
			Assert.AreEqual(6, Immutability.Immutable(3));
			Assert.AreEqual(-2, Immutability.Immutable(-5));
		}

		[Test]
		public void MutableCatTest()
		{
			var cat = new MutableCat();
			cat.Feed();
			Assert.AreEqual(false, cat.Hungry);
		}

		[Test]
		public void ImmutableCatTest()
		{
			Assert.AreEqual(false, (new ImmutableCat()).Feed().Hungry);
		}

		[Test]
		public void DirtyFunction()
		{
			Assert.AreEqual(2, Immutability.DirtyFunction(1));
			Assert.AreEqual(3, Immutability.DirtyFunction(1));
		}

		[Test]
		public void PureFunction()
		{
			Assert.AreEqual(20, Immutability.PureFunction(10));
			Assert.AreEqual(20, Immutability.PureFunction(10));
			Assert.AreEqual(Immutability.SimplePureFunction(249), Immutability.PureFunction(249));
		}
	}
}