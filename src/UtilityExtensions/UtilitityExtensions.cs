namespace UtilityExtensions;

using System.Collections.Generic;

public static class IEnumerableExtensions {
    public static IEnumerable<(T item, int index)> Enumerated<T>(this IEnumerable<T> self)       
       => self.Select((item, index) => (item, index));
}