using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Clever.Collections
{
    /// <summary>
    /// A list optimized for a small number of items.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    internal struct SmallList<T>
    {
        /// <summary>
        /// The size of this list's buffer after the first item is added.
        /// </summary>
        private const int InitialCapacity = 4;

        /// <summary>
        /// The buffer where this list's items are stored.
        /// </summary>
        private T[] _array;

        /// <summary>
        /// The number of items in this list.
        /// </summary>
        private int _count;

        /// <summary>
        /// Gets the number of items this list can hold before resizing.
        /// </summary>
        public int Capacity => _array?.Length ?? 0;

        /// <summary>
        /// Gets the number of items in this list.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Gets a value indicating whether this list is full.
        /// </summary>
        private bool IsFull => _count == Capacity;

        /// <summary>
        /// Gets or sets an item in this list.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The item at the specified index.</returns>
        public T this[int index]
        {
            get
            {
                Debug.Assert(index >= 0 && index < _count);
                return _array[index];
            }
            set
            {
                Debug.Assert(index >= 0 && index < _count);
                _array[index] = value;
            }
        }

        /// <summary>
        /// Adds an item to this list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            if (IsFull)
            {
                MakeRoom();
            }

            _array[_count++] = item;
        }

        /// <summary>
        /// Resizes this list when it is full.
        /// </summary>
        private void MakeRoom()
        {
            Debug.Assert(IsFull);

            T[] newArray;
            if (_count == 0)
            {
                newArray = new T[InitialCapacity];
            }
            else
            {
                newArray = new T[_count * 2];
                Array.Copy(_array, 0, newArray, 0, _count);
            }
            
            _array = newArray;
        }
    }
}
