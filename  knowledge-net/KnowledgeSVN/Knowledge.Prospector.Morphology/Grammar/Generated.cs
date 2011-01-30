using System;
using System.Collections.Generic;

namespace Knowledge.Prospector.Morphology.Grammar
{
	public partial struct GrammaticalCategory
	{
		#region GrammaticalCategory - Constants

		public static readonly GrammaticalCategory Unknown = new GrammaticalCategory(-1);

		public static readonly GrammaticalCategory Plural = new GrammaticalCategory(1);

		public static readonly GrammaticalCategory Singular = new GrammaticalCategory(2);

		public static readonly GrammaticalCategory AllNumbers = new GrammaticalCategory(Singular | Plural);

		public static readonly GrammaticalCategory Nominativ = new GrammaticalCategory(3);

		public static readonly GrammaticalCategory Genitiv = new GrammaticalCategory(4);

		public static readonly GrammaticalCategory Dativ = new GrammaticalCategory(5);

		public static readonly GrammaticalCategory Accusativ = new GrammaticalCategory(6);

		public static readonly GrammaticalCategory Instrumentalis = new GrammaticalCategory(7);

		public static readonly GrammaticalCategory Vocativ = new GrammaticalCategory(8);

		public static readonly GrammaticalCategory AllCases = new GrammaticalCategory(Nominativ | Genitiv | Dativ | Accusativ | Instrumentalis | Vocativ);

		public static readonly GrammaticalCategory Masculinum = new GrammaticalCategory(9);

		public static readonly GrammaticalCategory Feminum = new GrammaticalCategory(10);

		public static readonly GrammaticalCategory Neutrum = new GrammaticalCategory(11);

		public static readonly GrammaticalCategory MascFem = new GrammaticalCategory(12);

		public static readonly GrammaticalCategory AllGenders = new GrammaticalCategory(Masculinum | Feminum | Neutrum);

		public static readonly GrammaticalCategory ShortForm = new GrammaticalCategory(13);

		public static readonly GrammaticalCategory PresentTense = new GrammaticalCategory(14);

		public static readonly GrammaticalCategory FutureTense = new GrammaticalCategory(15);

		public static readonly GrammaticalCategory PastTense = new GrammaticalCategory(16);

		public static readonly GrammaticalCategory AllTimes = new GrammaticalCategory(PresentTense | FutureTense | PastTense);

		public static readonly GrammaticalCategory FirstPerson = new GrammaticalCategory(17);

		public static readonly GrammaticalCategory SecondPerson = new GrammaticalCategory(18);

		public static readonly GrammaticalCategory ThirdPerson = new GrammaticalCategory(19);

		public static readonly GrammaticalCategory AllPersons = new GrammaticalCategory(FirstPerson | SecondPerson | ThirdPerson);

		public static readonly GrammaticalCategory Imperative = new GrammaticalCategory(20);

		public static readonly GrammaticalCategory Animative = new GrammaticalCategory(21);

		public static readonly GrammaticalCategory NonAnimative = new GrammaticalCategory(22);

		public static readonly GrammaticalCategory AllAnimative = new GrammaticalCategory(Animative | NonAnimative);

		public static readonly GrammaticalCategory Comparative = new GrammaticalCategory(23);

		public static readonly GrammaticalCategory Perfective = new GrammaticalCategory(24);

		public static readonly GrammaticalCategory NonPerfective = new GrammaticalCategory(25);

		public static readonly GrammaticalCategory NonTransitive = new GrammaticalCategory(26);

		public static readonly GrammaticalCategory Transitive = new GrammaticalCategory(27);

		public static readonly GrammaticalCategory ActiveVoice = new GrammaticalCategory(28);

		public static readonly GrammaticalCategory PassiveVoice = new GrammaticalCategory(29);

		public static readonly GrammaticalCategory Indeclinable = new GrammaticalCategory(30);

		public static readonly GrammaticalCategory Initialism = new GrammaticalCategory(31);

		public static readonly GrammaticalCategory Patronymic = new GrammaticalCategory(32);

		public static readonly GrammaticalCategory Toponym = new GrammaticalCategory(33);

		public static readonly GrammaticalCategory Organisation = new GrammaticalCategory(34);

		public static readonly GrammaticalCategory Qualitative = new GrammaticalCategory(35);

		public static readonly GrammaticalCategory DeFactoSingTantum = new GrammaticalCategory(36);

		public static readonly GrammaticalCategory Interrogative = new GrammaticalCategory(37);

		public static readonly GrammaticalCategory Demonstrative = new GrammaticalCategory(38);

		public static readonly GrammaticalCategory Name = new GrammaticalCategory(39);

		public static readonly GrammaticalCategory SurName = new GrammaticalCategory(40);

		public static readonly GrammaticalCategory Impersonal = new GrammaticalCategory(41);

		public static readonly GrammaticalCategory Slang = new GrammaticalCategory(42);

		public static readonly GrammaticalCategory Misprint = new GrammaticalCategory(43);

		public static readonly GrammaticalCategory Colloquial = new GrammaticalCategory(44);

		public static readonly GrammaticalCategory Possessive = new GrammaticalCategory(45);

		public static readonly GrammaticalCategory Archaism = new GrammaticalCategory(46);

		public static readonly GrammaticalCategory SecondCase = new GrammaticalCategory(47);

		public static readonly GrammaticalCategory Poetry = new GrammaticalCategory(48);

		public static readonly GrammaticalCategory Profession = new GrammaticalCategory(49);

		public static readonly GrammaticalCategory ObjectCase = new GrammaticalCategory(50);

		public static readonly GrammaticalCategory Narrative = new GrammaticalCategory(51);

		public static readonly GrammaticalCategory Geographics = new GrammaticalCategory(52);

		public static readonly GrammaticalCategory Proper = new GrammaticalCategory(53);

		public static readonly GrammaticalCategory PersonalPronoun = new GrammaticalCategory(54);

		public static readonly GrammaticalCategory Predicative = new GrammaticalCategory(55);

		public static readonly GrammaticalCategory Uncountable = new GrammaticalCategory(56);

		public static readonly GrammaticalCategory ReflexivePronoun = new GrammaticalCategory(57);

		public static readonly GrammaticalCategory DemonstrativePronoun = new GrammaticalCategory(58);

		public static readonly GrammaticalCategory Mass = new GrammaticalCategory(59);

		public static readonly GrammaticalCategory Supremum = new GrammaticalCategory(60);

		public static readonly GrammaticalCategory PresentIndef = new GrammaticalCategory(61);

		public static readonly GrammaticalCategory Infinitive = new GrammaticalCategory(62);

		public static readonly GrammaticalCategory PastIndef = new GrammaticalCategory(63);

		public static readonly GrammaticalCategory PastParticiple = new GrammaticalCategory(64);

		public static readonly GrammaticalCategory Gerund = new GrammaticalCategory(65);

		public static readonly GrammaticalCategory Futurum = new GrammaticalCategory(66);

		public static readonly GrammaticalCategory Conditional = new GrammaticalCategory(67);

		public static readonly GrammaticalCategory ApostropheS = new GrammaticalCategory(68);

		public static readonly GrammaticalCategory Apostrophe = new GrammaticalCategory(69);

		public static readonly GrammaticalCategory Names = new GrammaticalCategory(70);

		#endregion
	}

	[Serializable]
	public static partial class RussianGrammaticalCategories
	{
		#region RussianGrammaticalCategories - Constants

		public static readonly GrammaticalCategory Unknown = new GrammaticalCategory(-1);

		public static readonly GrammaticalCategory Plural = new GrammaticalCategory(1);

		public static readonly GrammaticalCategory Singular = new GrammaticalCategory(2);

		public static readonly GrammaticalCategory AllNumbers = new GrammaticalCategory(Singular | Plural);

		public static readonly GrammaticalCategory Nominativ = new GrammaticalCategory(3);

		public static readonly GrammaticalCategory Genitiv = new GrammaticalCategory(4);

		public static readonly GrammaticalCategory Dativ = new GrammaticalCategory(5);

		public static readonly GrammaticalCategory Accusativ = new GrammaticalCategory(6);

		public static readonly GrammaticalCategory Instrumentalis = new GrammaticalCategory(7);

		public static readonly GrammaticalCategory Vocativ = new GrammaticalCategory(8);

		public static readonly GrammaticalCategory AllCases = new GrammaticalCategory(Nominativ | Genitiv | Dativ | Accusativ | Instrumentalis | Vocativ);

		public static readonly GrammaticalCategory Masculinum = new GrammaticalCategory(9);

		public static readonly GrammaticalCategory Feminum = new GrammaticalCategory(10);

		public static readonly GrammaticalCategory Neutrum = new GrammaticalCategory(11);

		public static readonly GrammaticalCategory MascFem = new GrammaticalCategory(12);

		public static readonly GrammaticalCategory AllGenders = new GrammaticalCategory(Masculinum | Feminum | Neutrum);

		public static readonly GrammaticalCategory ShortForm = new GrammaticalCategory(13);

		public static readonly GrammaticalCategory PresentTense = new GrammaticalCategory(14);

		public static readonly GrammaticalCategory FutureTense = new GrammaticalCategory(15);

		public static readonly GrammaticalCategory PastTense = new GrammaticalCategory(16);

		public static readonly GrammaticalCategory AllTimes = new GrammaticalCategory(PresentTense | FutureTense | PastTense);

		public static readonly GrammaticalCategory FirstPerson = new GrammaticalCategory(17);

		public static readonly GrammaticalCategory SecondPerson = new GrammaticalCategory(18);

		public static readonly GrammaticalCategory ThirdPerson = new GrammaticalCategory(19);

		public static readonly GrammaticalCategory AllPersons = new GrammaticalCategory(FirstPerson | SecondPerson | ThirdPerson);

		public static readonly GrammaticalCategory Imperative = new GrammaticalCategory(20);

		public static readonly GrammaticalCategory Animative = new GrammaticalCategory(21);

		public static readonly GrammaticalCategory NonAnimative = new GrammaticalCategory(22);

		public static readonly GrammaticalCategory AllAnimative = new GrammaticalCategory(Animative | NonAnimative);

		public static readonly GrammaticalCategory Comparative = new GrammaticalCategory(23);

		public static readonly GrammaticalCategory Perfective = new GrammaticalCategory(24);

		public static readonly GrammaticalCategory NonPerfective = new GrammaticalCategory(25);

		public static readonly GrammaticalCategory NonTransitive = new GrammaticalCategory(26);

		public static readonly GrammaticalCategory Transitive = new GrammaticalCategory(27);

		public static readonly GrammaticalCategory ActiveVoice = new GrammaticalCategory(28);

		public static readonly GrammaticalCategory PassiveVoice = new GrammaticalCategory(29);

		public static readonly GrammaticalCategory Indeclinable = new GrammaticalCategory(30);

		public static readonly GrammaticalCategory Initialism = new GrammaticalCategory(31);

		public static readonly GrammaticalCategory Patronymic = new GrammaticalCategory(32);

		public static readonly GrammaticalCategory Toponym = new GrammaticalCategory(33);

		public static readonly GrammaticalCategory Organisation = new GrammaticalCategory(34);

		public static readonly GrammaticalCategory Qualitative = new GrammaticalCategory(35);

		public static readonly GrammaticalCategory DeFactoSingTantum = new GrammaticalCategory(36);

		public static readonly GrammaticalCategory Interrogative = new GrammaticalCategory(37);

		public static readonly GrammaticalCategory Demonstrative = new GrammaticalCategory(38);

		public static readonly GrammaticalCategory Name = new GrammaticalCategory(39);

		public static readonly GrammaticalCategory SurName = new GrammaticalCategory(40);

		public static readonly GrammaticalCategory Impersonal = new GrammaticalCategory(41);

		public static readonly GrammaticalCategory Slang = new GrammaticalCategory(42);

		public static readonly GrammaticalCategory Misprint = new GrammaticalCategory(43);

		public static readonly GrammaticalCategory Colloquial = new GrammaticalCategory(44);

		public static readonly GrammaticalCategory Possessive = new GrammaticalCategory(45);

		public static readonly GrammaticalCategory Archaism = new GrammaticalCategory(46);

		public static readonly GrammaticalCategory SecondCase = new GrammaticalCategory(47);

		public static readonly GrammaticalCategory Poetry = new GrammaticalCategory(48);

		public static readonly GrammaticalCategory Profession = new GrammaticalCategory(49);

		#endregion
	}

	public static partial class RussianGrammaticalCategories
	{
		private static Dictionary<string, GrammaticalCategory> Converter = new Dictionary<string, GrammaticalCategory>();
		static RussianGrammaticalCategories()
		{
			#region RussianGrammaticalCategories - Converter

			Converter["��"] = Plural;

			Converter["��"] = Singular;

			Converter["��"] = Nominativ;

			Converter["��"] = Genitiv;

			Converter["��"] = Dativ;

			Converter["��"] = Accusativ;

			Converter["��"] = Instrumentalis;

			Converter["��"] = Vocativ;

			Converter["��"] = Masculinum;

			Converter["��"] = Feminum;

			Converter["��"] = Neutrum;

			Converter["��-��"] = MascFem;

			Converter["��"] = ShortForm;

			Converter["���"] = PresentTense;

			Converter["���"] = FutureTense;

			Converter["���"] = PastTense;

			Converter["1�"] = FirstPerson;

			Converter["2�"] = SecondPerson;

			Converter["3�"] = ThirdPerson;

			Converter["���"] = Imperative;

			Converter["��"] = Animative;

			Converter["��"] = NonAnimative;

			Converter["�����"] = Comparative;

			Converter["��"] = Perfective;

			Converter["��"] = NonPerfective;

			Converter["��"] = NonTransitive;

			Converter["��"] = Transitive;

			Converter["���"] = ActiveVoice;

			Converter["���"] = PassiveVoice;

			Converter["0"] = Indeclinable;

			Converter["����"] = Initialism;

			Converter["���"] = Patronymic;

			Converter["���"] = Toponym;

			Converter["���"] = Organisation;

			Converter["���"] = Qualitative;

			Converter["����"] = DeFactoSingTantum;

			Converter["����"] = Interrogative;

			Converter["������"] = Demonstrative;

			Converter["���"] = Name;

			Converter["���"] = SurName;

			Converter["����"] = Impersonal;

			Converter["����"] = Slang;

			Converter["���"] = Misprint;

			Converter["����"] = Colloquial;

			Converter["������"] = Possessive;

			Converter["���"] = Archaism;

			Converter["2"] = SecondCase;

			Converter["����"] = Poetry;

			Converter["����"] = Profession;

			#endregion
		}

		public static GrammaticalCategory Convert(string str)
		{
			if (Converter.ContainsKey(str))
				return Converter[str];
			else
				return Unknown;
		}
	}

	[Serializable]
	public static partial class EnglishGrammaticalCategories
	{
		#region EnglishGrammaticalCategories - Constants

		public static readonly GrammaticalCategory Unknown = new GrammaticalCategory(-1);

		public static readonly GrammaticalCategory Plural = new GrammaticalCategory(1);

		public static readonly GrammaticalCategory Singular = new GrammaticalCategory(2);

		public static readonly GrammaticalCategory AllNumbers = new GrammaticalCategory(Singular | Plural);

		public static readonly GrammaticalCategory Nominativ = new GrammaticalCategory(3);

		public static readonly GrammaticalCategory Masculinum = new GrammaticalCategory(9);

		public static readonly GrammaticalCategory Feminum = new GrammaticalCategory(10);

		public static readonly GrammaticalCategory FirstPerson = new GrammaticalCategory(17);

		public static readonly GrammaticalCategory SecondPerson = new GrammaticalCategory(18);

		public static readonly GrammaticalCategory ThirdPerson = new GrammaticalCategory(19);

		public static readonly GrammaticalCategory AllPersons = new GrammaticalCategory(FirstPerson | SecondPerson | ThirdPerson);

		public static readonly GrammaticalCategory Animative = new GrammaticalCategory(21);

		public static readonly GrammaticalCategory Comparative = new GrammaticalCategory(23);

		public static readonly GrammaticalCategory Perfective = new GrammaticalCategory(24);

		public static readonly GrammaticalCategory Organisation = new GrammaticalCategory(34);

		public static readonly GrammaticalCategory Possessive = new GrammaticalCategory(45);

		public static readonly GrammaticalCategory ObjectCase = new GrammaticalCategory(50);

		public static readonly GrammaticalCategory Narrative = new GrammaticalCategory(51);

		public static readonly GrammaticalCategory Geographics = new GrammaticalCategory(52);

		public static readonly GrammaticalCategory Proper = new GrammaticalCategory(53);

		public static readonly GrammaticalCategory PersonalPronoun = new GrammaticalCategory(54);

		public static readonly GrammaticalCategory Predicative = new GrammaticalCategory(55);

		public static readonly GrammaticalCategory Uncountable = new GrammaticalCategory(56);

		public static readonly GrammaticalCategory ReflexivePronoun = new GrammaticalCategory(57);

		public static readonly GrammaticalCategory DemonstrativePronoun = new GrammaticalCategory(58);

		public static readonly GrammaticalCategory Mass = new GrammaticalCategory(59);

		public static readonly GrammaticalCategory Supremum = new GrammaticalCategory(60);

		public static readonly GrammaticalCategory PresentIndef = new GrammaticalCategory(61);

		public static readonly GrammaticalCategory Infinitive = new GrammaticalCategory(62);

		public static readonly GrammaticalCategory PastIndef = new GrammaticalCategory(63);

		public static readonly GrammaticalCategory PastParticiple = new GrammaticalCategory(64);

		public static readonly GrammaticalCategory Gerund = new GrammaticalCategory(65);

		public static readonly GrammaticalCategory Futurum = new GrammaticalCategory(66);

		public static readonly GrammaticalCategory Conditional = new GrammaticalCategory(67);

		public static readonly GrammaticalCategory ApostropheS = new GrammaticalCategory(68);

		public static readonly GrammaticalCategory Apostrophe = new GrammaticalCategory(69);

		public static readonly GrammaticalCategory Names = new GrammaticalCategory(70);

		#endregion
	}

	public static partial class EnglishGrammaticalCategories
	{
		private static Dictionary<string, GrammaticalCategory> Converter = new Dictionary<string, GrammaticalCategory>();
		static EnglishGrammaticalCategories()
		{
			#region EnglishGrammaticalCategories - Converter

			Converter["-1"] = Unknown;

			Converter["pl"] = Plural;

			Converter["sg"] = Singular;

			Converter["nom"] = Nominativ;

			Converter["m"] = Masculinum;

			Converter["f"] = Feminum;

			Converter["1"] = FirstPerson;

			Converter["2"] = SecondPerson;

			Converter["3"] = ThirdPerson;

			Converter["anim"] = Animative;

			Converter["comp"] = Comparative;

			Converter["perf"] = Perfective;

			Converter["org"] = Organisation;

			Converter["poss"] = Possessive;

			Converter["obj"] = ObjectCase;

			Converter["narr"] = Narrative;

			Converter["geo"] = Geographics;

			Converter["prop"] = Proper;

			Converter["pers"] = PersonalPronoun;

			Converter["pred"] = Predicative;

			Converter["uncount"] = Uncountable;

			Converter["ref"] = ReflexivePronoun;

			Converter["dem"] = DemonstrativePronoun;

			Converter["mass"] = Mass;

			Converter["sup"] = Supremum;

			Converter["prsa"] = PresentIndef;

			Converter["inf"] = Infinitive;

			Converter["pasa"] = PastIndef;

			Converter["pp"] = PastParticiple;

			Converter["ing"] = Gerund;

			Converter["fut"] = Futurum;

			Converter["if"] = Conditional;

			Converter["plsq"] = ApostropheS;

			Converter["plsgs"] = Apostrophe;

			Converter["name"] = Names;

			#endregion
		}

		public static GrammaticalCategory Convert(string str)
		{
			if (Converter.ContainsKey(str))
				return Converter[str];
			else
				return Unknown;
		}
	}


}

namespace Knowledge.Prospector.Morphology.Grammar
{
	public partial struct PartOfSpeech
	{
		#region PartOfSpeech - Constants

		/// <summary>
		/// �� ��������� ����� ����
		/// </summary>
		public static readonly PartOfSpeech Unknown = new PartOfSpeech(0);

		/// <summary>
		/// ��� ���������������
		/// </summary>
		public static readonly PartOfSpeech Noun = new PartOfSpeech(1L << 0);

		/// <summary>
		/// ������ ��� ��������������
		/// </summary>
		public static readonly PartOfSpeech AdjectiveFull = new PartOfSpeech(1L << 1);

		/// <summary>
		/// ������� ��� ��������������
		/// </summary>
		public static readonly PartOfSpeech AdjectiveShort = new PartOfSpeech(1L << 2);

		/// <summary>
		/// ��� ��������������
		/// </summary>
		public static readonly PartOfSpeech Adjective = new PartOfSpeech(1L << 1 | 1L << 2);

		/// <summary>
		/// ������
		/// </summary>
		public static readonly PartOfSpeech Verb = new PartOfSpeech(1L << 3);

		/// <summary>
		/// ��� ������������
		/// </summary>
		public static readonly PartOfSpeech Numeral = new PartOfSpeech(CardinalNumeral | OrdinalNumeral);

		/// <summary>
		/// �������������� ������������
		/// </summary>
		public static readonly PartOfSpeech CardinalNumeral = new PartOfSpeech(1L << 4);

		/// <summary>
		/// ���������� ������������
		/// </summary>
		public static readonly PartOfSpeech OrdinalNumeral = new PartOfSpeech(1L << 5);

		/// <summary>
		/// �����������-���������������
		/// </summary>
		public static readonly PartOfSpeech Pronoun = new PartOfSpeech(1L << 6);

		/// <summary>
		/// ������������ ��������������
		/// </summary>
		public static readonly PartOfSpeech PronounAdjective = new PartOfSpeech(1L << 7);

		/// <summary>
		/// �����������-����������
		/// </summary>
		public static readonly PartOfSpeech PronounPredicate = new PartOfSpeech(1L << 8);

		/// <summary>
		/// �������
		/// </summary>
		public static readonly PartOfSpeech Adverb = new PartOfSpeech(1L << 9);

		/// <summary>
		/// ����������
		/// </summary>
		public static readonly PartOfSpeech Predicate = new PartOfSpeech(1L << 10);

		/// <summary>
		/// �������
		/// </summary>
		public static readonly PartOfSpeech Preposition = new PartOfSpeech(1L << 11);

		/// <summary>
		/// ����
		/// </summary>
		public static readonly PartOfSpeech Conjunction = new PartOfSpeech(1L << 12);

		/// <summary>
		/// ����������
		/// </summary>
		public static readonly PartOfSpeech Interjection = new PartOfSpeech(1L << 13);

		/// <summary>
		/// ������� (INP)
		/// </summary>
		public static readonly PartOfSpeech Introductory = new PartOfSpeech(1L << 14);

		/// <summary>
		/// �������
		/// </summary>
		public static readonly PartOfSpeech Particle = new PartOfSpeech(1L << 16);

		/// <summary>
		/// ���������
		/// </summary>
		public static readonly PartOfSpeech Participle = new PartOfSpeech(1L << 17);

		/// <summary>
		/// ������������
		/// </summary>
		public static readonly PartOfSpeech AdverbParticiple = new PartOfSpeech(1L << 18);

		/// <summary>
		/// ������� ���������
		/// </summary>
		public static readonly PartOfSpeech ParticipleShort = new PartOfSpeech(1L << 19);

		/// <summary>
		/// ���������, �������������� ����� �������
		/// </summary>
		public static readonly PartOfSpeech Infinitive = new PartOfSpeech(1L << 20);

		public static readonly PartOfSpeech ModalVerb = new PartOfSpeech(1L << 21);

		public static readonly PartOfSpeech Article = new PartOfSpeech(1L << 22);

		public static readonly PartOfSpeech Poss = new PartOfSpeech(1L << 23);

		#endregion
	}

	[Serializable]
	public static partial class RussianPartOfSpeeches
	{
		#region RussianPartOfSpeeches - Constants

		/// <summary>
		/// �� ��������� ����� ����
		/// </summary>
		public static readonly PartOfSpeech Unknown = new PartOfSpeech(0);

		/// <summary>
		/// ��� ���������������
		/// </summary>
		public static readonly PartOfSpeech Noun = new PartOfSpeech(1L << 0);

		/// <summary>
		/// ������ ��� ��������������
		/// </summary>
		public static readonly PartOfSpeech AdjectiveFull = new PartOfSpeech(1L << 1);

		/// <summary>
		/// ������� ��� ��������������
		/// </summary>
		public static readonly PartOfSpeech AdjectiveShort = new PartOfSpeech(1L << 2);

		/// <summary>
		/// ������
		/// </summary>
		public static readonly PartOfSpeech Verb = new PartOfSpeech(1L << 3);

		/// <summary>
		/// ��� ������������
		/// </summary>
		public static readonly PartOfSpeech Numeral = new PartOfSpeech(CardinalNumeral | OrdinalNumeral);

		/// <summary>
		/// �������������� ������������
		/// </summary>
		public static readonly PartOfSpeech CardinalNumeral = new PartOfSpeech(1L << 4);

		/// <summary>
		/// ���������� ������������
		/// </summary>
		public static readonly PartOfSpeech OrdinalNumeral = new PartOfSpeech(1L << 5);

		/// <summary>
		/// �����������-���������������
		/// </summary>
		public static readonly PartOfSpeech Pronoun = new PartOfSpeech(1L << 6);

		/// <summary>
		/// ������������ ��������������
		/// </summary>
		public static readonly PartOfSpeech PronounAdjective = new PartOfSpeech(1L << 7);

		/// <summary>
		/// �����������-����������
		/// </summary>
		public static readonly PartOfSpeech PronounPredicate = new PartOfSpeech(1L << 8);

		/// <summary>
		/// �������
		/// </summary>
		public static readonly PartOfSpeech Adverb = new PartOfSpeech(1L << 9);

		/// <summary>
		/// ����������
		/// </summary>
		public static readonly PartOfSpeech Predicate = new PartOfSpeech(1L << 10);

		/// <summary>
		/// �������
		/// </summary>
		public static readonly PartOfSpeech Preposition = new PartOfSpeech(1L << 11);

		/// <summary>
		/// ����
		/// </summary>
		public static readonly PartOfSpeech Conjunction = new PartOfSpeech(1L << 12);

		/// <summary>
		/// ����������
		/// </summary>
		public static readonly PartOfSpeech Interjection = new PartOfSpeech(1L << 13);

		/// <summary>
		/// ������� (INP)
		/// </summary>
		public static readonly PartOfSpeech Introductory = new PartOfSpeech(1L << 14);

		/// <summary>
		/// �������
		/// </summary>
		public static readonly PartOfSpeech Particle = new PartOfSpeech(1L << 16);

		/// <summary>
		/// ���������
		/// </summary>
		public static readonly PartOfSpeech Participle = new PartOfSpeech(1L << 17);

		/// <summary>
		/// ������������
		/// </summary>
		public static readonly PartOfSpeech AdverbParticiple = new PartOfSpeech(1L << 18);

		/// <summary>
		/// ������� ���������
		/// </summary>
		public static readonly PartOfSpeech ParticipleShort = new PartOfSpeech(1L << 19);

		/// <summary>
		/// ���������, �������������� ����� �������
		/// </summary>
		public static readonly PartOfSpeech Infinitive = new PartOfSpeech(1L << 20);

		#endregion
	}

	public static partial class RussianPartOfSpeeches
	{
		private static Dictionary<string, PartOfSpeech> Converter = new Dictionary<string, PartOfSpeech>();
		static RussianPartOfSpeeches()
		{
			#region RussianPartOfSpeeches - Converter

			Converter["*"] = Unknown;

			/// <summary>
			/// ��� ���������������
			/// </summary>
			Converter["�"] = Noun;

			/// <summary>
			/// ������ ��� ��������������
			/// </summary>
			Converter["�"] = AdjectiveFull;

			/// <summary>
			/// ������� ��� ��������������
			/// </summary>
			Converter["��_����"] = AdjectiveShort;

			/// <summary>
			/// ������
			/// </summary>
			Converter["�"] = Verb;

			/// <summary>
			/// �������������� ������������
			/// </summary>
			Converter["����"] = CardinalNumeral;

			/// <summary>
			/// ���������� ������������
			/// </summary>
			Converter["����-�"] = OrdinalNumeral;

			/// <summary>
			/// �����������-���������������
			/// </summary>
			Converter["��"] = Pronoun;

			/// <summary>
			/// ������������ ��������������
			/// </summary>
			Converter["��-�"] = PronounAdjective;

			/// <summary>
			/// �����������-����������
			/// </summary>
			Converter["��-�����"] = PronounPredicate;

			/// <summary>
			/// �������
			/// </summary>
			Converter["�"] = Adverb;

			/// <summary>
			/// ����������
			/// </summary>
			Converter["�����"] = Predicate;

			/// <summary>
			/// �������
			/// </summary>
			Converter["�����"] = Preposition;

			/// <summary>
			/// ����
			/// </summary>
			Converter["����"] = Conjunction;

			/// <summary>
			/// ����������
			/// </summary>
			Converter["����"] = Interjection;

			/// <summary>
			/// ������� (INP)
			/// </summary>
			Converter["�����"] = Introductory;

			/// <summary>
			/// �������
			/// </summary>
			Converter["����"] = Particle;

			/// <summary>
			/// ���������
			/// </summary>
			Converter["���������"] = Participle;

			/// <summary>
			/// ������������
			/// </summary>
			Converter["������������"] = AdverbParticiple;

			/// <summary>
			/// ������� ���������
			/// </summary>
			Converter["��_���������"] = ParticipleShort;

			/// <summary>
			/// ���������, �������������� ����� �������
			/// </summary>
			Converter["���������"] = Infinitive;

			#endregion
		}

		public static PartOfSpeech Convert(string str)
		{
			if (Converter.ContainsKey(str))
				return Converter[str];
			else
				return Unknown;
		}
	}

	[Serializable]
	public static partial class EnglishPartOfSpeeches
	{
		#region EnglishPartOfSpeeches - Constants

		/// <summary>
		/// �� ��������� ����� ����
		/// </summary>
		public static readonly PartOfSpeech Unknown = new PartOfSpeech(0);

		/// <summary>
		/// ��� ���������������
		/// </summary>
		public static readonly PartOfSpeech Noun = new PartOfSpeech(1L << 0);

		/// <summary>
		/// ��� ��������������
		/// </summary>
		public static readonly PartOfSpeech Adjective = new PartOfSpeech(1L << 1 | 1L << 2);

		/// <summary>
		/// ������
		/// </summary>
		public static readonly PartOfSpeech Verb = new PartOfSpeech(1L << 3);

		/// <summary>
		/// ��� ������������
		/// </summary>
		public static readonly PartOfSpeech Numeral = new PartOfSpeech(CardinalNumeral | OrdinalNumeral);

		/// <summary>
		/// �������������� ������������
		/// </summary>
		public static readonly PartOfSpeech CardinalNumeral = new PartOfSpeech(1L << 4);

		/// <summary>
		/// ���������� ������������
		/// </summary>
		public static readonly PartOfSpeech OrdinalNumeral = new PartOfSpeech(1L << 5);

		/// <summary>
		/// �����������-���������������
		/// </summary>
		public static readonly PartOfSpeech Pronoun = new PartOfSpeech(1L << 6);

		/// <summary>
		/// ������������ ��������������
		/// </summary>
		public static readonly PartOfSpeech PronounAdjective = new PartOfSpeech(1L << 7);

		/// <summary>
		/// �������
		/// </summary>
		public static readonly PartOfSpeech Adverb = new PartOfSpeech(1L << 9);

		/// <summary>
		/// �������
		/// </summary>
		public static readonly PartOfSpeech Preposition = new PartOfSpeech(1L << 11);

		/// <summary>
		/// ����
		/// </summary>
		public static readonly PartOfSpeech Conjunction = new PartOfSpeech(1L << 12);

		/// <summary>
		/// ����������
		/// </summary>
		public static readonly PartOfSpeech Interjection = new PartOfSpeech(1L << 13);

		/// <summary>
		/// �������
		/// </summary>
		public static readonly PartOfSpeech Particle = new PartOfSpeech(1L << 16);

		public static readonly PartOfSpeech ModalVerb = new PartOfSpeech(1L << 21);

		public static readonly PartOfSpeech Article = new PartOfSpeech(1L << 22);

		public static readonly PartOfSpeech Poss = new PartOfSpeech(1L << 23);

		public static readonly PartOfSpeech VerbToBe = new PartOfSpeech(1L << 24);

		#endregion
	}

	public static partial class EnglishPartOfSpeeches
	{
		private static Dictionary<string, PartOfSpeech> Converter = new Dictionary<string, PartOfSpeech>();
		static EnglishPartOfSpeeches()
		{
			#region EnglishPartOfSpeeches - Converter

			Converter["*"] = Unknown;

			/// <summary>
			/// ��� ���������������
			/// </summary>
			Converter["NOUN"] = Noun;

			/// <summary>
			/// ��� ��������������
			/// </summary>
			Converter["ADJECTIVE"] = Adjective;

			/// <summary>
			/// ������
			/// </summary>
			Converter["VERB"] = Verb;

			/// <summary>
			/// �������������� ������������
			/// </summary>
			Converter["NUMERAL"] = CardinalNumeral;

			/// <summary>
			/// ���������� ������������
			/// </summary>
			Converter["ORDNUM"] = OrdinalNumeral;

			/// <summary>
			/// �����������-���������������
			/// </summary>
			Converter["PRON"] = Pronoun;

			Converter["PN"] = Pronoun;

			/// <summary>
			/// ������������ ��������������
			/// </summary>
			Converter["PN_ADJ"] = PronounAdjective;

			/// <summary>
			/// �������
			/// </summary>
			Converter["ADVERB"] = Adverb;

			/// <summary>
			/// �������
			/// </summary>
			Converter["PREP"] = Preposition;

			/// <summary>
			/// ����
			/// </summary>
			Converter["CONJ"] = Conjunction;

			/// <summary>
			/// ����������
			/// </summary>
			Converter["INT"] = Interjection;

			/// <summary>
			/// �������
			/// </summary>
			Converter["PART"] = Particle;

			Converter["MOD"] = ModalVerb;

			Converter["ARTICLE"] = Article;

			Converter["POSS"] = Poss;

			Converter["VBE"] = VerbToBe;

			#endregion
		}

		public static PartOfSpeech Convert(string str)
		{
			if (Converter.ContainsKey(str))
				return Converter[str];
			else
				throw new Exception(string.Format("No such part of speech: {0}", str));
		}
	}


}
