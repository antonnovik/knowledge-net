using System;
using System.Collections.Generic;

namespace Knowledge.Prospector.Morphology
{
	[Serializable]
	public struct AccentModel
	{
		public List<byte> Accents;

		#region Equivalence functions and operators

		public static bool operator ==(AccentModel x, AccentModel y)
		{
			return x.Accents == y.Accents;

		}
		public static bool operator !=(AccentModel x, AccentModel y)
		{
			return !(x == y);
		}
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is AccentModel)) return false;
			else return (AccentModel)obj == this;
		}
		public override int GetHashCode()
		{
			return Accents.GetHashCode();
		}

		#endregion

		public bool ReadFromString(string s)
		{
			Accents = new List<byte>();
			foreach (string OneRecord in s.Split(';', ' ', '\r', '\n'))
			{
				if (OneRecord == "") return false;
				try
				{
					Accents.Add(byte.Parse(OneRecord));
				}
				catch { };
			}
			return true;
		}

		public override string ToString()
		{
			string Result = "";
			for (int i = 0; i < Accents.Count; i++)
			{
				Result += string.Format("%i;", Accents[i]);
			}
			return Result;
		}
	}
}
