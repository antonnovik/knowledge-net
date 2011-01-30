using System;
using Knowledge.Prospector.Morphology.Grammar;

namespace Knowledge.Prospector.Templates.EntityExpressions.Validators
{
	class PartOfSpeechValidatorFactory : ValidatorFactory<PartOfSpeechValidator>
	{
		private static readonly PartOfSpeechValidatorFactory _Instance = new PartOfSpeechValidatorFactory();

		public static PartOfSpeechValidatorFactory Instance
		{
			get { return _Instance; }
		}

		protected override PartOfSpeechValidator ParseValidatorKey(string key)
		{
			switch (key)
			{
				case "ADJECTIVE":
					return new PartOfSpeechValidator(PartOfSpeech.Adjective);
				case "NOUN":
					return new PartOfSpeechValidator(PartOfSpeech.Noun);
				case "NUMERAL":
					return new PartOfSpeechValidator(PartOfSpeech.Numeral);
				case "VERB":
					return new PartOfSpeechValidator(PartOfSpeech.Verb);
				case "PARTICIPLE":
					return new PartOfSpeechValidator(PartOfSpeech.Participle);
				default:
					throw new Exception("Unsupproted entity type \"" + key + "\".");
			}
		}
	}
}
