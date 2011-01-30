using System;

namespace Knowledge.Prospector.Common.Settings
{
	public class OneFileSettings
	{
		private string _fileName;
		private bool _enabled;
		private string _language;

		public OneFileSettings(string fileName, bool enabled, string language)
		{
			_fileName = fileName;
			_enabled = enabled;
			_language = language;
		}

		public OneFileSettings(string fileName, bool enabled)
			: this(fileName, enabled, string.Empty)	{ }

		public OneFileSettings(string fileName)
			: this(fileName, true) { }

		public OneFileSettings()
			: this(string.Empty, false) { }

		public string FileName
		{
			get { return _fileName; }
			set { _fileName = value; }
		}

		public bool Enabled
		{
			get { return _enabled; }
			set { _enabled = value; }
		}

		public string Language
		{
			get { return _language; }
			set { _language = value; }
		}

		public string[] Languages
		{
			get
			{
				return Language.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
			}
		}

		public override string ToString()
		{
			return FileName;
		}
	}
}
