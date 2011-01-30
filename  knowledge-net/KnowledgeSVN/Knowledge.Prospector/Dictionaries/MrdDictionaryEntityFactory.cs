using System;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Morphology;

namespace Knowledge.Prospector.Dictionaries
{
	internal class MrdDictionaryEntityFactory
	{
		private static readonly MrdDictionaryEntityFactory _Instance = new MrdDictionaryEntityFactory();

		public static MrdDictionaryEntityFactory Instance
		{
			get { return _Instance; }
		}

		private MrdDictionaryAdapter _MrdDictionaryAdapter
		public void Init(MrdDictionaryAdapter mrdDictionaryAdapter)
		{
			_MrdDictionaryAdapter = mrdDictionaryAdapter;
		}

		public IEntity CreateEntity(string word, FullLemmaInfo[] fis)
		{
			if (fis == null || fis.Length == 0)
				return CreateUnknownEntity(word);

			if (fis.Length == 1)
				return CreateOrdinalEntity(word, fis[0]);

			List<IEntity> result = new List<IEntity>();
			foreach (FullLemmaInfo fli in fis)
			{
				IEntity entity = Translate(fli);
				if (entity != null)
					result.Add(entity);
			}
			if (CheckChangeableEntity(result))
				return new ChangeableEntity(word, result.ToArray());
			else
				return result[0];
		}

		private UnknownEntity CreateUnknownEntity(string word)
		{
			return new UnknownEntity(word);
		}

		private bool CheckChangeableEntity(List<IEntity> allowedEntities)
		{
			Type type = allowedEntities[0].GetType();
			foreach (IEntity entity in allowedEntities)
				if (entity.GetType() != type)
					return true;
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		/// <remarks>Need to change returned UnknownEntity's Value.</remarks>
		private IEntity CreateOrdinalEntity(string word, FullLemmaInfo info)
		{
			if (!this.GramTable.ContainsKey(info.CommonMF.Gramcode))
				throw new Exception("Gramcode error");

			if (info == FullLemmaInfo.Unknown)
				return null;

			IEntity result = null;

			PartOfSpeech pos = this.GramTable[info.CommonMF.Gramcode].PartOfSpeech;

			if (pos == PartOfSpeech.Noun)
			{
				result = new ClassEntity(info.CommonLemma);
			}
			else if (pos == PartOfSpeech.Adjective
				|| pos == PartOfSpeech.AdjectiveFull
				|| pos == PartOfSpeech.AdjectiveShort)
			{
				result = new PropertyEntity(info.CommonLemma);
			}
			else
			{
				result = new UnknownEntity(info.CommonLemma);
			}

			result.PartOfSpeech = pos;
			return result;
		}

	}
}
