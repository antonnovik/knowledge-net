using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Knowledge.Prospector.Common.Utilites
{
	public static class XmlUtils
	{
		public static XmlDocument GetNode(string xml)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			return doc;
		}
	}
}
