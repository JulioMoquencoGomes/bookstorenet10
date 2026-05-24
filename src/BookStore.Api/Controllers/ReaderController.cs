using Microsoft.AspNetCore.Mvc;
using BookStore.Application.UseCases;
using BookStore.Domain.Entities;
using System.Collections.Generic;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReaderController : ControllerBase
    {
        private readonly ReaderService _readerService;

        public ReaderController(ReaderService readerService)
        {
            _readerService = readerService;
        }

        [HttpGet]
        public JsonResult Get() => new JsonResult(new { success = true, reader = _readerService.GetReaders()});

        [HttpGet("{id}")]
        public JsonResult Get(Guid id)
        {
            var reader = _readerService.GetReader(id);
            if (reader == null)
            {
                return new JsonResult(new { success = false, message = "Not found"});
            }
            return new JsonResult(new { success = true, reader = reader});
        }

        [HttpPost]
        public JsonResult Post(Reader reader)
        {
            reader = _readerService.Add(reader);
            return new JsonResult(new { success = true, reader = reader});
        }

        [HttpPut("{id}")]
        public JsonResult Put(Guid id, Reader reader)
        {
            if (id != reader.Id)
            {
                return new JsonResult(new { success = false, message = "Not updated"});
            }
            reader = _readerService.Update(reader);
            return new JsonResult(new { success = true, reader = reader});
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(Guid id)
        {
            _readerService.Delete(id);
            return new JsonResult(new { success = true, message = "Deleted with successful"});
        }
    }
}