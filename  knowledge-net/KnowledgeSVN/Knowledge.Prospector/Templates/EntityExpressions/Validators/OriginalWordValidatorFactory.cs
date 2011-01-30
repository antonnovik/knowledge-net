using System;

namespace Knowledge.Prospector.Templates.EntityExpressions.Validators
{
	class OriginalWordValidatorFactory : ValidatorFactory<OriginalWordValidator>
	{
		private static readonly OriginalWordValidatorFactory _Instance = new OriginalWordValidatorFactory();

		public static OriginalWordValidatorFactory Instance
		{
			get { return _Instance; }
		}

		protected override OriginalWordValidator ParseValidatorKey(string key)
		{
			string result = string.Empty;
			for (int i = 0; i < key.Length; i++)
			{
				if (key[i] == '\\')
				{
					i++;
					if (!(i < key.Length))
						throw new ArgumentException("There are no character after '\\' in word " + key, "word");
					result += key[i];
				}
				else
				{
					result += key[i];
				}
			}
			return new OriginalWordValidator(result.ToUpper());
		}
	}
}
