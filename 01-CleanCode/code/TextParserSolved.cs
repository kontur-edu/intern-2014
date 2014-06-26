using System.Collections.Generic;


namespace parse
{
	public class CleanParser
	{
		public string[] SplitToFields(string line)
		{
			var res = new List<string>();
			var pos = 0;
			string token;
			while ((token = ReadField(line, ref pos)) != null)
				res.Add(token);
			return res.ToArray();
		}

		private static string ReadField(string line, ref int pos)
		{
			SkipSpaces(line, ref pos);
			if (EndOfLine(line, pos)) return null;
			return IsOpenQuotation(line, pos)
				? ReadQuotedField(line, ref pos)
				: ReadSimpleField(line, ref pos);
		}

		private static bool IsOpenQuotation(string line, int pos)
		{
			return line[pos] == '"';
		}

		private static void SkipSpaces(string line, ref int pos)
		{
			while (pos < line.Length && line[pos] == ' ') pos++;
		}

		private static bool EndOfLine(string line, int pos)
		{
			return pos >= line.Length;
		}

		private static string ReadSimpleField(string line, ref int pos)
		{
			var startPos = pos;
			while (pos < line.Length && line[pos] != ' ')
				pos++;
			return line.Substring(startPos, pos - startPos);
		}

		private static string ReadQuotedField(string line, ref int pos)
		{
			var startPos = pos;
			pos++;
			while (pos < line.Length)
			{
				var ch = line[pos++];
				if (ch == '"')
					break;
				if (ch == '\\')
					pos++;
			}
			return line.Substring(startPos, pos - startPos);
		}
	}
}