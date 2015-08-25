using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtension {
    public static void ZipForEach<T1, T2>(this IEnumerable<T1> sequence1, IEnumerable<T2> sequence2, Action<T1, T2> selector)
    {
        using (var enu1 = sequence1.GetEnumerator())
        using (var enu2 = sequence2.GetEnumerator())
        {
            while (enu1.MoveNext() && enu2.MoveNext())
            {
                selector(enu1.Current, enu2.Current);
            }
        }
    }

    public static IEnumerable<T> SafeSkip<T>(this IEnumerable<T> sequence, int skipCount)
    {
        return sequence == null ? null : (skipCount > 0 ? sequence.Skip(skipCount) : sequence);
    }

    public static T SafeIndexer<T>(this List<T> list, int index) where T : class
    {
        return SafeIndexer(list, index, null);
    }

    public static T SafeIndexer<T>(this List<T> list, int index, T defaultObj) where T : class
    {
        return (index < 0 || index >= list.Count) ? defaultObj : list[index];
    }
}
