using System;

namespace Knowledge.Prospector.Morphology.Grammar
{
	[Serializable]
	public struct AnCode
	{
		private readonly string Code;

		public AnCode(string code)
		{
			Code = code;
		}

		public static readonly AnCode Unknown = string.Empty;

		public static implicit operator string(AnCode anCode)
		{
			return anCode.Code;
		}

		public static implicit operator AnCode(string code)
		{
			return new AnCode(code);
		}

		public static bool operator <(AnCode x, AnCode y)
		{
			return x.Code.CompareTo(y.Code) < 0;
		}

		public static bool operator >(AnCode x, AnCode y)
		{
			return !(x < y);
		}

		public static bool operator ==(AnCode x, AnCode y)
		{
			return x.Code == y.Code;
		}

		public static bool operator !=(AnCode x, AnCode y)
		{
			return !(x.Code == y.Code);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is AnCode))
				return false;
			else
				return this == (AnCode)obj;
		}

		public override int GetHashCode()
		{
			return Code.GetHashCode();
		}

		public override string ToString()
		{
			return Code;
		}

	}
}
