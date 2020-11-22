using System;
using System.Collections.Generic;
using System.Text;

namespace ShenzhenIO_Solitair_Solver {
	public static class ListUtils {

		public static T last<T>(this List<T> list) {
			return list[list.Count - 1];
		}

		public static T pop<T>(this List<T> list) {
			T popped = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
			return popped;
		}

	}
}
