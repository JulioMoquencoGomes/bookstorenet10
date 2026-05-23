using System.Collections.Generic;
using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book? GetById(Guid id);

        Book Add(Book entity);
        Book Update(Book entity);

        bool Delete(Guid id);
        bool Remove(Guid id);
    }
}