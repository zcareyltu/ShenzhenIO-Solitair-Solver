using CustomLinkedList;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DotNetPriorityQueue {
	public class PriorityQueue<T> {

		private CustomLinkedList<T> heap = new CustomLinkedList<T>();
		private IComparer<T> comparer;

		public int Count { get => heap.Count; }
		public bool IsEmpty { get => heap.Count == 0; }

		public PriorityQueue(IComparer<T> comparator) {
			this.comparer = comparator;
		}

		public PriorityQueue(Func<T, T, int> compareFunc) {
			this.comparer = new BasicComparator<T>(compareFunc);
		}

		public T Peek() {
			return heap.First.Value;
		}

		public void Push(params T[] values) {
			foreach(T value in values) {
				push(value);
			}
		}

		private void push(T value) {
			Node<T> node = heap.First;
			while(node != null) {
				if (comparer.Compare(value, node.Value) > 0) {
					heap.AddBefore(node, value);
					return;
				}
			}
			heap.Add(value);
		}

		public T Pop() {
			T value = heap.First.Value;
			heap.RemoveFirst();
			return value;
		}

		public T replace(T newValue) {
			T replacedValue = Pop();
			push(newValue);
			return replacedValue;
		}

		private class BasicComparator<J> : IComparer<J> {

			private Func<J, J, int> compareFunc;

			public BasicComparator(Func<J, J, int> compare) {
				this.compareFunc = compare;
			}

			public int Compare([AllowNull] J x, [AllowNull] J y) {
				return compareFunc(x, y);
			}

			
		}
	}
}
