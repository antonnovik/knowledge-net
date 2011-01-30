using System;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Data.Relationships
{
	[Serializable]
	public abstract class Relationship : IRelationship
	{
		private static string DefaultValue = string.Empty;
		private string _value = DefaultValue;
		private IEntityList<ITrueEntity> _entities;
		private RelationshipKind _kind;
		private string _id;
		private RelationshipAttributes _Attributes;

		#region Constructors

		protected Relationship()
		{
			_entities = new EntityList<ITrueEntity>();
		}

		protected Relationship(string value)
			: this()
		{
			_value = value;
		}

		protected Relationship(RelationshipEntity relationshipEntity)
			: this(relationshipEntity.Value)
		{
			this.Kind = relationshipEntity.Kind;
		}

		protected Relationship(int capacity)
		{
			_entities = new EntityList<ITrueEntity>(capacity);
		}

		#endregion

		#region Overrides

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (!(obj is IRelationship))
				return false;

			return Identity == (obj as IRelationship).Identity;
		}

		public bool Equals(IRelationship e)
		{
			if ((object)e == null) return false;
			return Identity == e.Identity;
		}

		public override int GetHashCode()
		{
			return Identity.GetHashCode();
		}

		public override string ToString()
		{
			if (Entities == null)
			{
				return string.Empty;
			}
			else
			{
				string str = string.Empty;
				for (int i = 0; i < Entities.Count; i++)
					str += Entities[i].Value + "->";
				return str;
			}			
		}

		#endregion

		#region IRelationship Members

		public virtual string Name
		{
			get { return "RelationshipBase"; }
		}

		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

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

		/// <summary>
		/// Entities participate in relationship.
		/// </summary>
		public IEntityList<ITrueEntity> Entities
		{
			get
			{
				return _entities;
			}
			protected set
			{
				_entities = value;
			}
		}

		public RelationshipAttributes Attributes
		{
			get { return _Attributes; }
			set { _Attributes = value; }
		}

		#endregion

		#region IIdentityUser Members

		public string Identity
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}

		/// <summary>
		/// Generates identity based on contained entities.
		/// </summary>
		public void GenerateIdentity()
		{
			if (Entities == null)
			{
				_id = string.Empty;
			}
			else
			{
				string id = Name + Value;
				for (int i = 0; i < Entities.Count; i++)
					id += Entities[i].Identity;
				_id = id;
			}
		}

		#endregion
	}
}
