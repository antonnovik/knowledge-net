using System.Xml;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Templates.EntityExpressions;

namespace Knowledge.Prospector.Templates.Handlers
{
	/// <summary>
	/// Base interface for handlers.
	/// </summary>
	public interface IBaseHandler
	{
		/// <summary>
		/// Execute handler for knowledge graph builder.
		/// </summary>
		/// <param name="builder">Knowledge graph builder.</param>
		/// <param name="info">Match information of entity template regex.</param>
		void Execute(IEntityGraphBuilder builder, EntityRegexMatchInfo info);

		/// <summary>
		/// Initialize handler from XmlNode.
		/// </summary>
		/// <param name="node">XmlNode to initialize handler from.</param>
		void Read(XmlNode node);
	}
}
