using System;
using System.Collections.Generic;
using Knowledge.Prospector.Common;

namespace Knowledge.Prospector.Morphology
{
	/// <summary>
	/// Tree structure that is effective for searching information about lemmas of MrdDictionary.
	/// </summary>
	[Serializable]
	public class MrdTree
	{
		/// <summary>
		/// Root of tree.
		/// </summary>
		private Leaf Root;

		/// <summary>
		/// Reference to MrdDictionary for with this Tree was constructed.
		/// </summary>
		private MrdDictionary OriginalDictionary;

		/// <summary>
		/// Creates instance of MrdTree.
		/// </summary>
		/// <param name="mrdDictionary">An instance of mrdDictionary for that this Tree will be constructed.</param>
		/// <exception cref="System.NullReferenceException">Thrown then a mrdDictionary is null.</exception>
		public MrdTree(MrdDictionary mrdDictionary)
		{
			if (mrdDictionary == null)
				throw new NullReferenceException("mrdDictionary");

			Root = new Leaf();
			OriginalDictionary = mrdDictionary;
			PrefixCache = new Cache<int, string[]>(GetPrefixes);
		}

		/// <summary>
		/// Appends information about lemma to tree.
		/// </summary>
		/// <param name="lemma"></param>
		/// <param name="li"></param>
		public void AppendLemmaInfo(string lemma, LemmaInfo li)
		{
			if (lemma != string.Empty && lemma[0] == '-')
				return;
			foreach (string prefix in PrefixCache.GetItem(li.FlexiaModelNo))
				Root.ProcessString(string.Format("{0}{1}", prefix, lemma), 0, li);
		}

		/// <summary>
		/// Find all samples equal to word.
		/// </summary>
		/// <param name="word"></param>
		/// <returns>Array of FullLemmaInfo, with contain information about each found sample.</returns>
		public FullLemmaInfo[] Find(string word)
		{
			if (word == null)
				return null;

			SearchInfo si = new SearchInfo(OriginalDictionary, word.ToUpper());

			Root.FindInfo(si, word.ToUpper());

			return si.ToArray();
		}

		/// <summary>
		/// Function for obtaining array of prefixes of FlexiaModel specified by flexNo.
		/// </summary>
		/// <param name="flexNo"></param>
		/// <returns></returns>
		private string[] GetPrefixes(int flexNo)
		{
			return OriginalDictionary.FlexiaModels[flexNo].GetAllPrefixesIncludeEmpty();
		}

		/// <summary>
		/// Cache for obtaining array of prefixes of FlexiaModel specified by flexNo.
		/// </summary>
		private Cache<int, string[]> PrefixCache;

		/// <summary>
		/// One leaf of tree.
		/// </summary>
		[Serializable]
		private class Leaf
		{
			/// <summary>
			/// Sign of this Leaf.
			/// </summary>
			public char Sign;

			/// <summary>
			/// List of sub leaves.
			/// </summary>
			OnceLinkedList<Leaf> SubLeaves;

			/// <summary>
			/// Info of strings ended in this leaf.
			/// </summary>
			OnceLinkedList<LemmaInfo> Info;

			/// <summary>
			/// Creates new instance of class.
			/// </summary>
			/// <param name="sign"></param>
			public Leaf(char sign)
			{
				Sign = sign;
			}

			public Leaf()
			{
			}


			/// <summary>
			/// Adds information to current leaf.
			/// </summary>
			/// <param name="info"></param>
			private void AddInfo(LemmaInfo info)
			{
				OnceLinkedList<LemmaInfo>.Append(ref Info, info);
			}

			/// <summary>
			/// Returns sub Leaf with specified sign.
			/// </summary>
			/// <param name="sign"></param>
			/// <returns></returns>
			protected Leaf GetSubLeaf(char sign)
			{
				//if (SubLeaves != null)
				//{
				//    MyList<Leaf> temp = SubLeaves;
				//    while (temp != null)
				//    {
				//        if (temp.Item.Sign == sign)
				//            return temp.Item;
				//        temp = temp.Next;
				//    }
				//}
				//return null;
				if (SubLeaves != null)
				{
					foreach (Leaf leaf in SubLeaves)
						if (leaf.Sign == sign)
							return leaf;
				}
				return null;
			}

			/// <summary>
			/// Returns sub Leaf with specified sign.
			/// Create sub Leaf if leaf with such sign doesn't exist.
			/// </summary>
			/// <param name="sign"></param>
			/// <returns></returns>
			protected Leaf GetOrAppendSubLeaf(char sign)
			{
				if (SubLeaves != null)
				{
					foreach (Leaf leaf in SubLeaves)
						if (leaf.Sign == sign)
							return leaf;
					return SubLeaves.AppendToLast(new Leaf(sign)).Item;
				}
				else
				{
					SubLeaves = new OnceLinkedList<Leaf>(new Leaf(sign));
					return SubLeaves.Item;
				}
			}

			/// <summary>
			/// Adds information about specified string to Leaf.
			/// </summary>
			/// <param name="str"></param>
			/// <param name="index"></param>
			/// <param name="info"></param>
			public void ProcessString(string str, int index, LemmaInfo info)
			{
				if (index == str.Length)
					AddInfo(info);
				else
					GetOrAppendSubLeaf(str[index]).ProcessString(str, index + 1, info);
			}

			public override string ToString()
			{
				return Sign.ToString();
			}


			/// <summary>
			/// Adds AnCodes of all Flexia models satisfied to word.
			/// </summary>
			/// <param name="word"></param>
			/// <param name="result"></param>
			protected void FindFlex(SearchInfo searchInfo, string word)
			{
				if (Info != null)
				{
					foreach (LemmaInfo li in Info)
					{
						FlexiaModel fm = searchInfo.OriginalDictionary.FlexiaModels[li.FlexiaModelNo];
						if (fm.HasFlex(word))
						{
							searchInfo.Add(new FullLemmaInfo(
								li,
								searchInfo.FullWord.Substring(0, searchInfo.FullWord.Length - word.Length) + fm.FirstFlex,
								fm.FirstMF,
								fm.FindMorphologicFormByFlex(word)));
						}
					}
				}
			}

			/// <summary>
			/// Adds AnCodes of all Flexia models satisfied to word.
			/// Also adds all ancodes of sub nodes.
			/// </summary>
			/// <param name="word">Right part of word.</param>
			/// <param name="result">List to add satisfied AnCodes.</param>
			public void FindInfo(SearchInfo searchInfo, string word)
			{
				FindFlex(searchInfo, word);

				if (word == string.Empty)
					return;

				if (SubLeaves == null)
					return;

				Leaf node = GetSubLeaf(word[0]);

				if (node != null)
					node.FindInfo(searchInfo, word.Length > 1 ? word.Substring(1) : string.Empty);

				return;
			}
		}

		/// <summary>
		/// One leaf of tree. Dictionary used for sub leaves.
		/// </summary>
		[Serializable]
		private class Leaf2
		{
			/// <summary>
			/// List of sub leaves.
			/// </summary>
			Dictionary<char, Leaf2> SubLeaves;

			/// <summary>
			/// Info of strings ended in this leaf.
			/// </summary>
			OnceLinkedList<LemmaInfo> Info;

			public Leaf2()
			{
			}

			/// <summary>
			/// Adds information to current leaf.
			/// </summary>
			/// <param name="info"></param>
			private void AddInfo(LemmaInfo info)
			{
				OnceLinkedList<LemmaInfo>.Append(ref Info, info);
			}

			/// <summary>
			/// Returns sub Leaf with specified sign.
			/// </summary>
			/// <param name="sign"></param>
			/// <returns></returns>
			protected Leaf2 GetSubLeaf(char sign)
			{
				if (SubLeaves != null && SubLeaves.ContainsKey(sign))
				{
					return SubLeaves[sign];
				}
				return null;
			}

			/// <summary>
			/// Returns sub Leaf with specified sign.
			/// Create sub Leaf if leaf with such sign doesn't exist.
			/// </summary>
			/// <param name="sign"></param>
			/// <returns></returns>
			protected Leaf2 GetOrAppendSubLeaf(char sign)
			{
				if (SubLeaves != null)
				{
					if (SubLeaves.ContainsKey(sign))
						return SubLeaves[sign];
				}
				else
				{
					SubLeaves = new Dictionary<char, Leaf2>();
				}

				Leaf2 temp = new Leaf2();
				SubLeaves[sign] = temp;
				return temp;
			}

			/// <summary>
			/// Adds information about specified string to Leaf.
			/// </summary>
			/// <param name="str"></param>
			/// <param name="index"></param>
			/// <param name="info"></param>
			public void ProcessString(string str, int index, LemmaInfo info)
			{
				if (index == str.Length)
					AddInfo(info);
				else
					GetOrAppendSubLeaf(str[index]).ProcessString(str, index + 1, info);
			}

			public override string ToString()
			{
				return SubLeaves.ToString();
			}

			/// <summary>
			/// Adds AnCodes of all Flexia models satisfied to word.
			/// </summary>
			/// <param name="word"></param>
			/// <param name="result"></param>
			protected void FindFlex(SearchInfo searchInfo, string word)
			{
				if (Info != null)
				{
					foreach (LemmaInfo li in Info)
					{
						FlexiaModel fm = searchInfo.OriginalDictionary.FlexiaModels[li.FlexiaModelNo];
						if (fm.HasFlex(word))
						{
							searchInfo.Add(new FullLemmaInfo(
								li,
								searchInfo.FullWord.Substring(0, searchInfo.FullWord.Length - word.Length) + fm.FirstFlex,
								fm.FirstMF,
								fm.FindMorphologicFormByFlex(word)));
						}
					}
				}
			}

			/// <summary>
			/// Adds AnCodes of all Flexia models satisfied to word.
			/// Also adds all ancodes of sub nodes.
			/// </summary>
			/// <param name="word">Right part of word.</param>
			/// <param name="result">List to add satisfied AnCodes.</param>
			public void FindInfo(SearchInfo searchInfo, string word)
			{
				FindFlex(searchInfo, word);

				if (word == string.Empty)
					return;

				if (SubLeaves == null)
					return;

				Leaf2 node = GetSubLeaf(word[0]);

				if (node != null)
					node.FindInfo(searchInfo, word.Length > 1 ? word.Substring(1) : string.Empty);

				return;
			}
		}

		/// <summary>
		/// Information about sought word.
		/// </summary>
		private class SearchInfo
		{
			public List<FullLemmaInfo> Result;
			public string FullWord;
			public MrdDictionary OriginalDictionary;

			public SearchInfo(MrdDictionary mrdDictionary, string fullWord)
			{
				Result = new List<FullLemmaInfo>();
				FullWord = fullWord;
				OriginalDictionary = mrdDictionary;
			}

			public void Add(FullLemmaInfo fullLemmaInfo)
			{
				Result.Add(fullLemmaInfo);
			}

			public FullLemmaInfo[] ToArray()
			{
				return Result.ToArray();
			}
		}
	}
}
