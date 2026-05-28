using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class LendRepository : RepositoryBase<Lend>, ILendRepository
    {
        public LendRepository(AppDbContext dbcontext): base (dbcontext)
        {
        }
    }
}