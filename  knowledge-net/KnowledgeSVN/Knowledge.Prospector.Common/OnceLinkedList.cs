using System;
using System.Collections;
using System.Collections.Generic;

namespace Knowledge.Prospector.Common
{
	/// <summary>
	/// Simple once linked list.
	/// </summary>
	/// <typeparam name="T">Type of items in list.</typeparam>
	[Serializable]
	public class OnceLinkedList<T> : IEnumerable<T> where T : new()
	{
		/// <summary>
		/// Item of current node.
		/// </summary>
		public T Item;

		/// <summary>
		/// Next node.
		/// </summary>
		public OnceLinkedList<T> Next;

		public OnceLinkedList(T item)
		{
			Item = item;
		}

		/// <summary>
		/// Replace Next node with new node.
		/// </summary>
		/// <param name="item">Item to add to new node.</param>
		/// <returns></returns>
		private OnceLinkedList<T> Append(T item)
		{
			Next = new OnceLinkedList<T>(item);
			return Next;
		}

		/// <summary>
		/// Append new Item to end of list.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public OnceLinkedList<T> AppendToLast(T item)
		{
			if (Next != null)
				return Next.AppendToLast(item);
			else
				return Append(item);
		}

		/// <summary>
		/// Returns list containing all previous nodes and new node.
		/// </summary>
		/// <param name="list">List containing items that will be in new list.</param>
		/// <param name="item">Item od new node.</param>
		/// <returns></returns>
		public static OnceLinkedList<T> Append(ref OnceLinkedList<T> list, T item)
		{
			if (list != null)
				return list.AppendToLast(item);
			else
			{
				list = new OnceLinkedList<T>(item);
				return list;
			}
		}

		//public T Find(T item)
		//{
		//    MyList<T> temp = this;

		//    while (temp != null)
		//    {
		//        if (temp.Item != item)
		//            temp = temp.Next;
		//        else
		//            return temp.Item;
		//    }

		//    return null;
		//}

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator()
		{
			OnceLinkedList<T> temp = this;

			while (temp != null)
			{
				yield return temp.Item;
				temp = temp.Next;
			}
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{

			OnceLinkedList<T> temp = this;

			while (temp != null)
			{
				yield return temp.Item;
				temp = temp.Next;
			}
		}

		#endregion

		public override string ToString()
		{
			string temp = Item.ToString() + " | ";

			if (Next != null)
				return temp + Next.ToString();
			else
				return temp;
		}
	}
}
