using System.Collections.Generic;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;
using Knowledge.Prospector.Templates.EntityExpressions;

namespace Knowledge.Prospector.Templates.Handlers.Common
{
	public class CreateConditionalRuleHandler : BaseHandler, ICommonHandler
	{
		public CreateConditionalRuleHandler()
			: base() { }

		#region ICommonHandler Members

		public void Execute(IEntityGraphBuilder builder, EntityRegexMatchInfo info)
		{
			List<IEntity>[] match = info.GetMatch();

			builder.Add(new ConditionalRuleRelationship(
				EntitiesUtils.PrepareToHandle(match[Arguments[0]][0]) as IClassEntity,
				EntitiesUtils.PrepareToHandle(match[Arguments[1]][0]) as IPropertyEntity,
				EntitiesUtils.PrepareToHandle(match[Arguments[2]][0]) as IPropertyEntity));
		}

		#endregion
	}
}
