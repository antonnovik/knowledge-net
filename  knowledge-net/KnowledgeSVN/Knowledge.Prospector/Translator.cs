using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.IO.Articles;

namespace Knowledge.Prospector
{
	public class Translator : ITranslator
	{
		#region ITranslator Members

		public IEntityList<IEntity> Translate(IArticle article)
		{
			return UseOneBlockToOneEntityRule(article);
		}

		#endregion

		/// <summary>
		/// Translate each block and separator to one entity.
		/// </summary>
		private IEntityList<IEntity> UseOneBlockToOneEntityRule(IArticle article)
		{
			IEntityList<IEntity> entityList = new EntityList<IEntity>();

			foreach(TextBlock textBlock in article)
				entityList.Add(EntityManager.Instance.CreateEntity(textBlock));

			return entityList;
		}
	}
}
