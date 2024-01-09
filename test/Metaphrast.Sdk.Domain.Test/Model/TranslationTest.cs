using FluentAssertions;
using Metaphrast.Sdk.Domain.Enum;
using Metaphrast.Sdk.Domain.Exception;
using Metaphrast.Sdk.Domain.Model;

namespace Metaphrast.Sdk.Domain.Test.Model;

public class TranslationTest
{
    [Fact]
    public void DefaultTranslationCreation()
    {
        var translation = new Translation("MyKey", "MyText");
        translation.Identifier.Should().Be("MyKey");
        translation.Text.Should().Be("MyText");
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    public void ValidationErrorThrowsMetaphrastException(string id)
    {
        var action = () =>
        {
            var translation = new Translation(id, "");
        };

        action.Should().Throw<MetaphrastDomainException>().Where(ex => ex.Reason == ErrorDomainReason.ValidationError);
    }
}