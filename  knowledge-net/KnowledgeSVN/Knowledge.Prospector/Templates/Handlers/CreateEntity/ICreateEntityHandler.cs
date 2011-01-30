using System.Xml;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Templates.EntityExpressions;

namespace Knowledge.Prospector.Templates.Handlers.CreateEntity
{
	/// <summary>
	/// ICreateEntityHandler is a handler
	/// used for creating new entities in
	/// <see cref="Knowledge.Prospector.Data.Collections.IEntityGraphBuilder" />
	/// </summary>
	public interface ICreateEntityHandler
	{
		/// <summary>
		/// Create new entity.
		/// </summary>
		/// <param name="info">Match information of entity template regex.</param>
		/// <returns></returns>
		IEntity Create(EntityRegexMatchInfo info);

		/// <summary>
		/// Initialize handler from XmlNode.
		/// </summary>
		/// <param name="node">XmlNode to initialize handler from.</param>
		void Read(XmlNode node);
	}
}