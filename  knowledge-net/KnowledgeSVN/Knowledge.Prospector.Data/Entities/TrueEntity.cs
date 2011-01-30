using System;

namespace Knowledge.Prospector.Data.Entities
{
	/// <summary>
	/// True entity is entity what can be used in graph of knowledge.
	/// </summary>
	[Serializable]
	public abstract class TrueEntity : Entity
	{
		#region IEntity Members

		public override string Name
		{
			get { return "TrueEntity"; }
		}

		#endregion

		#region Constructors

		protected TrueEntity(string id, string value)
			: base(id, value) { }

		protected TrueEntity(string value)
			: this(value, value) { }

		#endregion

		public static ITrueEntity ToTrueEntity(IEntity entity)
		{
			ITrueEntity trueEntity;
			if (entity is ChangeableEntity)
				trueEntity = (entity as ChangeableEntity).Change() as ITrueEntity;
			else
				trueEntity = entity as ITrueEntity;
			if (trueEntity == null)
				throw new ArgumentException("Can't cast " + entity.ToString() + " to ITrueEntity", "entity");
			return trueEntity;
		}

		public static bool TryToTrueEntity(IEntity entity, out ITrueEntity trueEntity)
		{
			if (entity is ChangeableEntity)
				trueEntity = (entity as ChangeableEntity).Change() as ITrueEntity;
			else
				trueEntity = entity as ITrueEntity;
			return trueEntity != null;
		}
	}
}
