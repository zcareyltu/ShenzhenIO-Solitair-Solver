using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CustomLinkedList {
	internal struct LinkedListEnumerator<T> : IEnumerator<T> {

		private CustomLinkedList<T> list;
		private Node<T> node;
		private T current;

		public T Current => current;

		object IEnumerator.Current => Current;

		internal LinkedListEnumerator(CustomLinkedList<T> list) {
			this.list = list;
			this.node = list.First;
			this.current = default(T);
		}

		public void Dispose() {
		}

		public bool MoveNext() {
			if(node != null) {
				current = node.Value;
				node = node.Next;
				return true;
			} else {
				return false;
			}
		}

		public void Reset() {
			current = default(T);
			node = list.First;
		}
	}
}
