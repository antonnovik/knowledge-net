using System;
using System.Xml;
using Knowledge.Prospector.Common;

namespace Knowledge.Prospector.Templates.Handlers
{
	public abstract class BaseHandler
	{
		protected int[] Arguments;
		protected string[] ArgumentsDescription;

		public BaseHandler() { }

		public void ParseArguments(string arguments)
		{
			arguments = Splitter.TrimBracket(arguments, '(', ')');
			string[] args = arguments.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			Arguments = new int[args.Length];
			ArgumentsDescription = new string[args.Length];

			for (int i = 0; i < args.Length; i++)
			{
				args[i] = args[i].Trim();
				string[] argParts = args[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
				if (argParts.Length == 1)
				{
					if(!int.TryParse(argParts[0], out Arguments[i]))
						ArgumentsDescription[i] = argParts[0];
				}
				else
				{
					ArgumentsDescription[i] = argParts[0];
					Arguments[i] = int.Parse(argParts[1]);
				}
			}
		}

		public virtual void Read(XmlNode node)
		{
			ParseArguments(node.Attributes["Arguments"].Value);
		}
	}
}
