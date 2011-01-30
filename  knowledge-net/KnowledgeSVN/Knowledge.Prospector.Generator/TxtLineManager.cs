using System;
using System.Collections.Generic;
using System.IO;

namespace Knowledge.Prospector.Generator
{
	public class TxtFormat
	{
		public string Name;
		public string Format;
		public string CommentsFormat;
		public List<string> Header = new List<string>();
		public List<string> Footer = new List<string>();

		public string[] Parameters;

		public static string[] Split(string str)
		{
			return str.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
		}

		public static string[] SplitSharp(string str)
		{
			return str.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
		}
	}

	public class TxtLine
	{
		public string Comments;
		public string[] Values;

		public TxtLine(string[] values)
		{
			Values = values;
		}

		public TxtLine(string[] values, string comment)
		{
			Values = values;
			Comments = comment;
		}
	}

	public class TxtLineManager
	{
		public string[] Headers;

		public TxtFormat[] Formats;

		public List<string> Header = new List<string>();
		public List<string> Footer = new List<string>();

		protected Dictionary<string, int> HeadersHash = new Dictionary<string, int>();

		protected void HashHeaders()
		{
			for (int i = 0; i < Headers.Length; i++)
				HeadersHash[Headers[i]] = i;
		}

		protected static string[] Split(string str)
		{
			return str.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
		}

		protected static string[] SplitTab(string str)
		{
			return str.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
		}

		protected string ApplyFormat(TxtFormat format, TxtLine line)
		{
			List<string> values = new List<string>();

			foreach (string parameter in format.Parameters)
			{
				if (line.Values[HeadersHash[parameter]] == "_")
					return null;
				values.Add(line.Values[HeadersHash[parameter]]);
			}

			return string.Format(format.Format, values.ToArray());
		}

		protected static void ApplyComments(StreamWriter sw, TxtFormat format, TxtLine line)
		{
			string tab = format.CommentsFormat != null ? format.CommentsFormat : string.Empty;
			if (line.Comments != null)
			{
				sw.WriteLine("{0}/// <summary>", tab);
				sw.WriteLine("{0}/// {1}", tab, line.Comments);
				sw.WriteLine("{0}/// </summary>", tab);
			}
		}

		protected static void PrintList(StreamWriter sw, List<string> strings)
		{
			foreach (string s in strings)
				sw.WriteLine(s);
		}

		protected static void PrintWithTab(StreamWriter sw, TxtFormat format, string s)
		{
			if (format.CommentsFormat != null)
				sw.WriteLine(format.CommentsFormat + s);
			else
				sw.WriteLine(s);
		}

		protected static string GetNext(StreamReader sr)
		{
			if(sr.EndOfStream)
				return string.Empty;

			string result = sr.ReadLine();
			while (!sr.EndOfStream && result.Trim() == string.Empty || result.Trim().StartsWith("//"))
				result = sr.ReadLine();
			return result;
		}

		protected static string GetNextAndComments(StreamReader sr)
		{
			if (sr.EndOfStream)
				return string.Empty;

			string result = sr.ReadLine();
			while (!sr.EndOfStream && result.Trim() == string.Empty || (result.Trim().StartsWith("//") && !result.Trim().StartsWith("///")))
				result = sr.ReadLine();
			return result;
		}


		public TxtLine[] Read(StreamReader sr)
		{

			//Reading formats!
			List<TxtFormat> formats = new List<TxtFormat>();
			string header = GetNext(sr);

			TxtFormat format = null;
			while (header.Trim().StartsWith("#"))
			{
				string[] temps = TxtFormat.SplitSharp(header);


				switch (temps[0].Trim())
				{
					case "+":
						format.Format += "\n" + temps[1];
						break;
					case "n":
					case "*":
						format.Format = temps[1];
						break;
					case "p":
						format.Parameters = TxtFormat.Split(temps[1]);
						break;
					case "=":
						formats.Add(format);
						break;
					case "name":
						format = new TxtFormat();
						format.Name = temps[1];
						break;
					case "c":
						format.CommentsFormat = temps[1];
						break;
					case "h":
						format.Header.Add(temps[1]);
						break;
					case "f":
						format.Footer.Add(temps[1]);
						break;
					case "g_h":
						Header.Add(temps[1]);
						break;
					case "g_f":
						Footer.Add(temps[1]);
						break;
				}

				header = GetNext(sr);
			}
			Formats = formats.ToArray();

			//Reading headers!
			Headers = Split(header);
			HashHeaders();

			//reading lines!
			List<TxtLine> list = new List<TxtLine>();
			string comments = null;

			while (!sr.EndOfStream)
			{
				//Reading line from source file
				string temp = GetNextAndComments(sr);

				if (temp.StartsWith("///"))
				{
					comments = temp.Substring(3);
					continue;
				}

				//creating new instance
				list.Add(new TxtLine(SplitTab(temp), comments));
				comments = null;
			}

			return list.ToArray();
		}
	}
}
