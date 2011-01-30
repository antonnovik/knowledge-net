using System;
using System.Collections.Generic;
using Knowledge.Prospector.Common;

namespace Knowledge.Prospector.Templates.EntityExpressions
{
	public static class EntityExpressionParser
	{
		public static EntityExpression[] Parse(string pattern)
		{
			return ParseMultiElementPattern(pattern);
		}

		/// <summary>
		/// Create new instance of entity regex element.
		/// </summary>
		/// <param name="pattern">String representation of element.</param>
		private static EntityExpression ParseOneElementPattern(string pattern)
		{
			pattern = pattern.Trim();

			if (pattern == null || pattern.Length == 0)
				throw new ArgumentException("Can't create new instance of pattern from empty string", "pattern");

			//Suffix
			switch (pattern[pattern.Length - 1])
			{
				case '*':
					return new MultipleEntityExpression(pattern.Remove(pattern.Length - 1));
				case '+':
					return new PositiveEntityExpression(pattern.Remove(pattern.Length - 1));
				case '?':
					return new OptionalEntityExpression(pattern.Remove(pattern.Length - 1));
				default:
					break;
			}

			if(pattern == string.Empty)
				throw new ArgumentException("Can't create new instance of EntityRegexElement from empty string", "name");

			if (pattern.StartsWith("#H.") || pattern.StartsWith("#h."))
				return new ClauseHolderEntityExpression(pattern);

			if (pattern.StartsWith("#W.") || pattern.StartsWith("#w."))
				return new WordHolderEntityExpression(pattern);

			return new OneEntityExpression(pattern);
		}

		/// <summary>
		/// Splits string with many regex elements to array of elements.
		/// </summary>
		/// <param name="pattern"></param>
		/// <returns>Array of regex elements.</returns>
		private static EntityExpression[] ParseMultiElementPattern(string pattern)
		{
			List<EntityExpression> temp = new List<EntityExpression>();
			foreach (string piece in Splitter.Split(pattern, '[', ']', '\\', ' '))
				temp.Add(ParseOneElementPattern(piece));
			return temp.ToArray();
		}

	}
}
