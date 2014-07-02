using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample2
{
	class Program
	{
		static void Main(string[] args)
		{
			var mutex = new Mutex(false, "my_mytex");
			var sw = Stopwatch.StartNew();
			for(int i = 0; i < 1000 * 1000; i++)
			{
				mutex.WaitOne();
				mutex.ReleaseMutex();
			}
			Console.WriteLine(sw.ElapsedMilliseconds);



			var sem = new Semaphore(1, 1, "my_semaphore");
			sw.Restart();
			for(int i = 0; i < 1000 * 1000; i++)
			{
				sem.WaitOne();
				sem.Release();
			}
			Console.WriteLine(sw.ElapsedMilliseconds);


			var mre = new EventWaitHandle(true, EventResetMode.ManualReset, "my_manual_reset_event");
			sw.Restart();
			for(int i = 0; i < 1000 * 1000; i++)
			{
				mre.WaitOne();
				mre.Set();
			}
			Console.WriteLine(sw.ElapsedMilliseconds);


			var are = new EventWaitHandle(true, EventResetMode.AutoReset, "my_auto_reset_event");
			sw.Restart();
			for(int i = 0; i < 1000 * 1000; i++)
			{
				are.WaitOne();
				are.Set();
			}
			Console.WriteLine(sw.ElapsedMilliseconds);


			sw.Restart();
			for(int i = 0; i < 1000 * 1000; i++)
			{
				Monitor.Enter(sw);
				Monitor.Exit(sw);
			}
			Console.WriteLine(sw.ElapsedMilliseconds);

		}
	}
}
