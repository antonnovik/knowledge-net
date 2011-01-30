using System;

namespace Knowledge.Prospector.Templates.EntityExpressions.Validators
{
	public class ValidatorManager
	{
		private static readonly ValidatorManager _Instance = new ValidatorManager();

		public static ValidatorManager Instance
		{
			get { return _Instance; }
		}

		public BaseValidator GetValidator(string keyword)
		{
			keyword = keyword.Trim();
			switch (keyword[0])
			{
				case '#':
					return GetMetaValidator(keyword.Substring(1));
				default:
					return OriginalWordValidatorFactory.Instance.GetValidator(keyword);
			}
		}

		private BaseValidator GetMetaValidator(string meta)
		{
			meta = meta.Trim().ToUpper();
			if (meta == string.Empty)
				throw new ArgumentException("Cannot create validator for empty string", "meta");
			string first = meta.Substring(0, meta.IndexOf('.'));
			string second = meta.Substring(meta.IndexOf('.') + 1);
			switch (first)
			{
				case "E":
					return TypeValidatorFactory.Instance.GetValidator(second);
				case "M":
					return PartOfSpeechValidatorFactory.Instance.GetValidator(second);
				default:
					throw new ArgumentException("Unsupported meta \"" + meta + "\".");
			}
		}
	}
}
