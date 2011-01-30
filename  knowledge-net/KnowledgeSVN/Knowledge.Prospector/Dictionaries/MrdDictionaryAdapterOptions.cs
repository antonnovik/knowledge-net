using System.Xml.Serialization;

namespace Knowledge.Prospector.Dictionaries
{
	[XmlRoot(ElementName = "MrdDictionary", IsNullable = true)]
	public class MrdDictionaryAdapterOptions
	{
		private string _mrdFileName;
		private string _gramTabFileName;
		private bool _enabled;
		private string _language;

		public MrdDictionaryAdapterOptions()
		{
			_mrdFileName = string.Empty;
			_gramTabFileName = string.Empty;
		}

		[XmlElement(ElementName = "MrdFileName")]
		public string MrdFileName
		{
			get { return _mrdFileName; }
			set { _mrdFileName = value; }
		}

		[XmlElement(ElementName = "GramTabFileName")]
		public string GramTabFileName
		{
			get { return _gramTabFileName; }
			set { _gramTabFileName = value; }
		}

		[XmlAttribute(AttributeName = "Enabled")]
		public bool Enabled
		{
			get { return _enabled; }
			set { _enabled = value; }
		}

		[XmlAttribute(AttributeName = "Language")]
		public string Language
		{
			get { return _language; }
			set { _language = value; }
		}
	}
}
