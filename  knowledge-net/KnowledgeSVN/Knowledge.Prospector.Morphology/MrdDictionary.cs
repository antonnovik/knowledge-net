#define TESTING_FINDING_

#define ENABLE_FAST_FINDING_

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Knowledge.Prospector.Common;

namespace Knowledge.Prospector.Morphology
{
	[Serializable]
	public class MrdDictionary
	{
		/// <summary>
		/// List of all acceptable Flexia Models.
		/// </summary>
		public List<FlexiaModel> FlexiaModels;

		/// <summary>
		/// UNUSED!
		/// List of all acceptable AccentModels.
		/// </summary>
		private List<AccentModel> AccentModels;

		/// <summary>
		/// List of all prefix sets.
		/// </summary>
		public List<Set<string>> PrefixSets;

#if ENABLE_FAST_FINDING
		public Dictionary<string, Set<LemmaInfo>> Lemmas;
#endif

		/// <summary>
		/// Store all wordform of all lemmas.
		/// </summary>
		private MrdTree Tree;

		public MrdDictionary()
		{
			FlexiaModels = new List<FlexiaModel>();
			AccentModels = new List<AccentModel>();
			PrefixSets = new List<Set<string>>();
#if ENABLE_FAST_FINDING
			Lemmas = new Dictionary<string, Set<LemmaInfo>>();
#endif
			Tree = new MrdTree(this);
		}


		#region Read functions

		/// <summary>
		/// Read list of flexia models from stream.
		/// </summary>
		/// <param name="sr">Stream reader.</param>
		/// <returns>List of flexia models.</returns>
		/// <remarks>
		/// Structure of stream:
		/// FlexiaModelsCount(int)
		/// FlexiaModel[0]
		/// ...
		/// FlexiaModel[FlexiaModelsCount-1]
		/// </remarks>
		private List<FlexiaModel> ReadFlexiaModels(StreamReader sr)
		{
			List<FlexiaModel> tempFlexiaModels = new List<FlexiaModel>();

			int paradigm_count = int.Parse(sr.ReadLine());

			for (int num = 0; num < paradigm_count; num++)
			{
				string line = sr.ReadLine().Trim();
				FlexiaModel fm = new FlexiaModel();
				if (!fm.ReadFromString(line))
					throw new Exception(string.Format("Cannot parse paradigm No %i", num + 1));

				tempFlexiaModels.Add(fm);
			}

			return tempFlexiaModels;
		}

		/// <summary>
		/// HOLLOW BLOCK! Read list of accent models from stream.
		/// </summary>
		/// <param name="sr">Stream reader.</param>
		/// <returns>List of accent models.</returns>
		/// <remarks>
		/// Structure of stream:
		/// AccentModelsCount(int)
		/// AccentModel[0]
		/// ...
		/// AccentModel[AccentModelsCount-1]
		/// </remarks>
		private List<AccentModel> ReadAccentModels(StreamReader sr)
		{
			List<AccentModel> tempAccentModels = new List<AccentModel>();

			int count = int.Parse(sr.ReadLine());
			for (int b = 0; b < count; b++)
			{
				string buffer = sr.ReadLine().Trim();

				//				CAccentModel M = new CAccentModel();
				//				if (!M.ReadFromString(buffer))
				//					throw new Exception(string.Format("Cannot parse line %s", buffer));

				//				AccentModels.Add(M);

			}
			return tempAccentModels;
		}

		/// <summary>
		/// HOLLOW BLOCK! Read user session.
		/// </summary>
		/// <param name="sr"></param>
		private void ReadSessions(StreamReader sr)
		{
			int count = int.Parse(sr.ReadLine());
			for (int b = 0; b < count; b++)
			{
				sr.ReadLine();
			}
		}

		/// <summary>
		/// Read list of lemmas from stream.
		/// </summary>
		/// <param name="sr"></param>
		private void ReadLemmas(StreamReader sr)
		{
			int base_count = int.Parse(sr.ReadLine());

			for (int b = 0; b < base_count; b++)
			{
				/* 
				 * [0] = <base>
				 * [1] = <flex_model_no>
				 * [2] = <accent_model_no>
				 * [3] = <session_no>
				 * [4] = <type_ancode>
				 * [5] = <prefix_set_no>
				 */
				string[] datas = sr.ReadLine().Trim().Split(' ');
				int FlexiaModelNo = int.Parse(datas[1]);

				//Check CommonAncode
				if (datas[4] == "-")
					datas[4] = string.Empty;

				//Check PrefixSetNo
				//	if (datas[5] != "-")
				//		if ((datas[5].Length == 0) || !int.TryParse(datas[5], out PrefixSetNo))
				//			throw new Exception("Cannot parse line ");

#if TESTING_FINDING
				if (b % 10000 == 0)
				{
					//			if (b % 50000 == 0 && b!=0 )
					//				Foo();
				}
#endif



				LemmaInfo li = new LemmaInfo(FlexiaModelNo, int.Parse(datas[2]), datas[4]);

				string lemm = datas[0];

				if (lemm == "#")
					lemm = string.Empty;

				Tree.AppendLemmaInfo(lemm, li);

#if ENABLE_FAST_FINDING
				lemm += FlexiaModels[FlexiaModelNo].FirstFlex;

				if (!Lemmas.ContainsKey(lemm))
					Lemmas[lemm] = new Set<LemmaInfo>();

				Lemmas[lemm].Add(li);
#endif
			}
#if TESTING_FINDING
			Foo();
#endif
		}

#if TESTING_FINDING
		public void Foo()
		{
			string str = "красивый";

			while (str != "quit")
			{
#if ENABLE_FAST_FINDING
				FullLemmaInfo[] fastResult = FastFindAllWord(str);
#endif
				FullLemmaInfo[] mrdTreeResult = Tree.Find(str);
			}
		}
#endif

		/// <summary>
		/// Reads one prefix set from string.
		/// </summary>
		/// <param name="PrefixSetStr">String consists of set of prefixes.</param>
		/// <returns>Set of prefixes.</returns>
		/// <remarks>
		/// Structure of string:
		/// Prefix[0] { ',' | ' ' | '\t' | '\r' | '\n' } Prefix[1]...
		/// </remarks>
		private Set<string> ReadOnePrefixSet(string PrefixSetStr)
		{
			Set<string> Result = new Set<string>();

			PrefixSetStr = PrefixSetStr.ToUpper().Trim();

			foreach (string str in PrefixSetStr.Split(',', ' ', '\t', '\r', '\n'))
				Result[str] = true;
			return Result;

		}

		/// <summary>
		/// Reads prefix sets.
		/// </summary>
		/// <param name="sr">Stream reader.</param>
		/// <remarks>
		/// Structure of stream:
		/// PrefixSetsCount(int)
		/// PrefixSet[0]
		/// ...
		/// PrefixSet[PrefixSetsCount-1]
		/// </remarks>
		private void ReadPrefixSets(StreamReader sr)
		{
			PrefixSets.Clear();

			int count = int.Parse(sr.ReadLine());
			for (int b = 0; b < count; b++)
			{
				Set<string> PrefixSet = ReadOnePrefixSet(sr.ReadLine());
				if (PrefixSet.Count == 0)
					throw new Exception("No prefixes found in prefix sets section");

				PrefixSets.Add(PrefixSet);
			}
		}

		//	Загружает *.mrd file.
		//---------------------------------------------------
		//	Описание формата *.mrd.

		//	file: paradigm_number
		//		paradigm |
		//		...	 } paradigm_number times
		//		paradigm |
		//	      base_number
		//		base PNUM |
		//		...	  } base_number times
		//		base PNUM |
		//	paradigm: DICT_TYPE DEPR form ...
		//	base: неизменяемая часть слова, или # если неизменяемая часть пустая
		//	DICT_TYPE: тип словаря, одна буква
		//	DEPR: одна буква (не используется, всегда '#')
		//	form: % FLEX * ancode ...
		//
		//	PNUM - номер парадигиы, начиная с 0
		//	
		//	FLEX: окончание.
		//
		//	Первое окончание - окончание нормальной формы.
		//	Остальные окончания отсортированы по алфавиту.
		//	Внутри одной формы анкоды отсортированы по алфавиту
		//	(сначала маленькие буквы, потом большие)

		//	Пробела внутри парадигмы нет.
		//---------------------------------------------------
		public void LoadMrd(string path)
		{
			StreamReader sr = new StreamReader(path, Encoding.Default);

			FlexiaModels = ReadFlexiaModels(sr);

			AccentModels = ReadAccentModels(sr);

			ReadSessions(sr);

			ReadPrefixSets(sr);

			ReadLemmas(sr);

			sr.Close();

#if ENABLE_FAST_FINDING
			FastInit();
#endif
		}

		#endregion

		public FullLemmaInfo[] Find(string word)
		{
			return Tree.Find(word);
		}

		#region Old/Slow Find functions

		//private FullLemmaInfo SlowFindWord(string word)
		//{
		//    Set<FullLemmaInfo> result = SlowFindAllWord(word);

		//    if (result.Count == 0)
		//        return FullLemmaInfo.Unknown;

		//    FullLemmaInfo maxFlex = new FullLemmaInfo();
		//    foreach (FullLemmaInfo fi in result.Keys)
		//    {
		//        if (fi.CurrentMF.FlexiaStr != null
		//            && (maxFlex.CurrentMF.FlexiaStr == null
		//            || fi.CurrentMF.FlexiaStr.Length > maxFlex.CurrentMF.FlexiaStr.Length))
		//            maxFlex = fi;
		//    }
		//    return maxFlex;
		//}

		//private Set<FullLemmaInfo> SlowFindAllWord(string word)
		//{
		//    word = word.ToUpper();
		//    Set<FullLemmaInfo> result = new Set<FullLemmaInfo>();
		//    if (FlexiaModels != null)
		//        for (int iFlexiaModelNo = 0; iFlexiaModelNo < FlexiaModels.Count; iFlexiaModelNo++)
		//        {
		//            FlexiaModel flexiaModel = this.FlexiaModels[iFlexiaModelNo];
		//            if (flexiaModel.Flexia != null && flexiaModel.Flexia.Values.Count != 0)
		//                foreach (MorphologicForm morphForm in flexiaModel.Flexia.Values)
		//                {
		//                    if (word.EndsWith(morphForm.FlexiaStr) && (word.StartsWith(morphForm.PrefixStr)))
		//                    {
		//                        string commonWord = word.Substring(morphForm.PrefixStr.Length, word.Length - morphForm.FlexiaStr.Length - morphForm.PrefixStr.Length) + flexiaModel.FirstFlex;
		//                        if (Lemmas.ContainsKey(commonWord))
		//                        {
		//                            foreach(
		//                            if (Lemmas[commonWord].FlexiaModelNo == iFlexiaModelNo)
		//                            {
		//                                FullLemmaInfo fi = new FullLemmaInfo(Lemmas[commonWord].FlexiaModelNo,
		//                                    Lemmas[commonWord].AccentModelNo,
		//                                    Lemmas[commonWord].CommonAncode);
		//                                fi.CommonLemma = commonWord;
		//                                fi.CommonMF = flexiaModel.FirstMF;
		//                                fi.CurrentMF = morphForm;
		//                                result.Add(fi);
		//                            }
		//                        }
		//                    }
		//                }
		//        }

		//    return result;
		//}

		#endregion

		#region New/Fast find functions

#if ENABLE_FAST_FINDING
		public FullLemmaInfo FastFindWord(string word)
		{
			//Set<FullLemmaInfo> result = FindAllWord(word);
			FullLemmaInfo[] result = FastFindAllWord(word);

			if (result.Length == 0)
				return FullLemmaInfo.Unknown;

			FullLemmaInfo maxFlex = new FullLemmaInfo();
			foreach (FullLemmaInfo fi in result)
			{
				if (fi.CurrentMF.FlexiaStr != null
					&& (maxFlex.CurrentMF.FlexiaStr == null
					|| fi.CurrentMF.FlexiaStr.Length > maxFlex.CurrentMF.FlexiaStr.Length))
					maxFlex = fi;
			}
			return maxFlex;
		}


		/// <summary>
		/// string = flex.
		/// List&lt;int&gt; = flexNos.
		/// [] = count of chars in flex.
		/// </summary>
		private Dictionary<string, List<int>>[] FastFlexia;

		private void FastAddFlexNo(string flex, int no)
		{
			Dictionary<string, List<int>> dict = FastFlexia[flex.Length];

			List<int> nos;
			if (dict.TryGetValue(flex, out nos))
			{
				nos.Add(no);
			}
			else
			{
				nos = new List<int>();
				nos.Add(no);

				FastFlexia[flex.Length][flex] = nos;
			}
		}


		public void FastInit()
		{
			FastFlexia = new Dictionary<string, List<int>>[20];
			for (int i = 0; i < 20; i++)
				FastFlexia[i] = new Dictionary<string, List<int>>();

			if (FlexiaModels != null)
				for (int iFlexiaModelNo = 0; iFlexiaModelNo < FlexiaModels.Count; iFlexiaModelNo++)
				{
					FlexiaModel flexiaModel = this.FlexiaModels[iFlexiaModelNo];
					if (flexiaModel.Flexia != null && flexiaModel.Flexia.Count != 0)
						foreach (MorphologicForm morphForm in flexiaModel.Flexia.Values)
							FastAddFlexNo(morphForm.FlexiaStr, iFlexiaModelNo);
				}
		}

		public FullLemmaInfo[] FastFindAllWord(string word)
		{
			word = word.ToUpper();

			List<FullLemmaInfo> result = new List<FullLemmaInfo>();

			if (Lemmas.ContainsKey(word))
			{
				foreach (LemmaInfo li in Lemmas[word].GetItems())
				{
					FlexiaModel flexiaModel = this.FlexiaModels[li.FlexiaModelNo];
					FullLemmaInfo fi = new FullLemmaInfo(li.FlexiaModelNo,
						li.AccentModelNo,
						li.CommonAncode);
					fi.CommonLemma = word;
					fi.CommonMF = flexiaModel.FirstMF;
					fi.CurrentMF = flexiaModel.FirstMF;
					result.Add(fi);
				}
			}

			for (int i = 0; i <= word.Length && i < FastFlexia.Length; i++)
			{
				string end = word.Substring(word.Length - i);

				List<int> nos;
				if (FastFlexia[i].TryGetValue(end, out nos))
					foreach (int iFlexiaModelNo in nos)
					{
						FlexiaModel flexiaModel = this.FlexiaModels[iFlexiaModelNo];
						MorphologicForm morphForm = flexiaModel.FindMorphologicFormByFlex(end);
						{
							if (word.StartsWith(morphForm.PrefixStr))
							{
								string commonWord = word.Substring(morphForm.PrefixStr.Length, word.Length - morphForm.FlexiaStr.Length - morphForm.PrefixStr.Length) + flexiaModel.FirstFlex;
								if (Lemmas.ContainsKey(commonWord))
								{
									foreach (LemmaInfo li in Lemmas[commonWord].GetItems())
									{
										if (li.FlexiaModelNo == iFlexiaModelNo)
										{
											FullLemmaInfo fi = new FullLemmaInfo(li.FlexiaModelNo,
												li.AccentModelNo,
												li.CommonAncode);
											fi.CommonLemma = commonWord;
											fi.CommonMF = flexiaModel.FirstMF;
											fi.CurrentMF = morphForm;
											result.Add(fi);
										}
									}
								}
							}
						}
					}
			}

			return result.ToArray();
		}
#endif


		#endregion
	}
}
