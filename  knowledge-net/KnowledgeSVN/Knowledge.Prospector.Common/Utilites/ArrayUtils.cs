using System;

namespace Knowledge.Prospector.Common.Utilites
{
	public static class ArrayUtils
	{
		//public int Comparer
		public static T[] QuickSort<T>(T[] items, Comparison<T> comparer)
		{
			SubQuickSort(items, 0, items.Length - 1, comparer);
			return items;
		}

		private static void SubQuickSort<T>(T[] items, int leftIndex, int rightIndex, Comparison<T> comparer)
		{
			T pivot = items[leftIndex];
			int left = leftIndex;
			int right = rightIndex;

			while (left < right)
			{
				while (left < right && (comparer(pivot, items[right])) <= 0)
					right--;

				if (left != right)
				{
					items[left] = items[right];
					left++;
				}

				while (left < right && (comparer(items[left], pivot)) <= 0)
					left++;

				if (left != right)
				{
					items[right] = items[left];
					right--;
				}

			}

			items[left] = pivot;

			if ((rightIndex - left) > 1)
				SubQuickSort(items, left + 1, rightIndex, comparer);
			if ((right - leftIndex) > 1)
				SubQuickSort(items, leftIndex, right - 1, comparer);
		}

	}
}
