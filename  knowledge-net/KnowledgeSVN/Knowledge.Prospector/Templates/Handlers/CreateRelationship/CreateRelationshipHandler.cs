using System.Collections.Generic;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Templates.Handlers.CreateRelationship
{
	public abstract class CreateRelationshipHandler : BaseHandler
	{
		/// <summary>
		/// Adds all combination of entities from succeed entities.
		/// </summary>
		/// <param name="participatedEntities">Entities.</param>
		/// <param name="argumentIndex">Starting index for addition to head.</param>
		/// <param name="head">Head of resulted list of entities.</param>
		/// <returns>Collection of entities lists.</returns>
		protected List<IEntityList<IEntity>> AddEntitiesToList(List<IEntity>[] succeed, int argumentIndex, IEntityList<IEntity> head)
		{
			List<IEntityList<IEntity>> bigList = new List<IEntityList<IEntity>>();
			if (!(argumentIndex < Arguments.Length))
				return bigList;


			int argument = Arguments[argumentIndex];
			for (int i = 0; i < succeed[argument].Count; i++)
			{
				//Copy head
				IEntityList<IEntity> entityList = new EntityList<IEntity>();
				for (int j = 0; j < head.Count; j++)
					entityList.Add(head[j]);
				entityList.Add(succeed[argument][i]);
				if (argumentIndex + 1 < Arguments.Length)
					bigList.AddRange(AddEntitiesToList(succeed, argumentIndex + 1, entityList));
				else
					bigList.Add(entityList);
			}

			return bigList;
		}

	}
}
