using System;
using Knowledge.Prospector.Morphology.Grammar;

namespace Knowledge.Prospector.Morphology
{
	[Serializable]
	public class LemmaInfo
	{
		public int FlexiaModelNo;
		public int AccentModelNo;
		public AnCode CommonAncode;

		public static readonly string AnyCommonAncode = string.Empty;

		public LemmaInfo()
			: this(-1, -1, AnCode.Unknown)
		{ }

		public LemmaInfo(int flexiaModelNo, int accentModelNo, string commonAncode)
		{
			FlexiaModelNo = flexiaModelNo;
			AccentModelNo = accentModelNo;
			CommonAncode = commonAncode;
		}

		public LemmaInfo(LemmaInfo initializator)
			: this(initializator.FlexiaModelNo, initializator.AccentModelNo, initializator.CommonAncode)
		{ }

		public override string ToString()
		{
			return string.Format("Flexia:{0}, Accent:{1}, Ancode:{2}", FlexiaModelNo, AccentModelNo, CommonAncode);
		}

		#region Equivalence operators and functions

		public static bool operator ==(LemmaInfo x, LemmaInfo y)
		{
			return (x.FlexiaModelNo == y.FlexiaModelNo)
				&& (x.AccentModelNo == y.AccentModelNo)
				&& (x.CommonAncode == y.CommonAncode);
		}
		public static bool operator !=(LemmaInfo x, LemmaInfo y)
		{
			return !(x == y);
		}

		public static bool operator <(LemmaInfo x, LemmaInfo y)
		{
			if (x.FlexiaModelNo != y.FlexiaModelNo)
				return x.FlexiaModelNo < y.FlexiaModelNo;

			if (x.CommonAncode != y.CommonAncode)
				return x.CommonAncode<y.CommonAncode;

			return x.AccentModelNo < y.AccentModelNo;
		}

		public static bool operator >(LemmaInfo x, LemmaInfo y)
		{
			return !(x < y);
		}

		public override int GetHashCode()
		{
			return CommonAncode.GetHashCode() + FlexiaModelNo*1024;
		}

		public override bool Equals(Object obj)
		{
			// If parameter is null, or cannot be cast to FlexiaModel,
			// return false.
			if (obj == null) return false;
			if (!(obj is LemmaInfo)) return false;
			// Return true if the fields match:
			return this == (LemmaInfo)obj;
		}
		public bool Equals(LemmaInfo li)
		{
			// Return true if the fields match:
			return this == li;
		}

		#endregion
	}
}
