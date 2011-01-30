using System;

namespace Knowledge.Prospector.Data.Entities
{
	/// <summary>
	/// Summary description for ClassEntity.
	/// </summary>
	[Serializable]
	public class ClassEntity : TrueEntity, IClassEntity
	{
		public ClassEntity(string value)
			: base(value) { }

		#region IEntity Members

		private static readonly string NameString = "Class";
		public override string Name
		{
			get { return NameString; }
		}

		#endregion

		public override string ToString()
		{
			return string.Format("<Class>{0}</Class>", Value);
		}

		/// <summary>
		/// Every class entity is implicitly a subclass of "Thing"
		/// </summary>
		public static readonly IClassEntity Thing = new ClassEntity("Thing");

		/// <summary>
		/// "Nothing" is implicitly empty subclass of every class entity.
		/// </summary>
		public static readonly IClassEntity Nothing = new ClassEntity("Nothing");
	}


}
