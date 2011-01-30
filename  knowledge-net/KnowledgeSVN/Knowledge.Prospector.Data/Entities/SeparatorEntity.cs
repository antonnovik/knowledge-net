using System;

namespace Knowledge.Prospector.Data.Entities
{
	/// <summary>
	/// Separator between words in Article. As emample '.', ';', ' ', ...
	/// </summary>
	[Serializable]
	public class SeparatorEntity : Entity, ISeparatorEntity
	{
		public SeparatorEntity(string value)
			: base(value) { }

		#region IEntity Members

		public override string Name
		{
			get { return "Separator"; }
		}

		#endregion

		/// <summary>
		/// Empty Separator ("").
		/// </summary>
		public static readonly SeparatorEntity Empty = new SeparatorEntity(string.Empty);
		/// <summary>
		/// Dot separator (".").
		/// </summary>
		public static readonly SeparatorEntity Dot = new SeparatorEntity(".");

		/// <summary>
		/// Comma separator (",").
		/// </summary>
		public static readonly SeparatorEntity Comma = new SeparatorEntity(",");

		/// <summary>
		/// Colon separator (":").
		/// </summary>
		public static readonly SeparatorEntity Colon = new SeparatorEntity(":");

		/// <summary>
		/// Semicolon separator (";").
		/// </summary>
		public static readonly SeparatorEntity Semicolon = new SeparatorEntity(";");

		/// <summary>
		/// Word space separator (" ").
		/// </summary>
		public static readonly SeparatorEntity Space = new SeparatorEntity(" ");

		/// <summary>
		/// New line separator ("\n").
		/// </summary>
		public static readonly SeparatorEntity NewLine = new SeparatorEntity("\n");

		/// <summary>
		/// Tab separator ("\t").
		/// </summary>
		public static readonly SeparatorEntity Tab = new SeparatorEntity("\t");

		/// <summary>
		/// Open round bracket ("(").
		/// </summary>
		public static readonly SeparatorEntity OpenRoundBracket = new SeparatorEntity("(");

		/// <summary>
		/// Close round bracket (")").
		/// </summary>
		public static readonly SeparatorEntity CloseRoundBracket = new SeparatorEntity(")");

		/// <summary>
		/// Open brace ("{").
		/// </summary>
		public static readonly SeparatorEntity OpenBrace = new SeparatorEntity("{");

		/// <summary>
		/// Close brace ("}").
		/// </summary>
		public static readonly SeparatorEntity CloseBrace = new SeparatorEntity("}");

		/// <summary>
		/// Open square bracket ("[").
		/// </summary>
		public static readonly SeparatorEntity OpenSquareBracket = new SeparatorEntity("[");
		
		/// <summary>
		/// Close square bracket ("]").
		/// </summary>
		public static readonly SeparatorEntity CloseSquareBracket = new SeparatorEntity("]");

		/// <summary>
		/// Open angle bracket ("&lt;").
		/// </summary>
		public static readonly SeparatorEntity OpenAngleBracket = new SeparatorEntity("<");
		
		/// <summary>
		/// Close angle bracket ("&gt;").
		/// </summary>
		public static readonly SeparatorEntity CloseAngleBracket = new SeparatorEntity(">");

		#region SeparatorEntity Members

		public static SeparatorEntity GetSeparator(string separator)
		{
			switch (separator)
			{
				case ".\n":
				case ".":
					return Dot;
				case ",":
					return Comma;
				case ":":
					return Colon;
				case ";":
					return Semicolon;
				case " ":
					return Space;
				case "\n":
					return NewLine;
				case "\t":
					return Tab;
				case "(":
					return OpenRoundBracket;
				case ")":
					return CloseRoundBracket;
				case "{":
					return OpenBrace;
				case "}":
					return CloseBrace;
				case "[":
					return OpenSquareBracket;
				case "]":
					return CloseSquareBracket;
				case "<":
					return OpenAngleBracket;
				case ">":
					return CloseAngleBracket;
				default:
					return new SeparatorEntity(separator);
				//	return Empty;
			}
		}

		public static bool IsSeparator(char sign)
		{
			switch (sign)
			{
				case '.':
				case ',':
				case ':':
				case ';':
				case ' ':
				case '\n':
				case '\t':
				case '(':
				case ')':
				case '{':
				case '}':
				case '[':
				case ']':
				case '<':
				case '>':
					//new
				case '-':
				case '#':
				case '@':
				case '&':
				case '*':
				case '=':
				case '|':
				case '/':
				case '\\':
				case '^':
				case '%':
				case '!':
				case '?':
				case '~':
				case '+':
				case '\'':
				case '\"':
					return true;
				default:
					return false;
			}
		}

		#endregion

		public override string ToString()
		{
			return string.Format("<Separator>{0}</Separator>", Value);
		}
	}
}
