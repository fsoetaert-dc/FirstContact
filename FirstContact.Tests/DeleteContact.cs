using NSubstitute;

namespace FirstContact.Tests;

public class DeleteContactTests
{
    [Fact]
    public void DeleteContactSendsCorrectIdToRepository()
    {
        var repo = Substitute.For<IContactRepository>();
        var service = new ContactService(repo);

        service.DeleteContact(20);
        repo.Received(1).Delete(Arg.Is(20));
    }

    [Fact]
    public void DeleteContactReturnsTrueWhenContactisDeleted()
    {
        var repo = Substitute.For<IContactRepository>();
        var service = new ContactService(repo);

        repo.Delete(20).Returns(true);
        var response = service.DeleteContact(20);
        Assert.True(response);

        repo.Received(1).Delete(20);
    }

    [Fact]
    public void DeleteContactReturnsFalseWhenContactCannotBeDeleted()
    {
        var repo = Substitute.For<IContactRepository>();
        var service = new ContactService(repo);

        repo.Delete(20).Returns(false);
        var response = service.DeleteContact(20);

        Assert.False(response);
    }
}