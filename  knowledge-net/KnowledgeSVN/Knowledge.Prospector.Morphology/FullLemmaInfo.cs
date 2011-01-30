using System;
using Knowledge.Prospector.Morphology.Grammar;

namespace Knowledge.Prospector.Morphology
{
	[Serializable]
	public class FullLemmaInfo : LemmaInfo
	{
		public FullLemmaInfo()
			: base()
		{ }

		public FullLemmaInfo(LemmaInfo lemmaInfo)
			: base(lemmaInfo)
		{ }

		public FullLemmaInfo(int flexiaModelNo, int accentModelNo, string commonAncode)
			: base(flexiaModelNo, accentModelNo, commonAncode)
		{ }

		public FullLemmaInfo(LemmaInfo lemmaInfo,
			string commonLemma,
			MorphologicForm commonMF,
			MorphologicForm currentMF)
			: base(lemmaInfo)
		{
			CommonLemma = commonLemma;
			CommonMF = commonMF;
			CurrentMF = currentMF;
		}

		public string CommonLemma;
		public MorphologicForm CommonMF;
		public MorphologicForm CurrentMF;
		public static readonly FullLemmaInfo Unknown = new FullLemmaInfo(-1, -1, AnCode.Unknown);

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (!(obj is FullLemmaInfo)) return false;
			// Return true if the fields match:
			return this == (FullLemmaInfo)obj;
			
		}

		public static bool operator ==(FullLemmaInfo x, FullLemmaInfo y)
		{
			return (x.FlexiaModelNo == y.FlexiaModelNo)
				&& (x.AccentModelNo == y.AccentModelNo)
				&& (x.CommonAncode == y.CommonAncode);
		}
		public static bool operator !=(FullLemmaInfo x, FullLemmaInfo y)
		{
			return !(x == y);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
