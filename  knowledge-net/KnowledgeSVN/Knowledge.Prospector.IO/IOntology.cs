using Knowledge.Prospector.Data.Collections;

namespace Knowledge.Prospector.IO
{
	/// <summary>
	/// Instances of this interface used for presenting inner structure of builded knowledges to user.
	/// </summary>
	public interface IOntology
	{
		/// <summary>
		/// Save EntityTree to File
		/// </summary>
		/// <param name="fileName"></param>
		void SaveGraph(EntityGraph graph, string fileName);
	}
}
