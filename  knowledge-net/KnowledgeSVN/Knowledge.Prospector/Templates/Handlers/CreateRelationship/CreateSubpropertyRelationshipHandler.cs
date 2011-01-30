using System.Collections.Generic;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;
using Knowledge.Prospector.Templates.EntityExpressions;

namespace Knowledge.Prospector.Templates.Handlers.CreateRelationship
{
	class CreateSubpropertyRelationshipHandler : CreateRelationshipHandler, ICreateRelationshipHandler
	{
		public CreateSubpropertyRelationshipHandler()
			: base() { }

		#region ICommonHandler Members

		public void Execute(IEntityGraphBuilder builder, EntityRegexMatchInfo info)
		{
			List<IEntityList<IEntity>> bigList = AddEntitiesToList(info.GetMatch(), 0, new EntityList<IEntity>());
			for (int i = 0; i < bigList.Count; i++)
			{
				IEntityList<ITrueEntity> oneList = bigList[i].ConvertToTrueEntityList();
				SubpropertyRelationship subpropertyRelation = new SubpropertyRelationship(oneList[0] as IPropertyEntity, oneList[1] as IPropertyEntity);
				builder.Add(subpropertyRelation);
			}
		}

		#endregion
}
}
