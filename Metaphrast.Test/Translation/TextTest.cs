using Metaphrast.Translation;
using Xunit;

namespace Metaphrast.Test.Translation;
public class TextTest
{

    [Theory]
    [InlineData(null, null, "", "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855")]
    [InlineData("", null, "", "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855")]
    [InlineData("     ", null, "     ", "7879981d4f226a8f0191d36730c07205d7a5ff1c780fca9b2f905f25264cf636")]
    [InlineData("England", null, "England", "c0ea960b065c6aa6dd567a1a741fa21aee3972692096e158cebb4309a3b2a716")]
    [InlineData(" England ", null, " England ", "3c15c0e86d4839c001b74dae1e72722445c70238f9241cf15c7e81b889456589")]
    [InlineData("England", "c0ea960b065c6aa6dd567a1a741fa21aee3972692096e158cebb4309a3b2a716", "England", "c0ea960b065c6aa6dd567a1a741fa21aee3972692096e158cebb4309a3b2a716")] // Value changed -> Wrong hash but store it to indicate an new text value to translate
    public void TextGenerationTest(string translationText, string hash, string expectedTranslationText, string expectedHash)
    {
        var text = new Text(translationText, hash);
        Assert.Equal(text.TranslationText, expectedTranslationText);
        Assert.Equal(text.Sha256, expectedHash);
    }
}
