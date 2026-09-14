using NSubstitute;

namespace FirstContact.Tests;

public class GetAllTests
{
    [Fact]
    public void GetAllReturnsContactResponses()
    {
        var repo = Substitute.For<IContactRepository>();
        var service = new ContactService(repo);

        var contacts = new[]
        {
          new Contact("Filthy Frank")
          {Id = 20},
          new Contact("Dirty Dave")
          {Id = 40}
        };

        repo.GetAll().Returns(contacts);

        var contactList = service.GetAll();

        Assert.Equal(2, contactList.Count);

        Assert.Contains(contactList, response =>
            response.Id == 20 &&
            response.Name == "Filthy Frank");

        Assert.Contains(contactList, response =>
            response.Id == 40 &&
            response.Name == "Dirty Dave");
    }


    [Fact]
    public void GetAllReturnsAnEmptyListIfNoContacts()
    {
        var repo = Substitute.For<IContactRepository>();
        var service = new ContactService(repo);

        var contacts = Array.Empty<Contact>();

        repo.GetAll().Returns(contacts);

        var contactList = service.GetAll();

        Assert.Empty(contactList);
    }
}