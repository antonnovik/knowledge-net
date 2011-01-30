using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Knowledge.Prospector.Common.Exceptions;

namespace Knowledge.Prospector.Common.Utilites
{
	public static class AssertUtils
	{
		public static void AssertParamNotNull(object paramValue, string paramName)
		{
			if (paramValue == null)
				throw new ArgumentNullException(paramName);
		}
		
		public static void AssertXmlAttributRequired(XmlNode node, string attributeName)
		{
			if (node.Attributes[attributeName] == null)
				throw new MissingXmlAttributeException(attributeName);
		}
	}
}
