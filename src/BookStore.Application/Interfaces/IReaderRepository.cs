using System.Collections.Generic;
using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
    public interface IReaderRepository
    {
        IEnumerable<Reader> GetAll();
        Reader? GetById(Guid id);

        bool Add(Reader entity);
        bool Update(Reader entity);

        bool Delete(Guid id);
        bool Remove(Guid id);
    }
}