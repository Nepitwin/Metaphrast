using Metaphrast.Crypto;
using Xunit;

namespace Metaphrast.Test.Crypto;

public class AlgorithmTest
{
    [Theory]
    [InlineData("Italy", "Italien", "b8a549e43270dccdaaa7024e76d3898be28afa6ece57ccd1bdb1040eb1d7763e")]
    [InlineData("Italien", "Italy", "b8a549e43270dccdaaa7024e76d3898be28afa6ece57ccd1bdb1040eb1d7763e")]
    [InlineData("Italy", "Italy", "0000000000000000000000000000000000000000000000000000000000000000")]
    public void Sha256XorHashTest(string inputA, string inputB, string expectedSha256)
    {
        Assert.Equal(expectedSha256, Algorithm.Sha256ToString(Algorithm.Sha256Xor(Algorithm.Sha256ToBytes(inputA), Algorithm.Sha256ToBytes(inputB))));
    }

    [Theory]
    [InlineData("Dutch", "45f16144c18cb09894ec3e2410e50711f7edcd915b80bf7c7151701dda0ee98a")]
    [InlineData("", "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855")]
    public void Sha256InputTest(string input, string expectedSha256)
    {
        Assert.Equal(expectedSha256, Algorithm.Sha256ToString(Algorithm.Sha256ToBytes(input)));
    }

    [Theory]
    [InlineData("45f16144c18cb09894ec3e2410e50711f7edcd915b80bf7c7151701dda0ee98a")]
    [InlineData("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855")]
    public void Sha256HashTest(string sha256)
    {
        Assert.Equal(sha256, Algorithm.Sha256ToString(Algorithm.Sha256StringToByteArray(sha256)));
    }
}

