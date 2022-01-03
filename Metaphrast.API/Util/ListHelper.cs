namespace Metaphrast.Util;
internal class ListHelper
{
    public static List<List<T>> Split<T>(List<T> locations, int nSize)
    {
        var list = new List<List<T>>();

        for (int i = 0; i < locations.Count; i += nSize)
        {
            list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
        }

        return list;
    }
}
