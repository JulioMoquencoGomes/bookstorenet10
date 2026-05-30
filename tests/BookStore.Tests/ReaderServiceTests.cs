using BookStore.Application.Interfaces;
using Moq;

using BookStore.Domain.Entities;
using BookStore.Application.UseCases;

namespace BookStore.Tests;

public class ReaderServiceTests
{
    [Fact]
    public void GetByIdSpecific()
    {
        var newGuid = Guid.NewGuid();
        var birthday = new DateTime(2000, 10, 25);
        var mockRepo = new Mock<IReaderRepository>();

        mockRepo.Setup(repo => repo.GetById(newGuid))
        .Returns( new Reader(id: newGuid, name: "Katilsa", birthday: birthday, urlimg: "https://url.com"));

        var service = new ReaderService(mockRepo.Object);
        var result = service.GetReader(newGuid);

        Assert.Equal("Katilsa" , result?.Name);
    }

    [Fact]
    public void SavingWithAllNeedParameters()
    {
        var newGuid = Guid.NewGuid();
        var mockRepo = new Mock<IReaderRepository>();

        var reader = new Reader(id: newGuid, name: "Katilsa", birthday: null, 
            urlimg: "https://url.com");

        mockRepo.Setup(repo => repo.Add(reader)).Returns(true);

        var service = new ReaderService(mockRepo.Object);
        var result = service.Add(reader);

        Assert.True(result);
    }

    [Fact]
    public void NotSaveWhenNameIsEmpty()
    {
        var newGuid = Guid.NewGuid();
        var mockRepo = new Mock<IReaderRepository>();

        var reader = new Reader(id: newGuid, name: "", birthday: null, 
            urlimg: "https://url.com");

        mockRepo.Setup(repo => repo.Add(reader)).Returns(false);

        var service = new ReaderService(mockRepo.Object);
        var result = service.Add(reader);

        Assert.False(result);
    }

    [Fact]
    public void NotSaveWhenUrlIsEmpty()
    {
        var newGuid = Guid.NewGuid();
        var mockRepo = new Mock<IReaderRepository>();

        var reader = new Reader(id: newGuid, name: "Katilsa", birthday: null, 
            urlimg: "");

        mockRepo.Setup(repo => repo.Add(reader)).Returns(false);

        var service = new ReaderService(mockRepo.Object);
        var result = service.Add(reader);

        Assert.False(result);
    }
}
