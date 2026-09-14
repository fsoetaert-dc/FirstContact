using NSubstitute;

namespace FirstContact.Tests;

public class AddContactTests
{
    [Fact]
    public void AddContactSendsNameToRepo()
    {
        var contact = new Contact("Filthy Frank");

        var repo = Substitute.For<IContactRepository>();
        var service = new ContactService(repo);

        var request = new CreateContactRequest
        {
            Name = contact.Name
        };

        service.AddContact(request);

        repo.Received(1).Add(Arg.Is<Contact>(c => c.Name == "Filthy Frank"));

    }

    [Fact]
    public void AddContactReturnsIdAndName()
    {
        var repo = Substitute.For<IContactRepository>();

        repo.When(r => r.Add(Arg.Any<Contact>()))
            .Do(callInfo =>
            {
                var contact = callInfo.Arg<Contact>();
                contact.Id = 42;
            });

        var service = new ContactService(repo);

        var request = new CreateContactRequest
        {
            Name = "Filthy Frank"
        };

        var response = service.AddContact(request);

        Assert.Equal("Filthy Frank", response.Name);
        Assert.Equal(42, response.Id);

    }
}