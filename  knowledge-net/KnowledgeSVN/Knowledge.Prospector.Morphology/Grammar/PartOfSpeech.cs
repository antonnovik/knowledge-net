using System;

namespace Knowledge.Prospector.Morphology.Grammar
{
	[Serializable]
	public partial struct PartOfSpeech
	{
		private long PartOfSpeechId;

		internal PartOfSpeech(long partOfSpeechId)
		{
			PartOfSpeechId = partOfSpeechId;
		}

		public PartOfSpeech(PartOfSpeech partOfSpeech)
		{
			PartOfSpeechId = partOfSpeech.PartOfSpeechId;
		}

		public static bool operator ==(PartOfSpeech x, PartOfSpeech y)
		{
		    return x.PartOfSpeechId == y.PartOfSpeechId;
		}

		public static bool operator !=(PartOfSpeech x, PartOfSpeech y)
		{
		    return !(x.PartOfSpeechId == y.PartOfSpeechId);
		}

		public static PartOfSpeech operator |(PartOfSpeech x, PartOfSpeech y)
		{
			return new PartOfSpeech(x.PartOfSpeechId | y.PartOfSpeechId);
		}

		public static PartOfSpeech operator &(PartOfSpeech x, PartOfSpeech y)
		{
			return new PartOfSpeech(x.PartOfSpeechId & y.PartOfSpeechId);
		}

		public bool IsUnknown
		{
			get
			{
				return PartOfSpeechId == 0;
			}
		}

		public bool Is(PartOfSpeech pos)
		{
			return (this.PartOfSpeechId & pos.PartOfSpeechId) != 0;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is PartOfSpeech))
				return false;
			else
				return this.PartOfSpeechId == ((PartOfSpeech)obj).PartOfSpeechId;
		}

		public bool Equals(PartOfSpeech obj)
		{
			return this.PartOfSpeechId == obj.PartOfSpeechId;
		}			

		public override int GetHashCode()
		{
			return PartOfSpeechId.GetHashCode();
		}

		public override string ToString()
		{
			return PartOfSpeechId.ToString();
		}
	}
}
