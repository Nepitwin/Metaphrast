namespace Metaphrast.Util;
internal static class ListHelper
{
    public static List<List<T>> Split<T>(List<T> locations, int nSize)
    {
        var list = new List<List<T>>();

        for (var i = 0; i < locations.Count; i += nSize)
        {
            list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
        }

        return list;
    }
}
