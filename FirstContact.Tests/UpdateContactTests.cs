using NSubstitute;

namespace FirstContact.Tests;

public class UpdateContactTests
{
    [Fact]
    public void UpdateContactChangesNameIfContactExistsAndCommmitReturnsTrue()
    {
        var repo = Substitute.For<IContactRepository>();
        var service = new ContactService(repo);

        repo.GetById(20).Returns(new Contact("Filthy Frank"){Id = 20});
        var request = new UpdateContactRequest {Name = "Dirty Dave"};
        var response = service.UpdateContact(20, request);

        repo.Received(1).Commit();

        Assert.True(response);
    }

    [Fact]
    public void UpdateContactReturnsFalseWhenContactDoesNotExist()
    {
        var repo = Substitute.For<IContactRepository>();
        var service = new ContactService(repo);

        repo.GetById(20).Returns((Contact?)null);
        var request = new UpdateContactRequest {Name = "Dirty Dave"};
        var response = service.UpdateContact(20, request);

        repo.Received(0).Commit();

        Assert.False(response);
    }

}