using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.UseCases
{
    public class ReaderService
    {
        private readonly IReaderRepository _readerRepository;

        public ReaderService(IReaderRepository readerRepository)
        {
            _readerRepository = readerRepository;
        }

        public IEnumerable<Reader> GetReaders() => _readerRepository.GetAll();
        IQueryable<Reader> GetAsQueryable() => _readerRepository.GetAsQueryable();
        public Reader? GetReader(Guid id) => _readerRepository.GetById(id);
        public bool Add(Reader reader) => _readerRepository.Add(reader);
        public bool Update(Reader reader) => _readerRepository.Update(reader);
        public bool Delete(Guid id) => _readerRepository.Delete(id);
        public bool Remove(Guid id) => _readerRepository.Remove(id);
    }
}