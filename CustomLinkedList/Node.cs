
namespace CustomLinkedList {
	public class Node<T> {

		public readonly T Value;
		public Node<T> Previous { get; internal set; }
		public Node<T> Next { get; internal set; }
		public CustomLinkedList<T> List { get; internal set; }

		public Node(T value) {
			this.List = null;
			this.Value = value;
		}

		internal Node(CustomLinkedList<T> list, T value) {
			this.List = list;
			this.Value = value;
		}

	}
}
