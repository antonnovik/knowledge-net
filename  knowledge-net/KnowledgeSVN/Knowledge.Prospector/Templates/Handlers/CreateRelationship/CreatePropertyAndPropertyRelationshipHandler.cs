using System.Collections.Generic;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;
using Knowledge.Prospector.Templates.EntityExpressions;

namespace Knowledge.Prospector.Templates.Handlers.CreateRelationship
{
	/// <summary>
	/// WARNING!
	/// Just for example. This class should be optimized in some way...
	/// </summary>
	public class CreatePropertyAndPropertyRelationshipHandler : CreateRelationshipHandler, ICreateRelationshipHandler
	{
		public CreatePropertyAndPropertyRelationshipHandler()
			: base() { }

		#region ICommonHandler Members

		public void Execute(IEntityGraphBuilder builder, EntityRegexMatchInfo info)
		{
			List<IEntity>[] match = info.GetMatch();
			PropertyEntity propertyEntity = new PropertyEntity(EntitiesUtils.PrepareToHandle(match[Arguments[0]][0]).Value);
			propertyEntity.Range = new CustomRange(ArgumentsDescription[2]);
			builder.Add(propertyEntity);

			ClassEntity classEntity = match[Arguments[1]][0] as ClassEntity;

			PropertyRelationship propertyRelation = new PropertyRelationship(propertyEntity, classEntity);
			builder.Add(propertyRelation);
		}

		#endregion
}
}
