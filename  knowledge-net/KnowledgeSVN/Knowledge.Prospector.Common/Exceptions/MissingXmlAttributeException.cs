using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.Prospector.Common.Exceptions
{
	public class MissingXmlAttributeException : Exception
	{
		protected static readonly string XmlAttributeName = "XmlAttributeName";
		protected static readonly string ToStringFormat = "Attribute {0} is required.";

		protected string _AttributeName;

		public string AttributeName
		{
			get { return _AttributeName; }
		}

		public MissingXmlAttributeException(string _AttributeName)
		{
			this._AttributeName = _AttributeName;
		}
		
		public override string ToString()
		{
			return string.Format(ToStringFormat, AttributeName);
		}
	}
}
