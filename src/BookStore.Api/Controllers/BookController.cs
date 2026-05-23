using Microsoft.AspNetCore.Mvc;
using BookStore.Application.UseCases;
using BookStore.Domain.Entities;
using System.Collections.Generic;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public JsonResult Get() => new JsonResult(new { success = true, book = _bookService.GetBooks()});

        [HttpGet("{id}")]
        public JsonResult Get(Guid id)
        {
            var book = _bookService.GetBook(id);
            if (book == null)
            {
                return new JsonResult(new { success = false, message = "Not found"});
            }
            return new JsonResult(new { success = true, book = book});
        }

        [HttpPost]
        public JsonResult Post(Book book)
        {
            book = _bookService.Add(book);
            return new JsonResult(new { success = true, book = book});
        }

        [HttpPut("{id}")]
        public JsonResult Put(Guid id, Book book)
        {
            if (id != book.Id)
            {
                return new JsonResult(new { success = false, message = "Not updated"});
            }
            book = _bookService.Update(book);
            return new JsonResult(new { success = true, book = book});
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(Guid id)
        {
            _bookService.Delete(id);
            return new JsonResult(new { success = true, message = "Deleted with successful"});
        }
    }
}