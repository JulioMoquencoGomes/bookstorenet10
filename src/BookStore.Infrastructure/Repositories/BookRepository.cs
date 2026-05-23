using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _dbContext;

        public BookRepository(AppDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public IEnumerable<Book> GetAll() => _dbContext.Books.ToList();
        
        public Book? GetById(Guid id) => _dbContext.Books.FirstOrDefault(u => u.Id == id);
        
        public Book Add(Book entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Book Update(Book entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public bool Delete(Guid id)
        {
            var book = this.GetById(id);
            if(book != null) {
                _dbContext.Remove(book);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Remove(Guid id) => this.Delete(id);
    }
}