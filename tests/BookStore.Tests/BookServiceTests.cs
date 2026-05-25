using BookStore.Application.Interfaces;
using Moq;

using BookStore.Domain.Entities;
using BookStore.Application.UseCases;

namespace BookStore.Tests;

public class BookServiceTests
{
    [Fact]
    public void GetBySpecificId()
    {
        var newGuid = Guid.NewGuid();
        var mockRepo = new Mock<IBookRepository>();

        mockRepo.Setup(repo => repo.GetById(newGuid))
        .Returns( new Book(id: newGuid, name: "Nome de teste", author: "Autor de teste", urlimg: ""));

        var service = new BookService(mockRepo.Object);
        var result = service.GetBook(newGuid);

        Assert.Equal("Nome de teste" , result?.Name);
        Assert.Equal("Autor de teste" , result?.Author);
    }

    [Fact]
    public void SavingWithAllParametersNeed()
    {
        var newGuid = Guid.NewGuid();
        var mockRepo = new Mock<IBookRepository>();

        var book = new Book(id: newGuid, name: "1984", author: "George Orwell", urlimg: "");
        mockRepo.Setup(repo => repo.Add(book)).Returns(true);

        var service = new BookService(mockRepo.Object);
        var result = service.Add(book);

        Assert.True(result);
    }
}
