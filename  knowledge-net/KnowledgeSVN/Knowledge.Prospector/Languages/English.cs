namespace Knowledge.Prospector.Languages
{
	public class English : Language, ILanguage
	{
		#region SingleTone

		/// <summary>
		/// Hide constructor
		/// </summary>
		private English() { }

		/// <summary>
		/// Used for storing single exemplar of class
		/// </summary>
		private static English _instance;

		/// <summary>
		/// Not more then one instance at one time
		/// </summary>
		/// <returns>Exemplar of class</returns>
		public static English GetInstance()
		{
			if (_instance == null)
			{
				_instance = new English();
			}
			return _instance;
		}

		#endregion

		#region ILanguage Members

		public override string Name
		{
			get { return "English"; }
		}

		#endregion
	}
}
