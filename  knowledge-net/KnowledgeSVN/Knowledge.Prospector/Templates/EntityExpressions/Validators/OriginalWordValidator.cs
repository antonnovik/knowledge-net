using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Templates.EntityExpressions.Validators
{
	class OriginalWordValidator : BaseValidator
	{
		private string Word;
		public OriginalWordValidator(string word)
		{
			Word = word;
		}

		public override bool IsValid(IEntity entity)
		{
			return entity.Value.ToUpper() == Word;
		}

		public override string ToString()
		{
			return string.Format("OriginalWordValidator: {0}", Word);
		}
	}
}
