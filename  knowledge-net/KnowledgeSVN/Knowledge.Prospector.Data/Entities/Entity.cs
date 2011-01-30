using System;
using Knowledge.Prospector.Common;
using Knowledge.Prospector.Morphology;
using Knowledge.Prospector.Morphology.Grammar;

namespace Knowledge.Prospector.Data.Entities
{
	[Serializable]
	public abstract class Entity : IEntity
	{

		#region Constructors

		protected Entity(string id, string value)
		{
			_id = IdentityManager.GetIdentity(id);
			_value = value.ToLower();
		}

		protected Entity(string value)
			: this(value, value) { }

		#endregion

		#region Overrides

		public int CompareTo(IEntity other)
		{
			return Identity.CompareTo(other.Identity);
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (!(obj is IEntity))
				return false;

			return Identity == (obj as IEntity).Identity;
		}

		public bool Equals(IEntity e)
		{
			if ((object)e == null) return false;
			return Identity == e.Identity;
		}

		public override int GetHashCode()
		{
			return Identity.GetHashCode();
		}

		#endregion

		#region IEntity Members

		private string _value;
		public virtual string Value
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

		public virtual string Name
		{
			get { return "EntityBase"; }
		}

		private PartOfSpeech _partOfSpech;
		public PartOfSpeech PartOfSpeech
		{
			get
			{
				return _partOfSpech;
			}
			set
			{
				_partOfSpech = value;
			}
		}

		private FullLemmaInfo _MorphologicalInfo;
		public FullLemmaInfo MorphologicalInfo
		{
			get { return _MorphologicalInfo; }
			set { _MorphologicalInfo = value; }
		}


		#endregion

		#region IIdentityUser Members

		private string _id;
		public virtual string Identity
		{
			get
			{
				return _id;
			}
			protected set
			{
				_id = value;
			}
		}

		public virtual void GenerateIdentity()
		{ }

		#endregion
	}
}
