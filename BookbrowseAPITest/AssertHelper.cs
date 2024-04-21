namespace BookbrowseAPI.Test
{
    internal static class AssertHelper
    {
        public static bool IsEqual<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            var list_a = a.ToList();
            var list_b = b.ToList();

            return list_a.SequenceEqual(list_b);
        }
    }
}
