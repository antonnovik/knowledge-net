using System;

namespace Knowledge.Prospector.Data.Entities
{
	/// <summary>
	/// Entity what have many means of another entities.
	/// </summary>
	[Serializable]
	public class ChangeableEntity : Entity
	{
		private IEntity[] _AllowedEntities;
		public IEntity[] AllowedEntities
		{
			get { return _AllowedEntities; }
			private set { _AllowedEntities = value; }
		}

		public ChangeableEntity(string value)
			: base(value) { }

		public ChangeableEntity(string value, IEntity[] allowedEntities)
			: base(value) 
		{
			AllowedEntities = allowedEntities;
		}

		#region IEntityBase Members

		public override string Name
		{
			get { return "Changeable"; }
		}

		#endregion

		public override string ToString()
		{
			return string.Format("<Changeable>{0}</Changeable>", Value);
		}

		public IEntity ChangeToEntity;

		public IEntity Change()
		{
			if (ChangeToEntity == null)
				throw new Exception("ChangeToEntity is not setted");
			return ChangeToEntity;
		}
	}
}
