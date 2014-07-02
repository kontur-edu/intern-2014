using System;
using System.Collections.Generic;
using System.Threading;

namespace Sample1
{
	class Program
	{
		private static LinkedList<int> list = new LinkedList<int>();

		static void Main()
		{
			var t = new Thread(Loop);
			t.Start();
			Loop();
			t.Join();
			Console.WriteLine(list.Count);
		}

		private static void Loop()
		{
			for(int i = 0; i < 100000; i++)
			{
				list.AddLast(i);
			}
		}
	}
}
