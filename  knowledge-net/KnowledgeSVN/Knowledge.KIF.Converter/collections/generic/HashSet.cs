/*
 * Created by: M. Sigalin
 * Created: Tuesday, November 07, 2006
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace Knowledge.KIF.Converter.Collections.Generic {
    public class HashSet<T>: ISet<T> {
        
        private static readonly object OBJECT = new object();
        
        private Dictionary<T, object> _hashtable;
        
        public HashSet() {
            _hashtable = new Dictionary<T, object>();
        }

        public HashSet(ISet<T> set): this(set, null) {
        }

        public HashSet(int capacity, IEqualityComparer<T> comparer) {
            _hashtable = new Dictionary<T, object>(capacity, comparer);
        }

        public HashSet(IEqualityComparer<T> comparer) {
            _hashtable = new Dictionary<T, object>(comparer);
        }
        
        public HashSet(int capacity): this(capacity, null) {
        }

        public HashSet(ISet<T> set, IEqualityComparer<T> comparer) {
            if (set == null)
                throw new ArgumentNullException("Source set cann't be null");

            _hashtable = new Dictionary<T, object>(set.Count, comparer);
            foreach (T t in set) {
                Add(t);
            }
        }
        
        public void Add(T item) {
            _hashtable.Add(item, OBJECT);
        }

        public void Clear() {
            _hashtable.Clear();
        }

        public bool Contains(T item) {
            return _hashtable.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex) {
            _hashtable.Keys.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item) {
            return _hashtable.Remove(item);
        }

        public int Count {
            get { return _hashtable.Count; }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return _hashtable.Keys.GetEnumerator();
        }

        public IEnumerator GetEnumerator() {
            return _hashtable.Keys.GetEnumerator();
        }
    }
}