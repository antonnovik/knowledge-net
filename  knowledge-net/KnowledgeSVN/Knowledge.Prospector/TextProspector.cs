using System;
using Knowledge.Prospector.Common.Utilites;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;
using Knowledge.Prospector.IO.Articles;
using Knowledge.Prospector.Languages;
using Knowledge.Prospector.Templates;

namespace Knowledge.Prospector
{
	public class TextProspector : IProspector<TextArticle>
	{
		#region IProspector Members

		public void Prospect(EntityGraph entityGraph, IArticle article)
		{
			AssertUtils.AssertParamNotNull(article, "article");

			if(article is TextArticle)
				Prospect(entityGraph, article as TextArticle);
			else
				throw new Exception(string.Format("Unsupported article type for this prospector: {0}", article.GetType()));
		}

		#endregion

		#region IProspector<TextArticle> Members

		private TextArticle _CurrentArticle;

		public void Prospect(EntityGraph entityGraph, TextArticle article)
		{
			AssertUtils.AssertParamNotNull(article, "article");

			_CurrentArticle = article;

			IEntityList<IEntity> entities = ConvertEachTBlockToEntity(article);

			entityGraph.ConditionalRuleRelationships.OnAdd += new Knowledge.Prospector.Common.SetEvent<Knowledge.Prospector.Data.Relationships.ConditionalRuleRelationship>(ConditionalRuleRelationships_OnAdd);

			//Buiding Graph
			ILanguage language = LanguageManager.GetInstance().DeterminateLanguage(article);
			IEntityGraphBuilder entityGraphBuilder = new EntityGraphBuilder(entityGraph, entities);
			
			//Applying rules
			EntityTemplateManager.GetInstance().ApplyTemplates(entityGraphBuilder, language);
			//	EntityGraphBuilderRules.UseEntitiesRule(entityGraphBuilder);
			//	EntityGraphBuilderRules.UseConvertRelationshipEntityToRelationshipRule(entityGraphBuilder);

			_CurrentArticle = null;
			entityGraph.ConditionalRuleRelationships.OnAdd -= ConditionalRuleRelationships_OnAdd;

		}

		bool ConditionalRuleRelationships_OnAdd(Knowledge.Prospector.Data.Relationships.ConditionalRuleRelationship item)
		{
			if(_CurrentArticle != null && _CurrentArticle.Info != null && _CurrentArticle.Info.Count != 0)
			{
				if(item.Attributes == null)
					item.Attributes = new RelationshipAttributes();
			
				CopyAttributes(item.Attributes, _CurrentArticle.Info);
			}
			return true;
		}
		
		protected void CopyAttributes(RelationshipAttributes relationshipAttributes, ArticleInfo articleInfo)
		{
			for(int i=0; i< articleInfo.Count; i++)
			{
				relationshipAttributes[articleInfo.GetKey(i)] = articleInfo[i];
			}
		}

		#endregion

		/// <summary>
		/// Translate each block and separator to one entity.
		/// </summary>
		private IEntityList<IEntity> ConvertEachTBlockToEntity(TextArticle article)
		{
			IEntityList<IEntity> entityList = new EntityList<IEntity>();

			foreach (TextBlock textBlock in article)
				entityList.Add(EntityManager.Instance.CreateEntity(textBlock));

			return entityList;
		}

	}
}
