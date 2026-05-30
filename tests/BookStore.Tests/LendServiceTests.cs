using BookStore.Application.Interfaces;
using Moq;

using BookStore.Domain.Entities;
using BookStore.Application.UseCases;

namespace BookStore.Tests;

public class LendServiceTests
{
    private List<Guid> GenerateGuids()
    {
        List<Guid> guids = [];

        //id
        guids.Add(Guid.NewGuid());
        //bookIid
        guids.Add(Guid.NewGuid());
        //readerId
        guids.Add(Guid.NewGuid());

        return guids;
    }

    [Fact]
    public void GetByIdSpecific()
    {
        var guids = this.GenerateGuids();

        var mockLendRepo = new Mock<ILendRepository>();
        var mockBookRepo = new Mock<IBookRepository>();
        var mockReaderRepo = new Mock<IReaderRepository>();

        mockLendRepo.Setup(repo => repo.GetById(guids[0]))
        .Returns( new Lend(id: guids[0], 
            bookId: guids[1], 
            readerId: guids[2],
            startDate: DateTime.Now, endDate: DateTime.Now
        ));

        var service = new LendService(mockLendRepo.Object, mockBookRepo.Object, mockReaderRepo.Object);
        var result = service.GetLend(guids[0]);

        Assert.Equal(guids[1] , result?.BookId);
        Assert.Equal(guids[2] , result?.ReaderId);
    }

    [Fact]
    public void SavingWithAllNeedParameters()
    {
        var guids = this.GenerateGuids();
        
        var mockLendRepo = new Mock<ILendRepository>();
        var mockBookRepo = new Mock<IBookRepository>();
        var mockReaderRepo = new Mock<IReaderRepository>();

        var lend = new Lend(id: guids[0], 
            bookId: guids[1], 
            readerId: guids[2],
            startDate: DateTime.Now, endDate: DateTime.Now
        );

        mockLendRepo.Setup(repo => repo.Add(lend)).Returns(true);

        var service = new LendService(mockLendRepo.Object, mockBookRepo.Object, mockReaderRepo.Object);
        var result = service.Add(lend);

        Assert.True(result);
    }

    [Fact]
    public void CannotSaveNewLendWithSameBookIdElseDelivery()
    {
        var guids = this.GenerateGuids();
        
        var mockLendRepo = new Mock<ILendRepository>();
        var mockBookRepo = new Mock<IBookRepository>();
        var mockReaderRepo = new Mock<IReaderRepository>();


        //first lend
        var lend = new Lend(id: guids[0], 
            bookId: guids[2], 
            readerId: guids[2],
            startDate: DateTime.Now, endDate: DateTime.Now
        );
        mockLendRepo.Setup(repo => repo.Add(lend)).Returns(true);
        var service = new LendService(mockLendRepo.Object, mockBookRepo.Object, mockReaderRepo.Object);
        var result = service.Add(lend);

        Assert.True(result);

        var newReaderId = Guid.NewGuid();

        //second lend
        var lendTwo = new Lend(id: guids[0], 
            bookId: guids[2], 
            readerId: newReaderId,
            startDate: DateTime.Now, endDate: DateTime.Now
        );
        mockLendRepo.Setup(repo => repo.Add(lendTwo)).Returns(false);
        var resultTwo = service.Add(lendTwo);
        
        Assert.False(resultTwo);
    }
}
