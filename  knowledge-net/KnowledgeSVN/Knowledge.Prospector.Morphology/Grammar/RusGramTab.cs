using System;

namespace Knowledge.Prospector.Morphology.Grammar
{
	[Serializable]
	public class RusGramTab : GramTab
	{
		#region Constructors

		public RusGramTab()
			: base(RusGramTabLineManager.GetInstance())
		{
		}

		#endregion

		#region Useless for this time
		/*

		#region Согласования


		/// <summary>
		/// Стандартное согласование между двумя именами  по  числу и падежу
		/// </summary>
		/// <param name="g1"></param>
		/// <param name="g2"></param>
		/// <returns></returns>
		public bool CaseNumber(rGrammems g1, rGrammems g2)
		{
			return ((rGrammems.rAllCases & g1 & g2) > 0) &&
				   ((rGrammems.rAllNumbers & g1 & g2) > 0);
		}


		/// <summary>
		/// Стандартное согласование между двумя именами  по  падежу, причем первый код должен
		/// иметь граммему множественного числа 
		/// </summary>
		/// <param name="g1"></param>
		/// <param name="g2"></param>
		/// <returns></returns>
		public bool CaseFirstPlural(rGrammems g1, rGrammems g2)
		{
			return (((rGrammems.rAllCases & g1 & g2) > 0)
					 && ((rGrammems.rPlural & g1) > 0)
				   );
		}



		/// <summary>
		/// Стандартное согласование между двумя именами  по роду и числу 
		/// </summary>
		/// <param name="g1"></param>
		/// <param name="g2"></param>
		/// <returns></returns>
		public bool GenderNumber(rGrammems g1, rGrammems g2)
		{

			return ((rGrammems.rAllNumbers & g1 & g2) > 0)
				 && (((g1 & g2 & rGrammems.rPlural) > 0)
					  || ((rGrammems.rAllGenders & g1 & g2) > 0)
					);
		}

		/// <summary>
		/// Стандартное согласование по лицу и числу (для 1 и 2 лица)
		/// </summary>
		/// <param name="g1"></param>
		/// <param name="g2"></param>
		/// <returns></returns>
		public bool PersonNumber(rGrammems g1, rGrammems g2)
		{
			bool t1 = (rGrammems.rAllNumbers & g1 & g2) > 0;
			bool t2 = ((rGrammems.rAllPersons & g1 & g2) > 0);
			return t1 && t2;
		}

		/// <summary>
		/// Стандартное согласование между подлежащим и сказуемым
		/// </summary>
		/// <param name="subj"></param>
		/// <param name="verb"></param>
		/// <returns></returns>
		public bool SubjectPredicate(rGrammems subj, rGrammems verb)
		{
			if (!((subj & rGrammems.rNominativ) > 0))
				return false;


			if (((verb & rGrammems.rPastTense) > 0)
				 || ((verb & rGrammems.rShortForm) > 0)
			  )
			{
				// ты вышел 
				// я вышел
				// ты был
				// я красива
				// мы шли
				// вы шли
				if ((subj & (rGrammems.rFirstPerson | rGrammems.rSecondPerson)) > 0)
					return ((verb & subj & rGrammems.rPlural) > 0)
						|| (((verb & (rGrammems.rMasculinum | rGrammems.rFeminum)) > 0)
						&& ((verb & subj & rGrammems.rSingular) > 0));
				else
					// он вышел
					// поезд ушел
					// девочка красива
					// девочки красивы 	
					// мальчик красив
					return GenderNumber(subj, verb);
			}
			else
				if (((verb & rGrammems.rPresentTense) > 0)
					|| ((verb & rGrammems.rFutureTense) > 0))
				{
					//  я выйду
					//  ты выедешь
					//  мы выйдем
					if ((subj & (rGrammems.rFirstPerson | rGrammems.rSecondPerson)) > 0
						|| (verb & (rGrammems.rFirstPerson | rGrammems.rSecondPerson)) > 0)
						return PersonNumber(subj, verb);
					else
						return (rGrammems.rAllNumbers & subj & verb) > 0;
				}
				else
					if ((verb & rGrammems.rImperative) > 0)
						return ((subj & rGrammems.rSecondPerson) > 0)
								&& ((rGrammems.rAllNumbers & subj & verb) > 0);

			return false;
		}


		/// <summary>
		/// Стандартное согласование между двумя именами  по  падежу 
		/// </summary>
		/// <param name="g1"></param>
		/// <param name="g2"></param>
		/// <returns></returns>
		public bool Case(rGrammems g1, rGrammems g2)
		{

			return ((rGrammems.rAllCases & g1 & g2) > 0);
		}



		/// <summary>
		/// Стандартное согласование между существительным и прилашательнымпо роду, числу и падежу, если 
		/// если существительное одушевленное
		/// </summary>
		/// <param name="gram_noun"></param>
		/// <param name="gram_adj"></param>
		/// <returns></returns>
		bool GenderNumberCaseAnimRussian(rGrammems gram_noun, rGrammems gram_adj)
		{
			return ((rGrammems.rAllCases & gram_noun & gram_adj) > 0)
						 && ((rGrammems.rAllNumbers & gram_noun & gram_adj) > 0)
						 && (((rGrammems.rAnimative & gram_adj) > 0)
								|| ((rGrammems.rAllAnimative & gram_adj) == 0)
							)
						 && (((rGrammems.rAllGenders & gram_noun & gram_adj) > 0)
							  || ((rGrammems.rAllGenders & gram_noun) == 0)
							  || ((rGrammems.rAllGenders & gram_adj) == 0)
							);
		}

		/// <summary>
		/// Стандартное согласование между существительным и прилашательнымпо роду, числу и падежу, если 
		/// если существительное неодушевленное
		/// </summary>
		/// <param name="gram_noun"></param>
		/// <param name="gram_adj"></param>
		/// <returns></returns>
		bool GenderNumberCaseNotAnimRussian(rGrammems gram_noun, rGrammems gram_adj)
		{
			return ((rGrammems.rAllCases & gram_noun & gram_adj) > 0)
						 && ((rGrammems.rAllNumbers & gram_noun & gram_adj) > 0)
						 && (((rGrammems.rNonAnimative & gram_adj) > 0)
								|| ((rGrammems.rAllAnimative & gram_adj) == 0)
							)
						 && (((rGrammems.rAllGenders & gram_noun & gram_adj) > 0)
							  || ((rGrammems.rAllGenders & gram_noun) == 0)
							  || ((rGrammems.rAllGenders & gram_adj) == 0)
							);
		}

		/// <summary>
		/// Стандартное согласование между существительным и прилашательным по роду, числу и падежу, если 
		/// если существительное не неодушевленное и не одушевленное
		///  (для местоимений, например, "все это было хорошо")
		/// </summary>
		/// <param name="gram_noun"></param>
		/// <param name="gram_adj"></param>
		/// <returns></returns>
		bool GenderNumberCaseRussian(rGrammems gram_noun, rGrammems gram_adj)
		{
			return ((rGrammems.rAllCases & gram_noun & gram_adj) > 0)
						 && ((rGrammems.rAllNumbers & gram_noun & gram_adj) > 0)
						 && (((rGrammems.rAllGenders & gram_noun & gram_adj) > 0)
							  || ((rGrammems.rAllGenders & gram_noun) == 0)
							  || ((rGrammems.rAllGenders & gram_adj) == 0)
							);
		}


		#endregion

		#region Gleiche

		public rGrammems Gleiche(rGrammemCompare CompareFunc, AnCode gram_codes1, AnCode gram_codes2)
		{
			if (gram_codes1 == null || gram_codes1 == AnCode.Unknown) return 0;
			if (gram_codes2 == null || gram_codes2 == AnCode.Unknown) return 0;
			rGrammems g1 = this[gram_codes1].Grammems;
			rGrammems g2 = this[gram_codes2].Grammems;
			if (CompareFunc(g1, g2))
				return g1 & g2;
			return 0;
		}

		#region useless
		//private rGrammems USELESS_Gleiche(rGrammemCompare CompareFunc, AnCode[] gram_codes1, AnCode[] gram_codes2)
		//{
		//    rGrammems grammems = 0;
		//    if (gram_codes1 == null || gram_codes1.Length == 0) return 0;
		//    if (gram_codes2 == null || gram_codes2.Length == 0) return 0;
		//    for (int i = 0; i < gram_codes1.Length; i++)
		//        for (int j = 0; j < gram_codes2.Length; j++)
		//        {
		//            if (CompareFunc(gram_codes1[i], gram_codes2[j]))
		//                grammems |= gram_codes1[i] & gram_codes2[j];
		//        }

		//    return grammems;
		//}

		/// <summary>
		///  Uses gleiche to compare ancodes from gram_codes1 with  ancodes gram_codes2
		///  returns all ancodes from gram_codes1, which satisfy CompareFunc
		/// </summary>
		/// <param name="CompareFunc"></param>
		/// <param name="gram_codes1"></param>
		/// <param name="gram_codes2"></param>
		/// <returns></returns>
		private List<AnCode> USELESS_GleicheAncode1(rGrammemCompare CompareFunc, AnCode[] gram_codes1, AnCode[] gram_codes2)
		{
			List<AnCode> Result = new List<AnCode>();
			if (gram_codes1 == null || gram_codes1.Length == 0) return Result;
			if (gram_codes2 == null || gram_codes2.Length == 0) return Result;

			for (int i = 0; i < gram_codes1.Length; i++)
			{
				for (int j = 0; j < gram_codes2.Length; j++)
				{
					if (Gleiche(CompareFunc, gram_codes1[i], gram_codes2[j]) > 0)
					{
						Result.Add(gram_codes1[i]);
						break;
					}
				}
			}

			return Result;
		}
		#endregion

		//Примеры работы функции GleicheGenderNumberCase:
		//- красивого стол;
		//- красивых столы;
		//- красивый пирата;
		//- красивые пиратов;
		//- достойных жизни;
		//- достойные матерей;
		//- длинные чудовищ
		//+ на этот дом;
		//+ красивую маму;
		//+ в красивый  дом;
		//+ в длинное облако;
		//+ примерить его цилиндр;
		//+ все это 
		/// <summary>
		/// проверяет, согласованы ли по роду, числу и падежу грамматические коды
		/// </summary>
		/// <param name="common_gram_code_noun"></param>
		/// <param name="gram_code_noun"></param>
		/// <param name="gram_code_adj"></param>
		/// <returns></returns>
		rGrammems GleicheGenderNumberCase(AnCode common_gram_code_noun, AnCode gram_code_noun, AnCode gram_code_adj)
		{
			if (common_gram_code_noun == string.Empty)
				// нет сведений об одушевленности
				return Gleiche(GenderNumberCaseRussian, gram_code_noun, gram_code_adj);
			else
				if ((this[common_gram_code_noun].Grammems & rGrammems.rNonAnimative) > 0)
					// неодушевленный
					return Gleiche(GenderNumberCaseNotAnimRussian, gram_code_noun, gram_code_adj);
				else
					if ((this[common_gram_code_noun].Grammems & rGrammems.rAnimative) > 0)
						// одушевленный
						return Gleiche(GenderNumberCaseAnimRussian, gram_code_noun, gram_code_adj);
					else
						// нет сведений об одушевленности
						return Gleiche(GenderNumberCaseRussian, gram_code_noun, gram_code_adj);

		}

		/// <summary>
		/// проверяет, согласованы ли по числу и падежу грамматические коды
		/// </summary>
		/// <param name="gram_code1"></param>
		/// <param name="gram_code2"></param>
		/// <returns></returns>
		public bool GleicheCaseNumber(AnCode gram_code1, AnCode gram_code2)
		{
			return Gleiche(CaseNumber, gram_code1, gram_code2) != 0;
		}

		/// <summary>
		/// проверяет, согласованы ли по роду и падежу грамматические коды
		/// </summary>
		/// <param name="gram_code1"></param>
		/// <param name="gram_code2"></param>
		/// <returns></returns>
		bool GleicheGenderNumber(AnCode gram_code1, AnCode gram_code2)
		{
			return Gleiche(GenderNumber, gram_code1, gram_code2) != 0;
		}
		bool GleicheSubjectPredicate(AnCode gram_code1, AnCode gram_code2)
		{
			return Gleiche(SubjectPredicate, gram_code1, gram_code2) != 0;
		}

		/// <summary>
		/// проверяет, согласованы ли по падежу грамматические коды
		/// </summary>
		/// <param name="gram_code1"></param>
		/// <param name="gram_code2"></param>
		/// <returns></returns>
		bool GleicheCase(AnCode gram_code1, AnCode gram_code2)
		{
			return Gleiche(Case, gram_code1, gram_code2) != 0;
		}


		#endregion

		#region Clause

		/// <summary>
		/// истина для предикативных типов клауз.
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public bool IsStrongClauseRoot(rPartOfSpeeches pos)
		{
			return (pos == rPartOfSpeeches.VERB)
					|| (pos == rPartOfSpeeches.ADVERB_PARTICIPLE) // субъект деепричастия  совпадлает с субъектом
				// главной  клаузы 
					|| (pos == rPartOfSpeeches.PARTICIPLE_SHORT)
					|| (pos == rPartOfSpeeches.ADJ_SHORT)
					|| (pos == rPartOfSpeeches.PREDK);
		}

		public bool is_left_noun_modifier(rPartOfSpeeches pos, rGrammems grammems)
		{
			return (pos == rPartOfSpeeches.ADJ_FULL)
					|| (pos == rPartOfSpeeches.NUMERAL_P)
					|| (pos == rPartOfSpeeches.PRONOUN_P)
					|| (pos == rPartOfSpeeches.PARTICIPLE);
		}

		public bool is_morph_participle(rPartOfSpeeches poses)
		{
			return (poses & rPartOfSpeeches.PARTICIPLE
				| poses & rPartOfSpeeches.PARTICIPLE_SHORT) > 0;
		}

		public bool is_verb_form(rPartOfSpeeches poses)
		{
			return is_morph_participle(poses)
					|| ((poses & rPartOfSpeeches.VERB)
					| (poses & rPartOfSpeeches.INFINITIVE)
					| (poses & rPartOfSpeeches.ADVERB_PARTICIPLE))>0;
		}

		private readonly string[] Particles = new string[] { "ЛИ", "ЖЕ", "БЫ", "УЖ", "ТОЛЬКО" };

		public bool IsParticle(string lemma, rPartOfSpeeches poses)
		{
			if (lemma == null) return false;
			if ((poses & rPartOfSpeeches.PARTICLE) <= 0) return false;
			foreach (string str in Particles)
				if (str == lemma)
					return true;
			return false;
		}

	//Есть такие  мест. прилагательные, которые могут выступать в роли существительных:
	//"КАЖДЫЙ", "ОДИН", "ДРУГОЙ","ТОТ","КОТОРЫЙ".
	//Для  них не имеет смысла вводит аналогичные местоимения,поскольку  все они могут 
	//быть использованы во всех родах. Эти прилагательные могут вести себя так же, как
	//существительные. Например,
	//Я купил два яблока, в каждом оказалось много червяков
	//Я знаю ту, которая тебя видит
	//Один пришел, другой ушел.
	//Я вижу дом, который разрушился.
	//Вышеуказанные мест. прилагательные отличаются, например, от местоимения "этот" и "всякий", поскольку	
	//трудно себе представить что-то вроде:
	//"эта пришла"
	//"всякая пришла"
	//Но возможно:
	//"всякое бывает"
	//"это бывает"
	//Здесь "всякое" и "это" - простые местоимения ср рода.

	//!! Таким образом, я не хочу дублировать в морфологии МС-П, если они могут использованы
	//!! во всех родах в качестве существительного.

	//1 марта 2001 года, Сокирко

		private readonly string[] MorphNouns = new string[] { "КАЖДЫЙ", "ОДИН", "ДРУГОЙ", "ТОТ", "КОТОРЫЙ" };
		public bool IsSynNoun(rPartOfSpeeches Poses, string Lemma)
		{
			return Poses == rPartOfSpeeches.NOUN
				|| Poses == rPartOfSpeeches.PRONOUN
				|| (Poses == rPartOfSpeeches.PRONOUN_P
				&& (Lemma == "КАЖДЫЙ"
					|| Lemma == "ОДИН"
					|| Lemma == "ДРУГОЙ"
					|| Lemma == "ТОТ"
					|| Lemma == "КОТОРЫЙ"));
		}

		#endregion

		*/

		#endregion

	}

}
