using NSubstitute;

namespace FirstContact.Tests;

public class SearchTests
{
    [Fact]
    public void SearchSearchesSearchTermInRepo()
    {
        var repo = Substitute.For<IContactRepository>();
        var service = new ContactService(repo);

        var contact = new Contact("Filthy Frank"){Id = 20};

        repo.Search("Filthy Frank").Returns(new[] { contact });
        //makes sure that when repo.Search("Filthy Frank") is called it returns new[] { contact }

        var searchResult = service.Search("Filthy Frank").ToList();

        repo.Received(1).Search("Filthy Frank");
        //checks if search has been called exactly 1 time with the term "Filthy Frank"
    }


    [Fact]
    public void SearchReturnsListOfContacts()
    {
        var repo = Substitute.For<IContactRepository>();
        var service = new ContactService(repo);

        var contact = new Contact("Filthy Frank") {Id = 20};
        
        repo.Search("Filthy Frank").Returns(new[] { contact });

        var searchResult = service.Search("Filthy Frank").ToList();

        Assert.Single(searchResult);

        var response = searchResult[0];
        Assert.Equal(20, response.Id);
        Assert.Equal("Filthy Frank", response.Name);
    }

    [Fact]
    public void SearchReturnsEmptyArrayIfNoContactsFound()
    {
        var repo = Substitute.For<IContactRepository>();
        var service = new ContactService(repo);

        repo.Search("Dirty Dave").Returns([]);

        var searchResult = service.Search("Dirty Dave").ToList();

        Assert.Empty(searchResult);
    }
}