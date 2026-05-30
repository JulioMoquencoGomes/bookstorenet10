using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        IEnumerable<T> GetAll();
        IQueryable<T> GetAsQueryable();
        T? GetById(Guid id);

        bool Add(T entity);
        bool Update(T entity);

        bool Delete(Guid id);
        bool Remove(Guid id);
    }
}