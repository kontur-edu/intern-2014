using System;
using System.Collections.Generic;


namespace parse
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var parser = new Parser();
			foreach (var field in parser.SplitToFields(Console.ReadLine()))
			{
				Console.WriteLine(field);
			}
		}
	}

	public class Parser
	{
		public string[] SplitToFields(string line)
		{
			var pos = 0;
			var res = new List<string>();
			while (pos < line.Length)
			{
				while (pos < line.Length && line[pos] == ' ')
					pos++;
				if (pos >= line.Length) break;
				var start = pos;
				if (line[pos] == '"')
				{
					pos++;
					while (pos < line.Length)
					{
						var ch = line[pos++];
						if (ch == '"')
							break;
						if (ch == '\\')
							pos++;
					}
					res.Add(line.Substring(start, pos - start));
				}
				else
				{
					while (pos < line.Length && line[pos] != ' ')
						pos++;
					res.Add(line.Substring(start, pos - start));
				}
			}
			return res.ToArray();
		}
	}
}