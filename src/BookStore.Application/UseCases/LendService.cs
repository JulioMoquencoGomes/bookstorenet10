using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.UseCases
{
    public class LendService
    {
        private readonly ILendRepository _lendRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IReaderRepository _readerRepository;

        public LendService(ILendRepository lendRepository,
        IBookRepository bookRepository,
        IReaderRepository readerRepository
        )
        {
            _lendRepository = lendRepository;
            _bookRepository = bookRepository;
            _readerRepository = readerRepository;
        }

        public IEnumerable<Lend> GetLends()
        {
            var lends = _lendRepository.GetAll();
            foreach(var lend in lends)
            {
                if(lend != null)
                {
                    Lend newLend = this.GetBookAndReaderByLend(lend);
                    lend.Book = newLend.Book;
                    lend.Reader = newLend.Reader;
                }
            }
            return lends;
        }
        IQueryable<Lend> GetAsQueryable() => _lendRepository.GetAsQueryable();
        
        public Lend? GetLend(Guid id)
        {
            var lend = _lendRepository.GetById(id);
            if(lend != null)
            {
                lend = this.GetBookAndReaderByLend(lend);
            }
            return lend;
        }

        private Lend GetBookAndReaderByLend(Lend lend)
        {
            lend.Book = _bookRepository.GetById(lend.BookId);
            lend.Reader = _readerRepository.GetById(lend.ReaderId);
            return lend;
        }

        public bool Add(Lend lend){

            var bookId = lend.BookId;
            var thereIsLend = _lendRepository.GetAsQueryable()
                .Where(w=>w.BookId == bookId && w.DeliveryDate == null).FirstOrDefault();


            if(thereIsLend != null)
            {
                return false;
            }

            var cond = _lendRepository.Add(lend);
            return cond;
        }
        public bool Update(Lend lend) => _lendRepository.Update(lend);
        public bool Delete(Guid id) => _lendRepository.Delete(id);
        public bool Remove(Guid id) => _lendRepository.Remove(id);
    }
}