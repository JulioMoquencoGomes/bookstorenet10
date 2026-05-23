using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using System.Collections.Generic;

namespace BookStore.Application.UseCases
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<Book> GetBooks() => _bookRepository.GetAll();

        public Book? GetBook(Guid id) => _bookRepository.GetById(id);
        public Book Add(Book book) => _bookRepository.Add(book);
        public Book Update(Book book) => _bookRepository.Update(book);
        public bool Delete(Guid id) => _bookRepository.Delete(id);
        public bool Remove(Guid id) => _bookRepository.Remove(id);
    }
}