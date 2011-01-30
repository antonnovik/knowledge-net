using System.Collections.Generic;
using Knowledge.Prospector.Common.Settings;

namespace Knowledge.Prospector.Dictionaries
{
	public class DictionaryManagerOptions
	{
		private List<OneFileSettings> _xmlDictionaryOptions;
		private List<MrdDictionaryAdapterOptions> _mrdDictionariesOptions;

		public DictionaryManagerOptions()
		{
			XmlDictionariesOptions = new List<OneFileSettings>();
			MrdDictionariesOptions = new List<MrdDictionaryAdapterOptions>();
		}

		public List<OneFileSettings> XmlDictionariesOptions
		{
			get
			{
				return _xmlDictionaryOptions;
			}
			set
			{
				_xmlDictionaryOptions = value;
			}
		}

		public List<MrdDictionaryAdapterOptions> MrdDictionariesOptions
		{
			get
			{
				return _mrdDictionariesOptions;
			}
			set
			{
				_mrdDictionariesOptions = value;
			}
		}

	}
}
