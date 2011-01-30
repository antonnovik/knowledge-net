using System;
using System.Collections.Generic;

namespace Knowledge.Prospector.Common.Settings
{
	/// <summary>
	/// Holds information about specified provider from config.
	/// </summary>
	/// <typeparam name="T">Type of provided class.</typeparam>
	public class ProviderConfiguration<T> where T : class
	{
		private List<Provider<T>> _Providers;

		public List<Provider<T>> Providers
		{
			get { return _Providers; }
			set { _Providers = value; }
		}

		public ProviderConfiguration()
		{
			_Providers = new List<Provider<T>>();
		}

		public Provider<T> this[string providerName]
		{
			get
			{
				int index = GetIndex(providerName);
				if (index != -1)
					return Providers[index];

				throw new ArgumentException(string.Format("There is provider with name {0}.", providerName), "providerName");
			}
		}
		
		public int GetIndex(string providerName)
		{
			for(int i=0; i< Providers.Count;i++)
			{
				if (Providers[i].Name == providerName)
					return i;
			}
			return -1;
		}
	}
}
