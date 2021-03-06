using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Knowledge.Prospector.Common.Utilites;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;

namespace Knowledge.Prospector.IO
{
	public class KnowledgeNetOntology : IOntology
	{

		#region Properties

		private string _newLine = "\n";
		private StreamWriter _sw;
		private EntityGraph _graph;

		/// <summary>
		/// NewLine string.
		/// </summary>
		private string NL
		{
			get
			{
				return _newLine;
			}
		}

		/// <summary>
		/// Stream writer.
		/// </summary>
		private StreamWriter SW
		{
			get
			{
				return _sw;
			}
		}

		/// <summary>
		/// Entity graph.
		/// </summary>
		private EntityGraph Graph
		{
			get
			{
				return _graph;
			}
		}

		private string _OntologyName = "Autogenerated ontology";
		public string OntologyName
		{
			get { return _OntologyName; }
			set { _OntologyName = value; }
		}


		#endregion

		private void InitializeStreamWriter(string fileName)
		{
			_sw = new StreamWriter(fileName, false, Encoding.UTF8);
			_newLine = _sw.NewLine;
		}

		#region IOntology Members

		public void SaveGraph(EntityGraph graph, string fileName)
		{
			fileName = PathUtils.Decode(fileName);
			InitializeStreamWriter(fileName);
			_graph = graph;

			PrintHeader();

			//Printing Classes
			SW.WriteLine(@"#concepts");
			foreach (IClassEntity entity in graph.Classes.GetItems())
			{
				Print(entity);
			}

			//Printing Properties
			SW.WriteLine(@"#properties");
			foreach (IPropertyEntity entity in graph.Properties.GetItems())
			{
				Print(entity);
			}

			foreach (IRelationship relationship in graph.Relationships.GetItems())
			{
				if(relationship is ClassToClassRelationship)
					PrintClassToClassRelationship(relationship as ClassToClassRelationship);
			}

			//Printing Rules
			if (graph.ConditionalRuleRelationships.Count != 0)
			{
				SW.WriteLine(@"#rules");
				SW.WriteLine("ruleset {0}", new Regex(@"\s").Replace(OntologyName, "_"));
				SW.WriteLine("{");

				foreach (ConditionalRuleRelationship rule in graph.ConditionalRuleRelationships.GetItems())
				{
					PrintConditionalRuleRelationship(rule);
				}

				SW.WriteLine("}");
			}



			//Printing Individuals
			SW.WriteLine(@"#individuals");
			foreach (IIndividualEntity entity in graph.Individuals.GetItems())
			{
				Print(entity);
			}

			PrintFooter();
			SW.Close();
		}

		#endregion

		#region Print functions

		protected void PrintHeader()
		{
			SW.WriteLine(@"#ontology ""{0}""", OntologyName);
		}

		protected void PrintFooter()
		{
			SW.WriteLine(@"#end_of_ontology ""{0}""", OntologyName);
		}

		protected void Print(IPropertyEntity propertyEntity)
		{
			SW.WriteLine("object property {0}", propertyEntity.Identity);
			SW.WriteLine("{");

			//print parents
			foreach (IPropertyEntity parent in Graph.GetParents(propertyEntity).GetItems())
			{
				SW.WriteLine("\tis_subproperty_of  {0};", parent.Identity);
			}

			//print domain
			SW.Write("\tdomain ");
			bool firstEntrance = true;
			foreach (IClassEntity classEntity in Graph.GetClasses(propertyEntity).GetItems())
			{
				if (firstEntrance)
				{
					firstEntrance = false;
					SW.Write("{0}", classEntity.Value);
				}
				else
					SW.Write(", {0}", classEntity.Value);
			}
			SW.WriteLine(";");

			//print range
			if(propertyEntity.Range != null)
				SW.WriteLine("\trange {0};", propertyEntity.Range.Name);
			else
				SW.WriteLine("\trange bool;");

			SW.WriteLine("}");
		}

		protected void Print(IClassEntity classEntity)
		{
			SW.WriteLine(classEntity.Identity);
			SW.WriteLine("{");
			foreach (IClassEntity parent in Graph.GetParents(classEntity).GetItems())
			{
				SW.WriteLine("\tis_subconcept_of {0};", parent.Identity);
			}
			foreach (IPropertyEntity property in Graph.GetProperties(classEntity).GetItems())
			{
				SW.WriteLine("\t{0};", property.Identity);
			}
			SW.WriteLine("}");
		}

		protected void Print(IIndividualEntity individualEntity)
		{
			
		}

		protected void PrintEntity(IDatatypeEntity datatypeEntity)
		{
		
		}

		protected void PrintClassToClassRelationship(ClassToClassRelationship classToClassRelationship)
		{
			SW.Write("{0} ", classToClassRelationship.Kind.ToString().ToLower());

			SW.WriteLine("object property {0}", classToClassRelationship.Value);
			SW.WriteLine("{");

			//print domain
			SW.WriteLine("\tdomain {0};", classToClassRelationship.Left.Value);

			//print range
			SW.WriteLine("\trange {0};", classToClassRelationship.Right.Value);

			SW.WriteLine("}");
		}

		protected void PrintConditionalRuleRelationship(ConditionalRuleRelationship rule)
		{
			//annotation
			if(rule.Attributes != null && rule.Attributes.Count != 0)
			{
				SW.WriteLine("\tannotation");
				SW.WriteLine("\t{");
				for (int i = 0; i < rule.Attributes.Count; i++)
				{
					SW.WriteLine(string.Format("\t\t{0} \"{1}\";", rule.Attributes.GetKey(i), rule.Attributes[i]));
				}
				SW.WriteLine("\t}");
			}

			SW.WriteLine("\trule {0}", rule.GetHashCode());
			SW.WriteLine("\t{");

			SW.WriteLine("\t\tif ({0}.{1} = true)", rule.Class.Value, rule.IfProperty.Value);
			SW.WriteLine("\t\tthen ({0}.{1} = true)", rule.Class.Value, rule.ThenProperty.Value);

			SW.WriteLine("\t}");
		}

		#endregion
	}
}
