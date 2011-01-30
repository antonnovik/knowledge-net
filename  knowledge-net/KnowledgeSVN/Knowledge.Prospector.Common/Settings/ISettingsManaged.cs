namespace Knowledge.Prospector.Common.Settings
{
	/// <summary>
	/// Interface for classes what is initiated by settings.
	/// </summary>
	/// <typeparam name="TSettingsType"></typeparam>
	public interface ISettingsManaged<TSettingsType>
	{
		void Init(TSettingsType settings);

		bool IsInited { get;}
	}
}
