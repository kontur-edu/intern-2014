using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample3
{
	class Program
	{
		static Stopwatch sw = new Stopwatch();

		static void Main()
		{
			var tp = new MyThreadPoolAdvanced<long>(4);

			sw.Start();
			for(int i = 0; i < 100; i++)
			{
				CheckPrime(StartNum + i);
			}
			Console.ReadLine();

			sw.Restart();
			for(int i = 0; i < 100; i++)
			{
				int i1 = i;
				tp.Add(CheckPrime, StartNum + i1);
			}
			Console.ReadLine();

			
		}

		private const long StartNum = 15485867;

		private static void CheckPrime(long n)
		{
			for(int i = 2; i <= n / 2; i++)
			{
				if(n % i == 0)
				{
					Console.WriteLine("{2}	FALSE: {0} is complex, found divisor {1}", n, i, sw.ElapsedMilliseconds);
					return;
				}
			}
			Console.WriteLine("{1}	TRUE: {0} is prime", n, sw.ElapsedMilliseconds);
		}
	}
}
