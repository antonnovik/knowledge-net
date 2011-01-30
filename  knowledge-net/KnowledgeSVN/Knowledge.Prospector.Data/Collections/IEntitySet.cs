using Knowledge.Prospector.Common;

namespace Knowledge.Prospector.Data.Collections
{
	/// <summary>
	/// Set of entities. Set stores only reference to entities.
	/// </summary>
	public interface IEntitySet<T> : ISet<T> where T : class, IIdentityUser
	{
		/// <summary>
		/// Adds entities list to set.
		/// </summary>
		/// <param name="entityList">Added entities list.</param>
		void Add(IEntityList<T> entityList);
	}
}
