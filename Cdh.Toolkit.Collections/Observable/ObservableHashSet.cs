﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Cdh.Toolkit.Extensions.ReaderWriterLockSlim;

namespace Cdh.Toolkit.Collections.Observable
{
    public class ObservableHashSet<T> : ObservableCollection<T>, IObservableCollection<T>
    {
        protected new HashSet<T> Decorated { get; private set; }

        public ObservableHashSet(HashSet<T> hashSet, EnumerateBehavior behavior, ReaderWriterLockSlim @lock)
            : base(hashSet, behavior, @lock)
        {
            Decorated = hashSet;
        }

        public ObservableHashSet(HashSet<T> hashSet, EnumerateBehavior behavior)
            : base(hashSet, behavior)
        {
            Decorated = hashSet;
        }

        public ObservableHashSet(EnumerateBehavior behavior, ReaderWriterLockSlim @lock)
            : base(new HashSet<T>(), behavior, @lock)
        {
            Decorated = (HashSet<T>)base.Decorated;
        }

        public ObservableHashSet(EnumerateBehavior behavior)
            : base(new HashSet<T>(), behavior)
        {
            Decorated = (HashSet<T>)base.Decorated;
        }

        public override bool Add(T item)
        {
            using (Lock.Write())
            {
                bool success;

                if (success = Decorated.Add(item))
                    FireAdded(item);

                return success;
            }
        }

        private T[] CloneAsArray()
        {
            T[] clone = new T[Decorated.Count];
            Decorated.CopyTo(clone);

            return clone;
        }

        #region HashSet<T> decorations

        public virtual void CopyTo(T[] array)
        {
            using (Lock.Read())
                Decorated.CopyTo(array);
        }

        public virtual void CopyTo(T[] array, int arrayIndex, int count)
        {
            using (Lock.Read())
                Decorated.CopyTo(array, arrayIndex, count);
        }

        public virtual void ExceptWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            using (Lock.Write())
                foreach (T item in other)
                    if (Decorated.Remove(item))
                        FireRemoved(item);
        }

        public virtual void IntersectWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            using (Lock.Write())
            {
                T[] clone = CloneAsArray();

                foreach (T item in clone)
                    if (!other.Contains(item))
                    {
                        Decorated.Remove(item);
                        FireRemoved(item);
                    }

                foreach (T item in other)
                    if (!Decorated.Contains(item))
                    {
                        Decorated.Remove(item);
                        FireRemoved(item);
                    }
            }
        }

        public virtual bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            using (Lock.Read())
                return Decorated.IsProperSubsetOf(other);
        }

        public virtual bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            using (Lock.Read())
                return Decorated.IsProperSupersetOf(other);
        }

        public virtual bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            using (Lock.Read())
                return Decorated.IsSubsetOf(other);
        }

        public virtual bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            using (Lock.Read())
                return Decorated.IsSupersetOf(other);
        }

        public virtual bool Overlaps(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            using (Lock.Read())
                return Decorated.Overlaps(other);
        }

        public virtual int RemoveWhere(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match");

            int count = 0;

            using (Lock.Write())
            {
                T[] clone = CloneAsArray();

                foreach (T item in clone)
                {
                    if (match(item))
                    {
                        Decorated.Remove(item);
                        FireRemoved(item);

                        count++;
                    }
                }
            }

            return count;
        }

        public virtual bool SetEquals(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            using (Lock.Read())
                return Decorated.SetEquals(other);
        }

        private IEnumerable<T> MakeDistinct(IEnumerable<T> other)
        {
            HashSet<T> otherSet = other as HashSet<T>;
            if (otherSet != null && otherSet.Comparer.Equals(Decorated.Comparer))
                return other;

            return other.Distinct(Decorated.Comparer);
        }

        public virtual void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            using (Lock.Write())
            {
                foreach (T item in MakeDistinct(other))
                {
                    if (Decorated.Contains(item))
                    {
                        Decorated.Remove(item);
                        FireRemoved(item);
                    }
                    else
                    {
                        Decorated.Add(item);
                        FireAdded(item);
                    }
                }
            }
        }

        public virtual void TrimExcess()
        {
            using (Lock.Write())
                Decorated.TrimExcess();
        }

        public virtual void UnionWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            using (Lock.Write())
            {
                foreach (T item in other)
                    if (Decorated.Add(item))
                        FireAdded(item);
            }
        }

        #endregion
    }
}
