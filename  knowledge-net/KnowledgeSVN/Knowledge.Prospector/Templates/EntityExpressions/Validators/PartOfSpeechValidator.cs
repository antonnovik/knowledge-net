using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Morphology.Grammar;

namespace Knowledge.Prospector.Templates.EntityExpressions.Validators
{
	public class PartOfSpeechValidator : BaseValidator
	{
		private PartOfSpeech PoS;

		public PartOfSpeechValidator(PartOfSpeech pos)
		{
			PoS = pos;
		}

		public override bool IsValid(IEntity entity)
		{
			if (entity is ChangeableEntity)
				return IsValid(entity as ChangeableEntity);
			return entity.PartOfSpeech.Is(PoS);
		}

		public bool IsValid(ChangeableEntity changeableEntity)
		{
			foreach (IEntity entity in changeableEntity.AllowedEntities)
			{
				if (IsValid(entity))
				{
					changeableEntity.ChangeToEntity = entity;
					return true;
				}
			}
			return false;
		}
	}
}
