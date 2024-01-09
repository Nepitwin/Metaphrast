using System.Globalization;
using FluentAssertions;
using Metaphrast.Sdk.Domain.Enum;
using Metaphrast.Sdk.Domain.Exception;
using Metaphrast.Sdk.Domain.Model;

namespace Metaphrast.Sdk.Domain.Test.Model;

public class ProjectTest
{
    [Fact]
    public void DefaultProjectCreation()
    {
        var identifier = Guid.NewGuid();
        var project = new Project(identifier, "MyProject");
        project.Identifier.Should().Be(identifier);
        project.Name.Should().Be("MyProject");
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    public void ValidationErrorThrowsMetaphrastException(string name)
    {
        var action = () =>
        {
            var project = new Project(Guid.NewGuid(), name);
        };

        action.Should().Throw<MetaphrastDomainException>().Where(ex => ex.Reason == ErrorDomainReason.ValidationError);
    }

    [Fact]
    public void AddLanguageToProjectShouldBeTrue()
    {
        var project = new Project(Guid.NewGuid(), "MyProject");
        project.AddLanguage(new CultureInfo("de")).Should().BeTrue("First time include from language");
        project.ContainsLanguage(new CultureInfo("de")).Should().BeTrue();
    }

    [Fact]
    public void AddMultipleEqualLanguagesToProjectShouldBeFalse()
    {
        var project = new Project(Guid.NewGuid(), "MyProject");
        project.AddLanguage(new CultureInfo("de")).Should().BeTrue();
        project.AddLanguage(new CultureInfo("de")).Should().BeFalse("Equal language which is already added");
        project.ContainsLanguage(new CultureInfo("de")).Should().BeTrue();
    }

    [Fact]
    public void AddSubLanguagesToProjectShouldBeTrue()
    {
        var project = new Project(Guid.NewGuid(), "MyProject");
        project.AddLanguage(new CultureInfo("en-US")).Should().BeTrue();
        project.AddLanguage(new CultureInfo("en-GB")).Should().BeTrue();
        project.ContainsLanguage(new CultureInfo("en-US")).Should().BeTrue();
        project.ContainsLanguage(new CultureInfo("en-GB")).Should().BeTrue();
    }

    [Fact]
    public void ContainsLanguageShouldBeFalse()
    {
        var project = new Project(Guid.NewGuid(), "MyProject");
        project.ContainsLanguage(new CultureInfo("de")).Should().BeFalse();
    }

    [Fact]
    public void ContainsTranslationsShouldBeFalse()
    {
        var project = new Project(Guid.NewGuid(), "MyProject");
        project.ContainsTranslation(new CultureInfo("de"), new Translation("MyIdentifier", "MyText")).Should().BeFalse();
    }

    [Fact]
    public void UpdateTranslationShouldThrowNotImplementedException()
    {
        var project = new Project(Guid.NewGuid(), "MyProject");
        Action action = () =>
            project.UpdateTranslation(new CultureInfo("de"), new Translation("MyIdentifier", "MyText"));
        action.Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void UpdateLanguageShouldThrowNotImplementedException()
    {
        var project = new Project(Guid.NewGuid(), "MyProject");
        Action action = () =>
            project.UpdateLanguage(new CultureInfo("de"),  new CultureInfo("de-DE"));
        action.Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void DeleteTranslationShouldThrowNotImplementedException()
    {
        var project = new Project(Guid.NewGuid(), "MyProject");
        Action action = () =>
            project.DeleteTranslation(new CultureInfo("de"), new Translation("MyIdentifier", "MyText"));
        action.Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void DeleteLanguageShouldThrowNotImplementedException()
    {
        var project = new Project(Guid.NewGuid(), "MyProject");
        Action action = () =>
            project.DeleteLanguage(new CultureInfo("de"));
        action.Should().Throw<NotImplementedException>();
    }
}