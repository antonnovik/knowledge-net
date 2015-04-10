using System.IO;
using System.Text;
using Knowledge.Prospector.Common.Utilites;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.IO
{
	public class OwlOntology : IOntology
	{
		private string _newLine = "\n";

		public string NewLine
		{
			get
			{
				return _newLine;
			}
			set
			{
				_newLine = value;
			}
		}

		#region IOntology Members

		public void SaveGraph(EntityGraph graph, string fileName)
		{
			fileName = PathUtils.Decode(fileName);
			using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
			{
				NewLine = sw.NewLine;

				sw.WriteLine(PrintHeader());
				foreach (IEntity entity in graph.GetAllEntities().GetItems())
				{
					string str = string.Empty;

					if (entity is PropertyEntity)
						str = PrintEntity((PropertyEntity)entity);

					else if (entity is ClassEntity)
						str = PrintEntity((IClassEntity)entity);

					else if (entity is IndividualEntity)
						str = PrintEntity((IndividualEntity)entity);

					else if (entity is SeparatorEntity)
						str = PrintEntity((SeparatorEntity)entity);

					else
						str = PrintEntity(entity);

					if (str != string.Empty)
					{
						sw.WriteLine();
						sw.WriteLine(str);
					}
				}
				sw.WriteLine(PrintFooter());
				sw.Close();
			}
		}

		#endregion

		#region Print functions

		protected string PrintHeader()
		{
			string str = string.Empty;
			str += 
@"<rdf:RDF 
	xmlns     =""http://www.javatechlab.com/test#""
	xml:base  =""http://www.javatechlab.com/test#""
	xmlns:owl =""http://www.w3.org/2002/07/owl#""
	xmlns:rdf =""http://www.w3.org/1999/02/22-rdf-syntax-ns#""
	xmlns:rdfs=""http://www.w3.org/2000/01/rdf-schema#""
	xmlns:xsd =""http://www.w3.org/2001/XMLSchema#"">"
			;
			str += NewLine + NewLine;

			str += 
@"<owl:Ontology rdf:about=""""> 
	<rdfs:comment>Autogenerated test ontology.</rdfs:comment>""
</owl:Ontology>"
			;
			return str;
		}

		protected string PrintFooter()
		{
			return @"</rdf:RDF>";
		}

		protected string PrintEntity(PropertyEntity propertyEntity)
		{
			string str = string.Empty;
			//print domain
			//if (propertyEntity.Domain != null)
			//{
			//    str += NewLine + @"<rdfs:domain rdf:resource=""" + propertyEntity.Domain.Identity + @""" />";
			//}
			//print range
			if (propertyEntity.Range != null)
			{
				str += NewLine + @"<rdfs:range rdf:resource=""" + propertyEntity.Range.Name + @""" />";
			}
			//print header
			string sHeaderBegin = @"<owl:ObjectProperty rdf:ID=""" + propertyEntity.Identity.Replace(' ', '_');

			//if body os empty print "/>", else print full footer("</...>").
			if (str != string.Empty)
			{
				str = sHeaderBegin + @""">" + str;
				//print footer
				str += NewLine + @"</owl:ObjectProperty>";
			}
			else
			{
				str = sHeaderBegin + @"""/>";
			}

			return str;
		}

		protected string PrintEntity(IClassEntity classEntity)
		{
			return @"<owl:Class rdf:ID=""" + classEntity.Identity.Replace(' ', '_') + @"""/>";
		}

		protected string PrintEntity(IndividualEntity individualEntity)
		{
			return @"<owl:Individual rdf:ID=""" + individualEntity.Identity.Replace(' ', '_') + @"""/>";
		}

		protected string PrintEntity(SeparatorEntity separatorEntity)
		{
			return string.Empty;
		}

		protected string PrintEntity(UnknownEntity unknownEntity)
		{
			return string.Empty;
		}

		protected string PrintEntity(IEntity entity)
		{
			return entity.Identity;
		}

		#endregion
	}
}