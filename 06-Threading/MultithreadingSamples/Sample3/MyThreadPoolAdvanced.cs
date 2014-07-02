using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample3
{
	class MyThreadPoolAdvanced<T>
	{
		private List<Thread> threads;
		Queue<Item<T>> items = new Queue<Item<T>>();

		public MyThreadPoolAdvanced(int threadsCount)
		{
			threads = new List<Thread>(threadsCount);
			for(int i = 0; i < threadsCount; i++)
			{
				var thread = new Thread(ThreadFunc) { IsBackground = true };
				threads.Add(thread);
				thread.Start();
			}
		}

		public void Add(Action<T> action, T param)
		{
			var item = new Item<T> { Action = action, Param = param };
			lock(items)
			{
				items.Enqueue(item);
				Monitor.Pulse(items);
			}
		}

		private void ThreadFunc()
		{
			while(true)
			{
				Item<T> item;
				lock(items)
				{
					while(items.Count == 0)
						Monitor.Wait(items);

					item = items.Dequeue();
				}

				try
				{
					item.Action(item.Param);
				}
				catch(Exception e)
				{
					Console.Error.WriteLine(e);
				}
			}
		}

		class Item<TT>
		{
			public Action<TT> Action { get; set; }
			public TT Param { get; set; }
		}
	}
}
