using Microsoft.AspNetCore.Mvc;
using BookStore.Application.UseCases;
using BookStore.Domain.Entities;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LendController : ControllerBase
    {
        private readonly LendService _lendService;

        public LendController(LendService lendService)
        {
            _lendService = lendService;
        }

        [HttpGet]
        public JsonResult Get() => new JsonResult(new { success = true, lend = _lendService.GetLends()});

        [HttpGet("{id}")]
        public JsonResult Get(Guid id)
        {
            var lend = _lendService.GetLend(id);
            if (lend == null)
            {
                return new JsonResult(new { success = false, message = "Not found"});
            }
            return new JsonResult(new { success = true, lend = lend});
        }

        [HttpPost]
        public JsonResult Post(Lend lend)
        {
            var success = _lendService.Add(lend);
            return new JsonResult(new { success = success});
        }

        [HttpPut("{id}")]
        public JsonResult Put(Guid id, Lend lend)
        {
            if (id != lend.Id)
            {
                return new JsonResult(new { success = false, message = "Not updated"});
            }
            var success = _lendService.Update(lend);
            return new JsonResult(new { success = success});
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(Guid id)
        {
            _lendService.Delete(id);
            return new JsonResult(new { success = true, message = "Deleted with successful"});
        }
    }
}