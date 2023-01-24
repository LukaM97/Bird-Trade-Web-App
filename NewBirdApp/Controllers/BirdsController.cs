using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewBirdApp.Models;
using NewBirdApp.Repository.Interfaces;

namespace NewBirdApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        private readonly IBirdRepository _birdsRepository;

        public BirdsController(IBirdRepository birdsRepository)
        {
            _birdsRepository = birdsRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_birdsRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Bird bird = _birdsRepository.GetOne(id);

            if (bird == null)
            {
                return NotFound();
            }
            return Ok(bird);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Bird bird)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _birdsRepository.Create(bird);

            return CreatedAtAction("GetOne", new { id = bird.Id }, bird);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, Bird bird)
        {

            if (id != bird.Id)
            {
                return BadRequest();
            }

            try
            {
                _birdsRepository.Update(bird);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(bird);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Bird bird = _birdsRepository.GetOne(id);
            if (bird == null)
            {
                return NotFound();
            }

            _birdsRepository.Delete(bird);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add/{id}")]
        public IActionResult AddBird(int id)
        {
            return Ok(_birdsRepository.AddBird(id));
        }

        [HttpGet("proba")]
        public IActionResult GetSumOfSoldBirds()
        {
            return Ok(_birdsRepository.GetSumOfSoldBirds());
        }
    }
}
