using FluentAssertions;
using Metaphrast.Sdk.Domain.Model;
using Metaphrast.Sdk.Domain.Port;
using Metaphrast.Sdk.Domain.Service;
using Moq;
using System.Globalization;
using System;
using Metaphrast.Sdk.Domain.Enum;
using Metaphrast.Sdk.Domain.Exception;

namespace Metaphrast.Sdk.Domain.Test.Service;

public class ProjectServiceTest
{
    [Fact]
    public void CreateProjectShouldSaveProject()
    {
        var guid = Guid.NewGuid();

        var moqAdapter = new Mock<IProjectRepository>();
        moqAdapter.Setup(adapter => adapter.Save(It.IsAny<Project>())).Returns(guid);

        var service = new ProjectService(moqAdapter.Object);
        var expectedGuid = service.Create("MyRandomProject");

        moqAdapter.Verify(d => d.Save(It.IsAny<Project>()), Times.Once);
        expectedGuid.Should().Be(guid);
    }

    [Fact]
    public void CreateProjectShouldRaiseMetaphrastException()
    {
        var moqAdapter = new Mock<IProjectRepository>();
        moqAdapter.Setup(adapter => adapter.Save(It.IsAny<Project>())).Throws(new MetaphrastDomainException(ErrorDomainReason.EntityInvalidOperation));

        var action = () =>
        {
            var service = new ProjectService(moqAdapter.Object);
            service.Create("MyProject");
        };

        action.Should().Throw<MetaphrastDomainException>().Where(ex => ex.Reason == ErrorDomainReason.EntityInvalidOperation);
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    public void CreateProjectShouldRaiseMetaphrastValidationError(string name)
    {
        var guid = Guid.NewGuid();

        var moqAdapter = new Mock<IProjectRepository>();
        moqAdapter.Setup(adapter => adapter.Save(It.IsAny<Project>())).Returns(guid);

        var action = () =>
        {
            var service = new ProjectService(moqAdapter.Object);
            service.Create(name);
        };

        action.Should().Throw<MetaphrastDomainException>().Where(ex => ex.Reason == ErrorDomainReason.ValidationError);
    }

    [Fact]
    public void CreateProjectShouldRaiseMetaphrastEntityNotCreated()
    {
        var moqAdapter = new Mock<IProjectRepository>();
        moqAdapter.Setup(adapter => adapter.Save(It.IsAny<Project>())).Throws(new System.Exception("Any Exception"));

        var action = () =>
        {
            var service = new ProjectService(moqAdapter.Object);
            service.Create("MyProject");
        };

        action.Should().Throw<MetaphrastDomainException>().Where(ex => ex.Reason == ErrorDomainReason.EntityNotCreated);
    }

    [Fact]
    public void AddLanguagePersistsLanguage()
    {
        var language = new CultureInfo("de");
        var guid = Guid.NewGuid();
        var project = new Project(guid, "MyRandomProject");

        var moqAdapter = new Mock<IProjectRepository>();
        moqAdapter.Setup(adapter => adapter.Save(It.IsAny<Project>())).Returns(guid);
        moqAdapter.Setup(adapter => adapter.GetProjectById(It.IsAny<Guid>())).Returns(project);
        
        var service = new ProjectService(moqAdapter.Object);
        project.ContainsLanguage(language).Should().BeFalse();
        service.AddLanguage(guid, language);

        moqAdapter.Verify(d => d.GetProjectById(It.IsAny<Guid>()), Times.Once);
        moqAdapter.Verify(d => d.Save(It.IsAny<Project>()), Times.Once);
        project.ContainsLanguage(language).Should().BeTrue();
    }

    [Fact]
    public void AddLanguageThrowsMetaphrastExceptionEntityNotFound()
    {
        var language = new CultureInfo("de");
        var guid = Guid.NewGuid();

        var action = () =>
        {
            var moqAdapter = new Mock<IProjectRepository>();
            var service = new ProjectService(moqAdapter.Object);
            service.AddLanguage(guid, language);
        };

        action.Should().Throw<MetaphrastDomainException>().Where(ex => ex.Reason == ErrorDomainReason.EntityNotFound);
    }

    [Fact]
    public void AddLanguageThrowsMetaphrastExceptionEntityAlreadyExists()
    {
        var language = new CultureInfo("de");
        var guid = Guid.NewGuid();

        var project = new Project(guid, "MyRandomProject");
        project.AddLanguage(language);


        var action = () =>
        {
            var moqAdapter = new Mock<IProjectRepository>();
            moqAdapter.Setup(adapter => adapter.GetProjectById(It.IsAny<Guid>())).Returns(project);
            var service = new ProjectService(moqAdapter.Object);
            service.AddLanguage(guid, language);
        };

        action.Should().Throw<MetaphrastDomainException>().Where(ex => ex.Reason == ErrorDomainReason.EntityAlreadyExists);
    }

    [Fact]
    public void AddLanguageThrowsMetaphrastExceptionEntityNotUpdated()
    {
        var language = new CultureInfo("de");
        var guid = Guid.NewGuid();

        var project = new Project(guid, "MyRandomProject");

        var action = () =>
        {
            var moqAdapter = new Mock<IProjectRepository>();
            moqAdapter.Setup(adapter => adapter.GetProjectById(It.IsAny<Guid>())).Returns(project);
            moqAdapter.Setup(adapter => adapter.Save(It.IsAny<Project>())).Throws(new System.Exception("Invalid Operation"));
            var service = new ProjectService(moqAdapter.Object);
            service.AddLanguage(guid, language);
        };

        action.Should().Throw<MetaphrastDomainException>().Where(ex => ex.Reason == ErrorDomainReason.EntityNotUpdated);
    }

    [Fact]
    public void AddLanguageThrowsMetaphrastException()
    {
        var language = new CultureInfo("de");
        var guid = Guid.NewGuid();

        var project = new Project(guid, "MyRandomProject");

        var action = () =>
        {
            var moqAdapter = new Mock<IProjectRepository>();
            moqAdapter.Setup(adapter => adapter.GetProjectById(It.IsAny<Guid>())).Returns(project);
            moqAdapter.Setup(adapter => adapter.Save(It.IsAny<Project>())).Throws(new MetaphrastDomainException(ErrorDomainReason.EntityInvalidOperation));
            var service = new ProjectService(moqAdapter.Object);
            service.AddLanguage(guid, language);
        };

        action.Should().Throw<MetaphrastDomainException>().Where(ex => ex.Reason == ErrorDomainReason.EntityInvalidOperation);
    }
}