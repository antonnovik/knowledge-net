using System;
using System.Collections.Generic;
using Knowledge.Prospector.Common.Settings;
using Knowledge.Prospector.Common.Utilites;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Languages;
using Knowledge.Prospector.Morphology;
using Knowledge.Prospector.Morphology.Grammar;

namespace Knowledge.Prospector.Dictionaries
{
	public class MrdDictionaryAdapter : MrdDictionary, IDictionary, ISettingsManaged<MrdDictionaryAdapterOptions>
	{
		private GramTab GramTable;

		#region IDictionary Members

		public IEntity Translate(string word)
		{
			if (!IsInited)
				RealInit();

			FullLemmaInfo[] fis = this.Find(word);

			return CreateEntity(word, fis);

		}

		private ILanguage[] languages;
		public ILanguage[] Languages
		{
			get { return languages; }
		}

		#endregion

		#region IOptionsManaged<MrdDictionaryAdapterOptions> Members

		private bool _IsInited = false;
		public bool IsInited
		{
			get { return _IsInited; }
			private set { _IsInited = value; }
		}
		
		private MrdDictionaryAdapterOptions Options;

		/// <summary>
		/// Will not be initialized for translating. That stage occures with first translating.
		/// </summary>
		/// <param name="options"></param>
		public void Init(MrdDictionaryAdapterOptions options)
		{
			if (options == null)
				throw new NullReferenceException("MrdDictionaryAdapterOptions");
			Options = options;

			ILanguage lang = LanguageManager.GetInstance().GetLanguage(options.Language);
			languages = new ILanguage[] { lang };
			if (lang is Russian)
				GramTable = new RusGramTab();
			else if (lang is English)
				GramTable = new EngGramTab();
			else throw new Exception("Unsupported grammatical table language: " + lang.Name);
		}

		#endregion

		/// <summary>
		/// Load all data from files.
		/// </summary>
		private void RealInit()
		{
			LoadMrd(PathUtils.Decode(Options.MrdFileName));
			GramTable.Read(PathUtils.Decode(Options.GramTabFileName));
			IsInited = true;
		}

		#region Create Entity

		public IEntity CreateEntity(string word, FullLemmaInfo[] fis)
		{
			if (fis == null || fis.Length == 0)
				return CreateUnknownEntity(word);

			if (fis.Length == 1)
				return CreateSimpleEntity(word, fis[0]);

			return CreateChangeableEntity(word, fis);
		}

		private UnknownEntity CreateUnknownEntity(string word)
		{
			return new UnknownEntity(word);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		/// <remarks>Need to change returned UnknownEntity's Value.</remarks>
		private IEntity CreateSimpleEntity(string word, FullLemmaInfo info)
		{
			if (!this.GramTable.ContainsKey(info.CommonMF.Gramcode))
				throw new Exception("Gramcode error");

			if (info == FullLemmaInfo.Unknown)
				return null;

			IEntity result = null;

			PartOfSpeech pos, currentPos;
			try
			{
				pos = this.GramTable[info.CommonMF.Gramcode].PartOfSpeech;
				currentPos = this.GramTable[info.CurrentMF.Gramcode].PartOfSpeech;
			}
			catch
			{
				return CreateUnknownEntity(word);
			}


			if (pos == PartOfSpeech.Noun)
			{
				result = new ClassEntity(info.CommonLemma);
				result.PartOfSpeech = currentPos;
			}
			else if (pos == PartOfSpeech.Adjective
				|| pos == PartOfSpeech.AdjectiveFull
				|| pos == PartOfSpeech.AdjectiveShort)
			{
				result = new PropertyEntity(info.CommonLemma);
				result.PartOfSpeech = currentPos;
			}
			else
			{
				result = new UnknownEntity(info.CommonLemma);
				result.PartOfSpeech = currentPos;
			}

			result.MorphologicalInfo = info;
			return result;
		}

		private IEntity CreateChangeableEntity(string word, FullLemmaInfo[] fis)
		{
			List<IEntity> result = new List<IEntity>();
			foreach (FullLemmaInfo fli in fis)
			{
				IEntity entity = CreateSimpleEntity(word, fli);
				if (entity != null)
					result.Add(entity);
			}
			if (CheckChangeableEntity(result))
				return new ChangeableEntity(word, result.ToArray());
			else
				return result[0];
		}

		private bool CheckChangeableEntity(List<IEntity> allowedEntities)
		{
			Type type = allowedEntities[0].GetType();
			foreach (IEntity entity in allowedEntities)
				if (entity.GetType() != type)
					return true;
			return false;
		}

		#endregion
	}
}
