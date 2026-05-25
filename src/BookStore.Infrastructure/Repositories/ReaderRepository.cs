using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly AppDbContext _dbContext;

        public ReaderRepository(AppDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public IEnumerable<Reader> GetAll() => _dbContext.Readers.ToList();
        
        public Reader? GetById(Guid id) => _dbContext.Readers.FirstOrDefault(u => u.Id == id);
        
        public bool Add(Reader entity)
        {
            try
            {
                _dbContext.Add(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception err)
            {
                return false;
            }
        }

        public bool Update(Reader entity)
        {
            try
            {
                _dbContext.Update(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception err)
            {
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            var reader = this.GetById(id);
            if(reader != null) {
                _dbContext.Remove(reader);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Remove(Guid id) => this.Delete(id);
    }
}