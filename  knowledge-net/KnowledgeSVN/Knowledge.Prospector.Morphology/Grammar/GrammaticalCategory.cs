using System;

namespace Knowledge.Prospector.Morphology.Grammar
{
	[Serializable]
	public partial struct GrammaticalCategory
	{
		private long GrammaticalCategoryId;

		public GrammaticalCategory(long grammaticalCategoryId)
		{
			GrammaticalCategoryId = grammaticalCategoryId;
		}

		public GrammaticalCategory(GrammaticalCategory grammaticalCategory)
		{
			GrammaticalCategoryId = grammaticalCategory.GrammaticalCategoryId;
		}

		public static bool operator ==(GrammaticalCategory x, GrammaticalCategory y)
		{
			return x.GrammaticalCategoryId == y.GrammaticalCategoryId;
		}

		public static bool operator !=(GrammaticalCategory x, GrammaticalCategory y)
		{
			return !(x.GrammaticalCategoryId == y.GrammaticalCategoryId);
		}

		public static GrammaticalCategory operator |(GrammaticalCategory x, GrammaticalCategory y)
		{
			return new GrammaticalCategory(x.GrammaticalCategoryId | y.GrammaticalCategoryId);
		}

		public static GrammaticalCategory operator &(GrammaticalCategory x, GrammaticalCategory y)
		{
			return new GrammaticalCategory(x.GrammaticalCategoryId & y.GrammaticalCategoryId);
		}

		public bool IsUnknown
		{
			get
			{
				return GrammaticalCategoryId < 1;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is GrammaticalCategory))
				return false;
			else
				return this.GrammaticalCategoryId == ((GrammaticalCategory)obj).GrammaticalCategoryId;
		}

		public bool Equals(GrammaticalCategory obj)
		{
			return this.GrammaticalCategoryId == obj.GrammaticalCategoryId;
		}			

		public override int GetHashCode()
		{
			return GrammaticalCategoryId.GetHashCode();
		}

		public override string ToString()
		{
			return GrammaticalCategoryId.ToString();
		}
	}
}
