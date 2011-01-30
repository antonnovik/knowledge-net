using System;
using System.Collections.Generic;

namespace Knowledge.Prospector.Common
{
	public static class Splitter
	{
		/// <summary>
		/// Splits line to left part and all other.
		/// </summary>
		/// <param name="line">string what will be splitted.</param>
		/// <param name="openBracket">Type of open bracket.</param>
		/// <param name="closeBracket">Type of close bracket.</param>
		/// <param name="oper">Binary operator char representation.</param>
		/// <param name="omit">Omitted char and next char from omitted will not be prased.</param>
		/// <returns>True if operator found, otherwise false.</returns>
		public static bool Split(string line, char openBracket, char closeBracket, char omit, char oper, out string left, out string right)
		{
			line = line.Trim();
			int CountOfReadedBrackets = 0;
			int index = 0;
			left = string.Empty;
			right = string.Empty;
			for (; index < line.Length; index++)
			{
				left += line[index];

				if (line[index] == omit)
				{
					index++;
					continue;
				}
				if (line[index] == openBracket)
				{
					CountOfReadedBrackets++;
					continue;
				}
				if (line[index] == closeBracket)
				{
					CountOfReadedBrackets--;
					continue;
				}
				if (CountOfReadedBrackets == 0 && line[index] == oper)
				{
					if (!(index < line.Length - 1))
						throw new Exception("Error in parsing of entity logic");

					left = line.Substring(0, index);
					right = line.Substring(index + 1);
					return true;
				}
			}
			return false;
		}

		public static string TrimBracket(string line, char openBracket, char closeBracket)
		{
			if (line == null || line == string.Empty)
				return line;

			line = string.Format("{0}{1}{2}", openBracket, line, closeBracket);
			while (line != string.Empty && line[0] == openBracket && line[line.Length - 1] == closeBracket)
			{
				line = line.Substring(1, line.Length - 2).Trim();
			}
			return line;
		}

		/// <summary>
		/// Splits line to pieces.
		/// </summary>
		/// <param name="line"></param>
		/// <param name="openBracket"></param>
		/// <param name="closeBracket"></param>
		/// <param name="omit"></param>
		/// <param name="splitChar"></param>
		/// <returns></returns>
		public static string[] Split(string line, char openBracket, char closeBracket, char omit, char splitChar)
		{
			line = line.Trim();
			List<string> tempList = new List<string>();
			int cuntOfReadedBrackets = 0;
			string piece = string.Empty;
			for (int i = 0; i < line.Length; i++)
			{
				piece += line[i];
				if (line[i] == omit)
				{
					i++;
					if (!(i < line.Length))
						throw new ArgumentException("There are no character after '\\' in string " + line, "line");
					piece += line[i];
					continue;
				}
				if (line[i] == openBracket)
				{
					cuntOfReadedBrackets++;
				}
				if (line[i] == closeBracket)
				{
					cuntOfReadedBrackets--;
				}
				if (cuntOfReadedBrackets == 0 && line[i] == splitChar)
				{
					piece = piece.Trim();
					if (piece != string.Empty)
					{
						tempList.Add(piece);
						piece = string.Empty;
					}
				}
			}
			if (piece != string.Empty)
			{
				tempList.Add(piece);
			}

			return tempList.ToArray();
		}
	}
}
