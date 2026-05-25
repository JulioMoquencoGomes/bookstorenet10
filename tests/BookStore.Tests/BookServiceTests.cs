using BookStore.Application.Interfaces;
using Moq;

using BookStore.Domain.Entities;
using BookStore.Application.UseCases;

namespace BookStore.Tests;

public class BookServiceTests
{
    [Fact]
    public void GetByIdSpecific()
    {
        var newGuid = Guid.NewGuid();
        var mockRepo = new Mock<IBookRepository>();

        mockRepo.Setup(repo => repo.GetById(newGuid))
        .Returns( new Book(id: newGuid, name: "Nome de teste", author: "Autor de teste", urlimg: "https://url.com"));

        var service = new BookService(mockRepo.Object);
        var result = service.GetBook(newGuid);

        Assert.Equal("Nome de teste" , result?.Name);
        Assert.Equal("Autor de teste" , result?.Author);
        Assert.Equal("https://url.com" , result?.Urlimg);
    }

    [Fact]
    public void SavingWithAllNeedParameters()
    {
        var newGuid = Guid.NewGuid();
        var mockRepo = new Mock<IBookRepository>();

        var book = new Book(id: newGuid, name: "1984", author: "George Orwell", 
            urlimg: "https://m.media-amazon.com/images/I/61NAx5pd6XL.jpg");

        mockRepo.Setup(repo => repo.Add(book)).Returns(true);

        var service = new BookService(mockRepo.Object);
        var result = service.Add(book);

        Assert.True(result);
    }

    [Fact]
    public void NotSaveWhenNameIsEmpty()
    {
        var newGuid = Guid.NewGuid();
        var mockRepo = new Mock<IBookRepository>();

        var book = new Book(id: newGuid, name: "", author: "George Orwell", 
            urlimg: "https://m.media-amazon.com/images/I/61NAx5pd6XL.jpg");
            
        mockRepo.Setup(repo => repo.Add(book)).Returns(false);

        var service = new BookService(mockRepo.Object);
        var result = service.Add(book);

        Assert.False(result);
    }

    [Fact]
    public void NotSaveWhenAuthorIsEmpty()
    {
        var newGuid = Guid.NewGuid();
        var mockRepo = new Mock<IBookRepository>();

        var book = new Book(id: newGuid, name: "1984", author: "", 
            urlimg: "https://m.media-amazon.com/images/I/61NAx5pd6XL.jpg");
            
        mockRepo.Setup(repo => repo.Add(book)).Returns(false);

        var service = new BookService(mockRepo.Object);
        var result = service.Add(book);

        Assert.False(result);
    }

    [Fact]
    public void NotSaveWhenUrlIsEmpty()
    {
        var newGuid = Guid.NewGuid();
        var mockRepo = new Mock<IBookRepository>();

        var book = new Book(id: newGuid, name: "1984", author: "George Orwell", 
            urlimg: "");
            
        mockRepo.Setup(repo => repo.Add(book)).Returns(false);

        var service = new BookService(mockRepo.Object);
        var result = service.Add(book);

        Assert.False(result);
    }
}
