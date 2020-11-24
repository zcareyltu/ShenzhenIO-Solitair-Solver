using CustomLinkedList;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DotNetPriorityQueue {
    public class PriorityQueue<T> {

        //object array
        List<T> queue = new List<T>();
        int heapSize = -1;
        IComparer<T> comparer;
        bool _isMinPriorityQueue;
        public int Count { get { return queue.Count; } }

        
        public PriorityQueue(bool IsMinimumQueue, IComparer<T> comparer) {
            this._isMinPriorityQueue = IsMinimumQueue;
            this.comparer = comparer;
        }

        public PriorityQueue(bool IsMinimumQueue, Func<T, T, int> comparer) {
            this._isMinPriorityQueue = IsMinimumQueue;
            this.comparer = new BasicComparer<T>(comparer);
		}


        /// <summary>
        /// Enqueue the object with priority
        /// </summary>
        /// <param name="priority"></param>
        /// <param name="obj"></param>
        public void Enqueue(T obj) {
            queue.Add(obj);
            heapSize++;
            //Maintaining heap
            if (_isMinPriorityQueue)
                BuildHeapMin(heapSize);
            else
                BuildHeapMax(heapSize);
        }
        /// <summary>
        /// Dequeue the object
        /// </summary>
        /// <returns></returns>
        public T Dequeue() {
            if (heapSize > -1) {
                T returnVal = queue[0];
                queue[0] = queue[heapSize];
                queue.RemoveAt(heapSize);
                heapSize--;
                //Maintaining lowest or highest at root based on min or max queue
                if (_isMinPriorityQueue)
                    MinHeapify(0);
                else
                    MaxHeapify(0);
                return returnVal;
            } else
                throw new Exception("Queue is empty");
        }
        //public void UpdatePriority(T obj, int priority) { ...}
        //public bool IsInQueue(T obj) { ...}

        /// <summary>
        /// Maintain max heap
        /// </summary>
        /// <param name="i"></param>
        private void BuildHeapMax(int i) {
            while (i >= 0 && this.comparer.Compare(queue[(i - 1) / 2], queue[i]) < 0) {
                Swap(i, (i - 1) / 2);
                i = (i - 1) / 2;
            }
        }
        /// <summary>
        /// Maintain min heap
        /// </summary>
        /// <param name="i"></param>
        private void BuildHeapMin(int i) {
            while (i >= 0 && this.comparer.Compare(queue[(i - 1) / 2], queue[i]) > 0) {
                Swap(i, (i - 1) / 2);
                i = (i - 1) / 2;
            }
        }
        private void MaxHeapify(int i) {
            int left = ChildL(i);
            int right = ChildR(i);

            int heighst = i;
            if (left <= heapSize && this.comparer.Compare(queue[heighst], queue[left]) < 0)
                heighst = left;
            if (right <= heapSize && this.comparer.Compare(queue[heighst], queue[right]) < 0)
                heighst = right;

            if (heighst != i) {
                Swap(heighst, i);
                MaxHeapify(heighst);
            }
        }
        private void MinHeapify(int i) {
            int left = ChildL(i);
            int right = ChildR(i);

            int lowest = i;

            if (left <= heapSize && this.comparer.Compare(queue[lowest], queue[left]) > 0)
                lowest = left;
            if (right <= heapSize && this.comparer.Compare(queue[lowest], queue[right]) > 0)
                lowest = right;

            if (lowest != i) {
                Swap(lowest, i);
                MinHeapify(lowest);
            }
        }

        private void Swap(int i, int j) {
            var temp = queue[i];
            queue[i] = queue[j];
            queue[j] = temp;
        }
        private int ChildL(int i) {
            return i * 2 + 1;
        }
        private int ChildR(int i) {
            return i * 2 + 2;
        }
    }
    public class BasicComparer<J> : IComparer<J> {

		private Func<J, J, int> compareFunc;

		public BasicComparer(Func<J, J, int> compare) {
			this.compareFunc = compare;
		}

		public int Compare([AllowNull] J x, [AllowNull] J y) {
			return compareFunc(x, y);
		}


	}

}
