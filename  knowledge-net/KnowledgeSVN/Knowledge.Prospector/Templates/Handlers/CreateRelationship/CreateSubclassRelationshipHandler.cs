using System.Collections.Generic;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;
using Knowledge.Prospector.Templates.EntityExpressions;

namespace Knowledge.Prospector.Templates.Handlers.CreateRelationship
{
	public class CreateSubclassRelationshipHandler : CreateRelationshipHandler, ICreateRelationshipHandler
	{
		public CreateSubclassRelationshipHandler()
			: base() { }

		#region ICommonHandler Members

		public void Execute(IEntityGraphBuilder builder, EntityRegexMatchInfo info)
		{
			List<IEntityList<IEntity>> bigList = AddEntitiesToList(info.GetMatch(), 0, new EntityList<IEntity>());
			for (int i = 0; i < bigList.Count; i++)
			{
				IEntityList<ITrueEntity> oneList = bigList[i].ConvertToTrueEntityList();
				SubclassRelationship subclassRelation = new SubclassRelationship(oneList[0] as IClassEntity, oneList[1] as IClassEntity);
				builder.Add(subclassRelation);
			}
		}

		#endregion
}
}
