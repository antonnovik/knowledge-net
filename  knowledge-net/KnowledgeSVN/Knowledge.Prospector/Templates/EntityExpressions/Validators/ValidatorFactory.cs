using Knowledge.Prospector.Common;

namespace Knowledge.Prospector.Templates.EntityExpressions.Validators
{
	public abstract class ValidatorFactory<TValidator> where TValidator : BaseValidator
	{
		private Cache<string, TValidator> ValidatorCache;

		protected ValidatorFactory()
		{
			ValidatorCache = new Cache<string, TValidator>(new Cache<string, TValidator>.ObtainItemDelegate(ParseValidatorKey));
		}

		protected abstract TValidator ParseValidatorKey(string key);

		public TValidator GetValidator(string key)
		{
			return ValidatorCache.GetItem(key);
		}
	}
}
