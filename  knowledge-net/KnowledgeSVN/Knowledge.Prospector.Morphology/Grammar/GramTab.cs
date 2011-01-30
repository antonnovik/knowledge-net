using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Knowledge.Prospector.Morphology.Grammar
{
	[Serializable]
	public abstract class GramTab : Dictionary<AnCode, GramTabLine>
	{
		private GramTabLineManager _lineManager;

		protected GramTab(GramTabLineManager manager)
		{
			LineManager = manager;
		}

		public GramTabLineManager LineManager
		{
			get
			{
				return _lineManager;
			}
			set
			{
				_lineManager = value;
			}
		}

		public virtual bool Read(string fileName)
		{
			StreamReader sr = new StreamReader(fileName, Encoding.Default);

			while (!sr.EndOfStream)
			{
				string str = sr.ReadLine().Trim();
				if (str == string.Empty || str.StartsWith("//")) continue;

				AnCode code = new AnCode(str.Substring(0, str.IndexOf(' ')));

				//if(str.Contains(' '))
				str = str.Substring(str.IndexOf(' ')).Trim();

				// пропускаем первую букву после Анкода, пока не знаю как её использовать
				str = str.Substring(1).Trim();


				GramTabLine line;
				if (LineManager.TryReadFromString(str, out line))
					this[code] = line;
			}
			sr.Close();
			return true;
		}


		public virtual bool AreEqualPartOfSpeech(AnCode code1, AnCode code2)
		{
			return this[code1].PartOfSpeech.Equals(this[code2].PartOfSpeech);
		}
	}
}
