using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.UseCases
{
    public class LendService
    {
        private readonly ILendRepository _lendRepository;

        public LendService(ILendRepository lendRepository)
        {
            _lendRepository = lendRepository;
        }

        public IEnumerable<Lend> GetLends() => _lendRepository.GetAll();
        IQueryable<Lend> GetAsQueryable() => _lendRepository.GetAsQueryable();
        public Lend? GetLend(Guid id) => _lendRepository.GetById(id);
        public bool Add(Lend lend) => _lendRepository.Add(lend);
        public bool Update(Lend lend) => _lendRepository.Update(lend);
        public bool Delete(Guid id) => _lendRepository.Delete(id);
        public bool Remove(Guid id) => _lendRepository.Remove(id);
    }
}