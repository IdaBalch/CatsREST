using CatsREST.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatsREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly ICatsRepository _repository;

        public CatsController(ICatsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/cats
        [HttpGet]
        public ActionResult<IEnumerable<Cat>> GetCats(
            string? nameStartsWith = null,
            int? minAge = null,
            string? sortOrder = null)
        {
            try
            {
                return Ok(_repository.GetCats(
                    nameStartsWith,
                    minAge,
                    sortOrder));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/cats/1
        [HttpGet("{id}")]
        public ActionResult<Cat> GetCat(int id)
        {
            Cat? cat = _repository.GetById(id);

            if (cat == null)
            {
                return NotFound();
            }

            return Ok(cat);
        }

        // POST: api/cats
        [HttpPost]
        public ActionResult<Cat> AddCat(Cat cat)
        {
            Cat newCat = _repository.AddCat(cat);

            return CreatedAtAction(
                nameof(GetCat),
                new { id = newCat.Id },
                newCat);
        }

        // PUT: api/cats/1
        [HttpPut("{id}")]
        public ActionResult<Cat> UpdateCat(int id, Cat cat)
        {
            Cat? updatedCat = _repository.UpdateCat(id, cat);

            if (updatedCat == null)
            {
                return NotFound();
            }

            return Ok(updatedCat);
        }

        // DELETE: api/cats/1
        [HttpDelete("{id}")]
        public ActionResult<Cat> DeleteCat(int id)
        {
            Cat? deletedCat = _repository.DeleteById(id);

            if (deletedCat == null)
            {
                return NotFound();
            }

            return Ok(deletedCat);
        }
    }
}
