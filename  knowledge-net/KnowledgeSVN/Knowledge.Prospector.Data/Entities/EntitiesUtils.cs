namespace Knowledge.Prospector.Data.Entities
{
	public static class EntitiesUtils
	{
		public static IEntity PrepareToHandle(IEntity entity)
		{
			ChangeableEntity changeableEntity = entity as ChangeableEntity;
			if (changeableEntity != null && changeableEntity.ChangeToEntity != null)
			{
				return changeableEntity.Change();
			}

			return entity;
		}
	}
}
