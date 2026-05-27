using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class ReaderRepository : RepositoryBase<Reader> ,IReaderRepository
    {
        public ReaderRepository(AppDbContext dbcontext) : base (dbcontext)
        {
        }
    }
}