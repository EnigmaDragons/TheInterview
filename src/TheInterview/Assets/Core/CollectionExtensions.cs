using System;
using System.Collections.Generic;
using System.Linq;

public static class CollectionExtensions
{    
    [Obsolete] public static bool None<T>(this IEnumerable<T> items, Func<T, bool> condition) => !items.Any(condition);
    [Obsolete] public static void ForEach<T>(this IEnumerable<T> items, Action<T> action) => items.ToList().ForEach(action);
    [Obsolete] public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, T item) => items.Concat(item.AsArray());
    [Obsolete] public static bool None<T>(this IEnumerable<T> items) => !items.Any();
    [Obsolete] public static List<T> With<T>(this IEnumerable<T> list, T item) => list.Concat(new[] {item}).ToList();
    [Obsolete] public static List<T> Without<T>(this IEnumerable<T> list, T item)
    {
        var copyList = list.ToList();
        copyList.Remove(item);
        return copyList;
    }
    
    public static T[] AsArray<T>(this T item) => new [] {item};
    public static TValue ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> d, TKey key, Func<TValue> getDefault) => d.TryGetValue(key, out var value) ? value : getDefault();

    public static void ForEach<T>(this T[] arr, Action<T> action)
    {
        foreach (var t in arr)
            action(t);
    }
}
