using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http.Headers;

namespace CustomLinkedList {
	public class CustomLinkedList<T> : ICollection<T>, /*System.Collections.ICollection,*/ IReadOnlyCollection<T> {

		public int Count { get; private set; }
		public Node<T> First { get; private set; } = null;
		public Node<T> Last { get; private set; } = null;

		public bool IsReadOnly => false;

		public CustomLinkedList() {

		}

		private CustomLinkedList(Node<T> node) {
			if (node == null) throw new ArgumentNullException("node");
			First = node;
			node.List = this;
			Count = 1;
			while(node.Next != null) {
				node = node.Next;
				node.List = this;
				Count++;
			}
			Last = node;
		}

		public CustomLinkedList(IEnumerable<T> items) {
			foreach(T item in items) {
				Add(item);
			}
		}

		public void Add(T value) {
			AddLast(value);
		}

		public void Add(CustomLinkedList<T> items) {
			 foreach(T item in items) {
				Add(item);
			}
		}

		public Node<T> AddAfter(Node<T> node, T value) {
			ValidateNode(node);
			Node<T> result = new Node<T>(this, value);
			InternalInsertNodeAfter(node, result);
			return result;
		}

		public void AddAfter(Node<T> node, Node<T> newNode) {
			ValidateNode(node);
			ValidateNewNode(newNode);
			InternalInsertNodeAfter(node, newNode);
		}

		public Node<T> AddBefore(Node<T> node, T value) {
			ValidateNode(node);
			Node<T> result = new Node<T>(this, value);
			InternalInsertNodeBefore(node, result);
			if(node == First) {
				First = result;
			}
			return result;
		}

		public void AddBefore(Node<T> node, Node<T> newNode) {
			ValidateNode(node);
			ValidateNewNode(newNode);
			InternalInsertNodeBefore(node, newNode);
			if(node == First) {
				First = newNode;
			}
		}

		public Node<T> AddFirst(T value) {
			Node<T> result = new Node<T>(this, value);
			if(First == null) {
				InternalInsertNodeToEmptyList(result);
			} else {
				InternalInsertNodeBefore(First, result);
				First = result;
			}
			return result;
		}

		public void AddFirst(Node<T> node) {
			ValidateNewNode(node);
			if(First == null) {
				InternalInsertNodeToEmptyList(node);
			} else {
				InternalInsertNodeBefore(First, node);
				First = node;
			}
		}

		public Node<T> AddLast(T value) {
			Node<T> result = new Node<T>(this, value);
			if(First == null) {
				InternalInsertNodeToEmptyList(result);
			} else {
				InternalInsertNodeAfter(Last, result);
				Last = result;
			}
			return result;
		}

		public void AddLast(Node<T> node) {
			ValidateNewNode(node);
			if(First == null) {
				InternalInsertNodeToEmptyList(node);
			} else {
				InternalInsertNodeAfter(Last, node);
				Last = node;
			}
		}

		public void Clear() {
			Node<T> node = First;
			while(node != null) {
				node.List = null;
				node = node.Next;
			}
			First = null;
			Last = null;
			Count = 0;
		}

		public bool Contains(T value) {
			return Find(value) != null;
		}

		public Node<T> Find(T value) {
			Node<T> node = First;
			EqualityComparer<T> c = EqualityComparer<T>.Default;
			while(node != null) {
				if(c.Equals(node.Value, value)) {
					return node;
				}
				node = node.Next;
			}
			return null;
		}

		public Node<T> FindLast(T value) {
			Node<T> node = Last;
			EqualityComparer<T> c = EqualityComparer<T>.Default;
			while (node != null) {
				if (c.Equals(node.Value, value)) {
					return node;
				}
				node = node.Previous;
			}
			return null;
		}

		public bool Remove(T value) {
			Node<T> node = Find(value);
			if(node != null) {
				InternalRemoveNode(node);
				return true;
			}
			return false;
		}

		public void Remove(Node<T> node) {
			ValidateNode(node);
			InternalRemoveNode(node);
		}

		public void RemoveFirst() {
			if (First == null) throw new InvalidOperationException("List is empty.");
			InternalRemoveNode(First);
		}

		public void RemoveLast() {
			if (Last == null) throw new InvalidOperationException("List is empty.");
			InternalRemoveNode(Last);
		}

		public Node<T> NodeAt(int index) {
			if (index >= Count) throw new IndexOutOfRangeException();
			Node<T> node = First;
			while (index-- > 0) {
				node = node.Next;
			}
			return node;
		}

		/// <summary>
		/// Removes the node and all nodes after, creating a new linked list
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public CustomLinkedList<T> RemoveRange(Node<T> node) {
			ValidateNode(node);
			CustomLinkedList<T> result = new CustomLinkedList<T>(node);
			if(node == First) {
				First = null;
			} else {
				node.Previous.Next = null;
			}
			Last = node.Previous;
			node.Previous = null;
			Count -= result.Count;
			return result;
		}

		public void CopyTo(T[] array, int index) {
			if (array == null) throw new ArgumentNullException("array");
			if (index < 0 || index > array.Length) throw new ArgumentOutOfRangeException("index");
			if (array.Length - index < Count) throw new ArgumentException("Insufficient Space");

			Node<T> node = First;
			while(node != null) { 
				array[index++] = node.Value;
				node = node.Next;
			}
		}

		public IEnumerator<T> GetEnumerator() {
			return new LinkedListEnumerator<T>(this);
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		private void ValidateNode(Node<T> node) {
			if(node == null) {
				throw new ArgumentNullException("node");
			}

			if(node.List != this) {
				throw new InvalidOperationException("node list does not match");
			}
		}

		private void ValidateNewNode(Node<T> node) {
			if(node == null) {
				throw new ArgumentNullException("node");
			}
			//We are going to make some assumptions, so uncomment this
			//while(node != null) {
				if(node.List != null) {
					throw new InvalidOperationException("node belongs to another list"/*"At least one node belongs to antoher list"*/);
				}
			//node = node.Next;
			//}

			//Be sure no extra nodes are attached
			node.Previous = null;
			node.Next = null;
		}

		private Node<T> FindEndNode(Node<T> newNode, out int nodeLength) {
			nodeLength = 1;
			newNode.List = this;
			Node<T> endNode = newNode;
			while (endNode.Next != null) {
				endNode = endNode.Next;
				endNode.List = this;
				nodeLength++;
			}
			return endNode;
		}

		private void InternalInsertNodeBefore(Node<T> node, Node<T> newNode) {
			//int len;
			//Node<T> endNode = FindEndNode(newNode, out len);
			newNode.List = this;
			newNode.Next = node;
			newNode.Previous = node.Previous;
			if(node.Previous != null) node.Previous.Next = newNode;
			node.Previous = newNode;
			Count++;
		}

		private void InternalInsertNodeAfter(Node<T> node, Node<T> newNode) {
			//int len;
			//Node<T> endNode = FindEndNode(newNode, out len);
			newNode.Previous = node;
			newNode.Next = node.Next;
			if(node.Next != null) node.Next.Previous = newNode;
			node.Next = newNode;
			Count++;
		}

		private void InternalInsertNodeToEmptyList(Node<T> newNode) {
			//int len;
			//Node<T> endNode = FindEndNode(newNode, out len);
			newNode.Previous = null;
			newNode.Next = null;
			First = newNode;
			Last = newNode;
			Count = 1;
		}

		private void InternalRemoveNode(Node<T> node) {
			if(node == First) {
				First = node.Next;
			} else {
				node.Previous.Next = node.Next;
			}
			if(node == Last) {
				Last = node.Previous;
			} else {
				node.Next.Previous = node.Previous;
			}

			node.Previous = null;
			node.Next = null;
			node.List = null;

			Count--;
		}
	}

	
}
