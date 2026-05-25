using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book? GetById(Guid id);

        bool Add(Book entity);
        bool Update(Book entity);

        bool Delete(Guid id);
        bool Remove(Guid id);
    }
}