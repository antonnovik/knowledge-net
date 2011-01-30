using System.IO;
using Knowledge.Prospector.Common.Utilites;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;

namespace Knowledge.Prospector.IO
{
	/// <summary>
	/// Summary description for TestOntology.
	/// </summary>
	public class TestOntology : IOntology
	{
		#region IOntology Members

		public void SaveGraph(EntityGraph entityTree, string fileName)
		{
			fileName = PathUtils.Decode(fileName);
			using (StreamWriter sw = new StreamWriter(fileName))
			{
				//Write header
				sw.WriteLine("<TestOntology>");

				//Write Entities
				foreach (IEntity entity in entityTree.GetAllEntities().GetItems())
				{
					sw.WriteLine();
					PrintEntity(sw, entity);
				}

				//Write Relationships
				foreach (IRelationship relationship in entityTree.GetAllRelationships())
				{
					sw.WriteLine();
					if (relationship is ConditionalRuleRelationship)
						PrintRule(sw, relationship as ConditionalRuleRelationship);
					else
						PrintRelationship(sw, relationship);
				}

				//Write header
				sw.WriteLine();
				sw.WriteLine("</TestOntology>");

				sw.Close();
			}
		}

		#endregion

		private void PrintEntity(StreamWriter sw, IEntity entity)
		{
			PrintEntity(sw, entity, string.Empty, string.Empty);
		}

		private void PrintEntity(StreamWriter sw, IEntity entity, string begin, string end)
		{
			if (entity != null)
				if(entity.Value!=string.Empty)
					sw.WriteLine(string.Format("{0}<{1} value = \"{2}\" />{3}", begin, entity.Name, entity.Value, end));
				else
					sw.WriteLine(string.Format("{0}<{1} />{2}", begin, entity.Name, end));
			else
				sw.WriteLine(string.Format("{0}<null value = \"NULL\" />{1}", begin, end));
		}

		private void PrintRelationship(StreamWriter sw, IRelationship relationship)
		{
			//Write header
			if (relationship.Value == null || relationship.Value == string.Empty)
				sw.WriteLine(string.Format("<{0}>", relationship.Name));
			else
				sw.WriteLine(string.Format("<{0} value = \"{1}\">", relationship.Name, relationship.Value));

			if ((relationship.Kind & RelationshipKind.Functional) > 0)
				sw.WriteLine("\t<Functional/>");
			if ((relationship.Kind & RelationshipKind.Symmetric) > 0)
				sw.WriteLine("\t<Symmetric/>");
			if ((relationship.Kind & RelationshipKind.Transitive) > 0)
				sw.WriteLine("\t<Transitive/>");

			//Write all contained entities
			foreach (IEntity entity in relationship.Entities)
				PrintEntity(sw, entity, "\t", string.Empty);

			//Write footer
			sw.WriteLine("</" + relationship.Name + ">");
		}

		private void PrintRule(StreamWriter sw, ConditionalRuleRelationship rule)
		{
			sw.Write(string.Format("<{0}", "ConditionalRule"));
			sw.Write(string.Format(" if=\"{0} {1}\"", rule.Value, rule.IfProperty.Value));
			sw.Write(string.Format(" then=\"{0} {1}\"", rule.Class.Value, rule.ThenProperty.Value));
			sw.WriteLine(" />");
		}
	}
}