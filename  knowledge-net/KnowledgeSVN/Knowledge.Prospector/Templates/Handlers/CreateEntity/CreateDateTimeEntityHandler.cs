using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Templates.EntityExpressions;

namespace Knowledge.Prospector.Templates.Handlers.CreateEntity
{
	class CreateDateTimeEntityHandler : CreateEntityHandler, ICreateEntityHandler
	{
		public CreateDateTimeEntityHandler(string arguments)
		{
			ParseArguments(arguments);
		}

		public CreateDateTimeEntityHandler()
		{ }
		

		#region ICreateEntityHandler Members

		public IEntity Create(EntityRegexMatchInfo info)
		{
			DateTimeEntity dtEntity = new DateTimeEntity();
			for (int i = 0; i < Arguments.Length; i++)
			{
				string value = info.GetFirstValue(Arguments[i]);
				dtEntity.Set(ArgumentsDescription[i], value);
			}
			return dtEntity;
		}

		#endregion
}
}
