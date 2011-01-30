using System.Collections.Generic;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;
using Knowledge.Prospector.Templates.EntityExpressions;

namespace Knowledge.Prospector.Templates.Handlers.CreateRelationship
{
	class CreateClassToClassRelationshipHandler : CreateRelationshipHandler, ICreateRelationshipHandler
	{
		public CreateClassToClassRelationshipHandler()
			: base() { }

		#region ICommonHandler Members

		public void Execute(IEntityGraphBuilder builder, EntityRegexMatchInfo info)
		{
			List<IEntity>[] match = info.GetMatch();

			builder.Add(new ClassToClassRelationship(
				match[Arguments[0]][0].Value,
				match[Arguments[1]][0] as IClassEntity,
				match[Arguments[2]][0] as IClassEntity,
				RelationshipKindHelper.GetKind(ArgumentsDescription[3])));
		}

		#endregion
}
}
