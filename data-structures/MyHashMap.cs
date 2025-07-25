using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_structures
{
    public class HashMap<TKey, TValue>
    {
        int _count;
        int[] _buckets;
        Entry[] _entries;
        IEqualityComparer<TKey> _comparer;
        int _freeSpaceIndex;
        int _freeSpaceCount;
        struct Entry
        {
            public int hashCode;
            public int next;
            public TValue value;
            public TKey key;
        }
        public HashMap(int capacity) : this(capacity,null) { }
        public HashMap(int capacity, EqualityComparer<TKey> comparer)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException();
            if (capacity > 0)
            {
                int size = ClosestPrime(capacity);
                _buckets = new int[size];
                _entries = new Entry[size];
                for (int i = 0; i < size; i++)
                {
                    _buckets[i] = -1;
                }
            }

            this._comparer = comparer ?? EqualityComparer<TKey>.Default;

        }
        public int Count { get { return _count = _freeSpaceCount; } }
        public TValue this[TKey key]
        {
            get
            {
                int entry = FindEntry(key);
                if (entry >= 0) return _entries[entry].value;
                throw new KeyNotFoundException();
            }
            set
            {
                Add(key,value,true);
            }
        }
        public void Add(TKey key,TValue value)
        {
            Add(key,value,false);
        }
        private void Add(TKey key, TValue value, bool canMutate)
        {
            int hashValue = _comparer.GetHashCode(key) & 0x7FFFFFFF;
            int targetBucket = hashValue % _buckets.Length;
            if (_count == _entries.Length) return; //full and this implementation does not support resizing

            for (int i = _buckets[targetBucket]; i >= 0; i = _entries[i].next)
            {
                if (_entries[i].hashCode == hashValue && _comparer.Equals(_entries[i].key, key))
                {
                    if (canMutate)
                    {
                        _entries[i].value = value;
                        return;
                    }
                }
            }

            int index = 0;
            if (_freeSpaceCount > 0)
            {
                index = _freeSpaceIndex;
                _freeSpaceIndex = _entries[index].next;
                _freeSpaceCount--;
            } else
            {
                index = _count;
                _count++;
            }
            
            _entries[index].hashCode = hashValue;
            _entries[index].key = key;
            _entries[index].value = value;
            _entries[index].next = _buckets[targetBucket];
            _buckets[targetBucket] = index;
        }
        public bool Remove(TKey key)
        {
            if (key == null) throw new ArgumentNullException();

            if (_buckets != null)
            {
                int hashValue = _comparer.GetHashCode(key) & 0x7FFFFFFF;
                int targetBucket = hashValue % _buckets.Length;
                int lastEntryIndex = -1;

                for (int entryIndex = _buckets[targetBucket]; entryIndex >= 0; lastEntryIndex = entryIndex, entryIndex = _entries[entryIndex].next)
                {
                    if (_entries[entryIndex].hashCode == hashValue && _comparer.Equals(_entries[entryIndex].key,key))
                    {
                        if (lastEntryIndex < 0)
                        {
                            _buckets[targetBucket] = _entries[entryIndex].next;
                        } else
                        {
                            _entries[lastEntryIndex].next = _entries[entryIndex].next;
                        }

                        _entries[entryIndex].hashCode = -1;
                        _entries[entryIndex].next = _freeSpaceIndex;
                        _entries[entryIndex].key = default(TKey);
                        _entries[entryIndex].value = default(TValue);
                        _freeSpaceIndex = entryIndex;
                        _freeSpaceCount++;
                        return true;
                    }
                }
            }
            return false;
        }
        internal int FindEntry(TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            if (_buckets != null)
            {
                int hashCode = _comparer.GetHashCode(key) & 0x7FFFFFFF;
                int targetBucket = hashCode % _buckets.Length;
                for (int i = _buckets[targetBucket]; i >=0; i = _entries[i].next)
                {
                    if (_entries[i].hashCode == hashCode && _comparer.Equals(_entries[i].key,key))
                    {
                        return i;
                    }
                }
            }
            return - 1;
        }
        internal int ClosestPrime(int min)
        {
            if (min < 0) throw new ArgumentException();
            for (int i = (1 | min); i < int.MaxValue; i += 2)
            {
                if (IsPrime(i))
                {
                    return i;
                }
            }
            return min;
        }
        internal bool IsPrime(int value)
        {
            if ((value & 1) != 0)
            {
                int limit = (int)Math.Sqrt(value);
                for (int d = 3; d < limit; d += 2)
                {
                    if ((value % d) == 0) return false;
                }
                return true;
            }
            return value == 2;
        }
    }
}
