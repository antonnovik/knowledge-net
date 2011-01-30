namespace Knowledge.Prospector.Languages
{
	public class DefaultLanguage : Language, ILanguage
	{
		#region SingleTone

		/// <summary>
		/// Hide constructor
		/// </summary>
		private DefaultLanguage() { }

		/// <summary>
		/// Used for storing single exemplar of class
		/// </summary>
		private static DefaultLanguage _instance;

		/// <summary>
		/// Not more then one instance at one time
		/// </summary>
		/// <returns>Exemplar of class</returns>
		public static DefaultLanguage GetInstance()
		{
			if (_instance == null)
			{
				_instance = new DefaultLanguage();
			}
			return _instance;
		}

		#endregion

		#region ILanguage Members

		public override string Name
		{
			get { return "Default"; }
		}

		#endregion
	}
}
