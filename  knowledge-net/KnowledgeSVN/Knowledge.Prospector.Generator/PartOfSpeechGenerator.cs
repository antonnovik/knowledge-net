using System.IO;
using System.Text;

namespace Knowledge.Prospector.Generator
{
	public class PartOfSpeechGenerator : TxtLineManager
	{
		public void Generate(string input, string output)
		{
			StreamWriter sw = new StreamWriter(output);

			Generate(input, sw);

			sw.Close();
		}

		public void Generate(string input, StreamWriter sw)
		{
			StreamReader sr = new StreamReader(input, Encoding.UTF8);

			TxtLine[] lines = Read(sr);

			sr.Close();


			PrintList(sw, Header);


			foreach (TxtFormat format in Formats)
			{
				PrintList(sw, format.Header);

				PrintWithTab(sw, format, string.Format("#region {0}", format.Name));
				sw.WriteLine();

				foreach (TxtLine line in lines)
				{
					string temp = ApplyFormat(format, line);

					if (temp != null)
					{
						ApplyComments(sw, format, line);
						sw.WriteLine(temp);
						sw.WriteLine();
					}
				}

				PrintWithTab(sw, format, "#endregion");

				PrintList(sw, format.Footer);

				sw.WriteLine();
				sw.WriteLine();
			}

			PrintList(sw, Footer);
		}
	}
}
