using System;

namespace Knowledge.Prospector.Morphology.Grammar
{
	[Serializable]
	public class EngGramTab : GramTab
	{
		#region Constructors

		public EngGramTab()
			: base(EngGramTabLineManager.GetInstance())
		{
		}

		#endregion
	}
}
