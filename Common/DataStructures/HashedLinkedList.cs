using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.Common.DataStructures
{
    public class HashedLinkedList<T> : ICollection<T>, IReadOnlyCollection<T>, ICollection
    {
        private readonly LinkedList<T> _data = new LinkedList<T>();
        private readonly Dictionary<T, LinkedListNode<T>> _hashLookup = new Dictionary<T, LinkedListNode<T>>();

        public LinkedListNode<T> First => _data.First;
        public LinkedListNode<T> Last => _data.Last;

        /// <summary>
        /// Initializes a new instance of the HashedLinkedList<T> class that is empty.
        /// </summary>
        public HashedLinkedList() {}

        /// <summary>
        /// Initializes a new instance of the HashedLinkedList<T> class that contains elements copied from the specified IEnumerable
        /// and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="data"></param>
        public HashedLinkedList(IEnumerable<T> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            foreach (var item in data)
            {
                var node = _data.AddLast(item);
                _hashLookup.Add(item, node);
            }
        }
        
        public void Clear()
        {
            _data.Clear();
            _hashLookup.Clear();
        }
        public bool Contains(T item) => _hashLookup.ContainsKey(item);
        public int Count => _data.Count;

        #region Add Methods

        /// <summary>
        /// Adds a new node containing the specified value after the specified existing node in the HashedLinkedList<T>.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns>The new LinkedListNode&lt;T&gt; containing value</returns>
        public LinkedListNode<T> AddAfter(LinkedListNode<T> node,
                             T value)
        {
            return InternalAdd(node, value, _data.AddAfter);
        }

        /// <summary>
        /// Adds a new node containing the specified value before the specified existing node in the HashedLinkedList<T>.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns>The new LinkedListNode&lt;T&gt; containing value</returns>
        public LinkedListNode<T> AddBefore(LinkedListNode<T> node,
                                           T value)
        {
            return InternalAdd(node, value, _data.AddBefore);
        }

        /// <summary>
        /// Adds a new node containing the specified value to the start of the HashedLinkedList<T>.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns>The new LinkedListNode&lt;T&gt; containing value</returns>
        public LinkedListNode<T> AddFirst(T value)
        {
            return InternalAdd(value, _data.AddFirst);
        }

        /// <summary>
        /// Adds a new node containing the specified value to the end of the HashedLinkedList<T>.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns>The new LinkedListNode&lt;T&gt; containing value</returns>
        public LinkedListNode<T> AddLast(T value)
        {
            return InternalAdd(value, _data.AddLast);
        }

        private LinkedListNode<T> InternalAdd(LinkedListNode<T> node, T value,
                                              Func<LinkedListNode<T>, T, LinkedListNode<T>> function)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (function == null) throw new ArgumentNullException(nameof(function));
            if (_hashLookup.ContainsKey(value)) throw new ArgumentException("Value already exists in collection", nameof(value));

            var newNode = function(node, value);
            _hashLookup.Add(value, newNode);
            return newNode;
        }

        private LinkedListNode<T> InternalAdd(T value,
                                              Func<T, LinkedListNode<T>> function)
        {
            if (function == null) throw new ArgumentNullException(nameof(function));
            if (_hashLookup.ContainsKey(value)) throw new ArgumentException("Value already exists in collection", nameof(value));

            var newNode = function(value);
            _hashLookup.Add(value, newNode);
            return newNode;
        }
        #endregion

        #region Remove

        public bool Remove(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            // Normally removing an item involves iterating through the linkedList until we find the node.
            // Thanks to the Dictionary we can just skip right to it...
            var node = _hashLookup[item];
            if (node == null) return false;  // item not found
            _hashLookup.Remove(item);
            _data.Remove(node);
            return true;
        }

        public void Remove(LinkedListNode<T> item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _hashLookup.Remove(item.Value);
            _data.Remove(item);
        }

        /// <summary>
        /// Removes the node at the start of the HashedLinkedList<T>
        /// </summary>
        public void RemoveFirst()
        {
            var node = _data.First;
            if (node == null) return;
            _hashLookup.Remove(node.Value);
            _data.RemoveFirst();
        }
        
        /// <summary>
        /// Removes the node at the end of the HashedLinkedList<T>
        /// </summary>
        public void RemoveLast()
        {
            var node = _data.Last;
            if (node == null) return;
            _hashLookup.Remove(node.Value);
            _data.RemoveLast();
        }
        #endregion

        #region IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        
        public ICollection<T> Keys => _hashLookup.Keys;

        public LinkedListNode<T> this[T key] => _hashLookup[key];

        public void CopyTo(T[] array, int arrayIndex)
        {
            _data.CopyTo(array, arrayIndex);
        }

        void ICollection<T>.Add(T item)
        {
            AddLast(item);
        }

        void System.Collections.ICollection.CopyTo(Array array, int index) => throw new NotImplementedException();
        object System.Collections.ICollection.SyncRoot => throw new NotImplementedException();
        bool System.Collections.ICollection.IsSynchronized => false;
        bool ICollection<T>.IsReadOnly => false;
    }
}
