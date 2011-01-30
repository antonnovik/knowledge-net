using System;
using System.Collections.Generic;
using Knowledge.Prospector.Morphology.Grammar;

namespace Knowledge.Prospector.Morphology
{
	[Serializable]
	public class FlexiaModel
	{
		/// <summary>
		/// Reserved.
		/// </summary>
		private string Comments;


		public Dictionary<string, MorphologicForm> Flexia;

		private MorphologicForm firstMF;
		
		private const string FlexiaModelCommDelim = "q//q";

		public MorphologicForm FindMorphologicFormByFlex(string flex)
		{
			return Flexia[flex];
		}

		public int Count
		{
			get
			{
				return Flexia.Keys.Count;
			}
		}

		public string[] GetAllPrefixes()
		{
			List<string> temp = new List<string>();
			foreach (MorphologicForm mf in Flexia.Values)
			{
				if (mf.PrefixStr != string.Empty)
					temp.Add(mf.PrefixStr);
			}
			return temp.ToArray();
		}

		public string[] GetAllPrefixesIncludeEmpty()
		{
			List<string> temp = new List<string>();
			temp.Add(string.Empty);
			foreach (MorphologicForm mf in Flexia.Values)
			{
				if (mf.PrefixStr != string.Empty)
					temp.Add(mf.PrefixStr);
			}
			return temp.ToArray();
		}

		public string[] GetAllFlexes()
		{
			string[] temp = new string[Flexia.Keys.Count];
			Flexia.Keys.CopyTo(temp, 0);
			return temp;
		}

		public bool HasFlex(string flex)
		{
			return Flexia.ContainsKey(flex);
		}

		/// <summary>
		/// METHOD POTENTIALLY WRONG
		/// </summary>
		/// <param name="word"></param>
		/// <returns></returns>
		public MorphologicForm FindMorphologicFormByWord(string word)
		{
			foreach (MorphologicForm mf in Flexia.Values)
			{
				if (word.EndsWith(mf.FlexiaStr))
					return mf;
			}
			return new MorphologicForm();
		}



		#region Equivalence operators and functions

		public static bool operator ==(FlexiaModel x, FlexiaModel y)
		{
			return x.Flexia == y.Flexia;
		}
		public static bool operator !=(FlexiaModel x, FlexiaModel y)
		{
			return !(x == y);
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public override bool Equals(Object obj)
		{
			// If parameter is null, or cannot be cast to FlexiaModel,
			// return false.
			if (obj == null) return false;
			if (!(obj is FlexiaModel)) return false;
			// Return true if the fields match:
			return this == (FlexiaModel)obj;
		}
		public bool Equals(FlexiaModel fm)
		{
			// If parameter is null, return false:
			if ((object)fm == null) return false;
			// Return true if the fields match:
			return this == fm;
		}

		#endregion

		public bool ReadFromString(string s)
		{
			Flexia = new Dictionary<string, MorphologicForm>();

			int comm = s.IndexOf(FlexiaModelCommDelim);
			if (comm != -1)
			{
				Comments = s.Substring(comm + FlexiaModelCommDelim.Length).Trim();

				s.Trim();
			}
			else
				Comments = string.Empty;

			string[] forms = s.Split(new char[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
			for(int i=0; i < forms.Length; i++)
			{
				int ast = forms[i].IndexOf('*');
				if (ast == -1) return false;
				int last_ast = forms[i].LastIndexOf('*');
				MorphologicForm mf;
				if (last_ast != ast)
				{
					string Prefix = forms[i].Substring(last_ast + 1);
					mf = new MorphologicForm(forms[i].Substring(ast + 1, last_ast - ast - 1), forms[i].Substring(0, ast), Prefix);
				}
				else
				{
					mf = new MorphologicForm(forms[i].Substring(ast + 1), forms[i].Substring(0, ast), string.Empty);
				}

				if (i==0)
					firstMF = mf;

				Flexia[mf.FlexiaStr] = mf;
			}

			return true;

		}

		public override string ToString()
		{
			string Result = string.Empty;
			foreach(MorphologicForm mf in Flexia.Values)
			{
				if (mf.PrefixStr != string.Empty)
					Result = string.Format("{0}%{1}*{2}*{3}", Result, mf.FlexiaStr, mf.Gramcode, mf.PrefixStr);
				else
					Result = string.Format("{0}%{1}*{2}", Result, mf.FlexiaStr, mf.Gramcode);
			}

			if (Comments != string.Empty)
				Result += FlexiaModelCommDelim + Comments;
			return Result;
		}

		public string FirstFlex
		{
			get
			{
				return firstMF.FlexiaStr;
			}
		}

		public string FirstCode
		{
			get
			{
				return firstMF.Gramcode.ToString();
			}
		}

		public MorphologicForm FirstMF
		{
			get
			{
				return firstMF;
			}
		}

		public bool HasAncode(AnCode searchedAncode)
		{
			if (Flexia == null || Flexia.Count == 0)
				return false;
			foreach(MorphologicForm mf in Flexia.Values)
			{
				if (mf.Gramcode == searchedAncode)
					return true;
			}
			return false;
		}

	}
}
