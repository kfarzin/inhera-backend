namespace Inhera.Shared.Util.Extensions
{
    public static class ListExtensions
    {
        public static bool IsSameAs<T>(this IList<T> firstList, IList<T> secondList) => (firstList, secondList) switch
        {
            (null, null) => true,
            (var a, null) when a != null => false,
            (null, var a) when a != null => false,
            (var a, var b) when a.Count != b.Count => false,
            (var a, var b) => a.Except(b).Any(),
        };

        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(
    this List<List<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> result =
              new[] { Enumerable.Empty<T>() };
            foreach (var sequence in sequences)
            {
                var s = sequence;
                result =
                  from seq in result
                  from item in s
                  select seq.Concat(new[] { item });
            }
            return result;
        }
    }
}
