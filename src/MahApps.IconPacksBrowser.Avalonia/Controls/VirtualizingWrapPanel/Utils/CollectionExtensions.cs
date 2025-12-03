using System;
using System.Collections;
using System.Collections.Generic;

namespace MahApps.IconPacksBrowser.Avalonia.Controls.Utils;

internal static class CollectionExtensions
{
    internal static void InsertMany<T>(this List<T> list, int index, T item, int count)
    {
        var repeat = FastRepeat<T>.Instance;
        repeat.Count = count;
        repeat.Item = item;
        list.InsertRange(index, FastRepeat<T>.Instance);
        repeat.Item = default;
    }

    private class FastRepeat<T> : ICollection<T>
    {
        public static readonly FastRepeat<T> Instance = new();
        public int Count { get; set; }
        public bool IsReadOnly => true;
        public T? Item { get; set; }
        public void Add(T item) => throw new NotSupportedException();
        public void Clear() => throw new NotSupportedException();
        public bool Contains(T item) => throw new NotSupportedException();
        public bool Remove(T item) => throw new NotSupportedException();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator()
        {
            if (Item is null)
                throw new InvalidOperationException("Item was null.");

            var item = Item;
            var count = Count;

            for (var i = 0; i < count; i++)
            {
                yield return item;
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (Item is null) 
                throw new InvalidOperationException("Item was null.");

            var end = arrayIndex + Count;

            for (var i = arrayIndex; i < end; ++i)
            {
                array[i] = Item;
            }
        }
    }
}