using System;
using Knowledge.Prospector.Morphology.Grammar;

namespace Knowledge.Prospector.Morphology
{
	[Serializable]
	public struct MorphologicForm
	{
		public readonly AnCode Gramcode;
		public readonly string FlexiaStr;
		public readonly string PrefixStr;

		public MorphologicForm(AnCode gramcode, string flexiaStr, string prefixStr)
		{
			Gramcode = gramcode;
			FlexiaStr = flexiaStr;
			PrefixStr = prefixStr;
		}

		public bool Assert
		{
			get
			{
				return Gramcode != string.Empty && (FlexiaStr != string.Empty || PrefixStr != string.Empty);
			}
		}

		public static readonly MorphologicForm Unknown = new MorphologicForm(AnCode.Unknown, string.Empty, string.Empty);

		#region Equivalence operators and functions

		public static bool operator ==(MorphologicForm x, MorphologicForm y)
		{
			return x.Gramcode == y.Gramcode
				&& x.FlexiaStr == y.FlexiaStr
				&& x.PrefixStr == y.PrefixStr;
		}

		public static bool operator !=(MorphologicForm x, MorphologicForm y)
		{
			return !(x == y);
		}
		public override int GetHashCode()
		{
			return Gramcode.GetHashCode() + FlexiaStr.GetHashCode() + PrefixStr.GetHashCode();
		}
		public override bool Equals(Object obj)
		{
			// If parameter is null, or cannot be cast to FlexiaModel,
			// return false.
			if (obj == null) return false;
			if (!(obj is MorphologicForm)) return false;
			// Return true if the fields match:
			return this == (MorphologicForm)obj;
		}
		public bool Equals(MorphologicForm mf)
		{
			// Return true if the fields match:
			return this == mf;
		}

		#endregion
	}
}
