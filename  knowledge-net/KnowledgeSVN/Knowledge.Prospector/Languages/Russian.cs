namespace Knowledge.Prospector.Languages
{
	/// <summary>
	/// Summary description for Russian.
	/// </summary>
	public class Russian : Language, ILanguage
	{
		#region SingleTone

		/// <summary>
		/// Hide constructor
		/// </summary>
		private Russian() { }

		/// <summary>
		/// Used for storing single exemplar of class
		/// </summary>
		private static Russian _instance;

		/// <summary>
		/// Not more then one instance at one time
		/// </summary>
		/// <returns>Exemplar of class</returns>
		public static Russian GetInstance()
		{
			if (_instance == null)
			{
				_instance = new Russian();
			}
			return _instance;
		}

		#endregion

		#region ILanguage Members

		public override string Name
		{
			get { return "Russian"; }
		}

		#endregion
}
}
