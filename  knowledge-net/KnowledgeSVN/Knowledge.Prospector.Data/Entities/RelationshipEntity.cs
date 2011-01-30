using System;
using Knowledge.Prospector.Data.Relationships;

namespace Knowledge.Prospector.Data.Entities
{
	[Serializable]
	public class RelationshipEntity : Entity, IEntity
	{
		protected RelationshipKind _kind;
		protected string _type;

		public RelationshipEntity(string value)
			: base(value) { }

		#region IEntity Members

		public override string Name
		{
			get { return "Relationship"; }
		}

		#endregion

		/// <summary>
		/// Gets or sets type of property.
		/// </summary>
		public RelationshipKind Kind
		{
			get
			{
				return _kind;
			}
			set
			{
				_kind = value;
			}
		}

		public string Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}
}
}
