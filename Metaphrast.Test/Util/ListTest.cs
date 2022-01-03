using System.Collections.Generic;
using Metaphrast.Util;
using Xunit;

namespace Metaphrast.Test.Util;
public class ListTest
{
    [Theory]
    [InlineData(55, 50, 2)]
    [InlineData(55, 1, 55)]
    [InlineData(0, 1, 0)]
    public void SplitListTest(int elements, int split, int expectedSplits)
    {
        var list = new List<string>();
        for (var i = 0; i < elements; i++)
        {
            list.Add("A" + i);
        }

        var lists = ListHelper.Split(list, split);
        Assert.Equal(expectedSplits, lists.Count);
    }

}
