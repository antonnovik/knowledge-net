namespace Knowledge.Prospector.Dictionaries
{
	/// <summary>
	/// Summary description for RussianToEntityDictionary.
	/// </summary>
	public class RussianXmlDictionary : XmlDictionary, IDictionary
	{
		public RussianXmlDictionary(string fileName)
		{
			base.Init(fileName);
		}



	}
}
