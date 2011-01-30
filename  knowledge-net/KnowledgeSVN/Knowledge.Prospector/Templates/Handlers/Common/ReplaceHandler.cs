using System.Xml;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Templates.EntityExpressions;
using Knowledge.Prospector.Templates.Handlers.CreateEntity;

namespace Knowledge.Prospector.Templates.Handlers.Common
{
	public class ReplaceHandler : BaseHandler, ICommonHandler
	{
		private ICreateEntityHandler CreateHandler;

		private int From;
		private int Count;

		public ReplaceHandler() { }

		public override void Read(XmlNode node)
		{
			From = int.Parse(node.Attributes["From"].Value);
			Count = int.Parse(node.Attributes["Count"].Value);
			CreateHandler = CreateEntityHandlerManager.GetHandler(node);
		}

		#region ICommonHandler Members

		public void Execute(IEntityGraphBuilder builder, EntityRegexMatchInfo info)
		{
			builder.Replace(info.StartIndex + From, PositionType.Absolute, CreateHandler.Create(info), Count);
		}

		#endregion
	}
}
