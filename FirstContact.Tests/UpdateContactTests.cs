using NSubstitute;

namespace FirstContact.Tests;

public class UpdateContactTests
{
    [Fact]
    public void UpdateContactChangesNameIfContactExistsAndCommmitReturnsTrue()
    {
        var repo = Substitute.For<IContactRepository>();
        var service = new ContactService(repo);
        var contact = new Contact("Filthy Frank") { Id = 20 };

        repo.GetById(20).Returns(contact);
        var request = new UpdateContactRequest { Name = "Dirty Dave" };
        var response = service.UpdateContact(20, request);

        repo.Received(1).Commit();

        Assert.True(response);
        Assert.Equal("Dirty Dave", contact.Name);
    }

    [Fact]
    public void UpdateContactReturnsFalseWhenContactDoesNotExist()
    {
        var repo = Substitute.For<IContactRepository>();
        var service = new ContactService(repo);

        repo.GetById(20).Returns((Contact?)null);
        var request = new UpdateContactRequest { Name = "Dirty Dave" };
        var response = service.UpdateContact(20, request);

        repo.Received(0).Commit();

        Assert.False(response);
    }

}