using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewBirdApp.Models;
using NewBirdApp.Models.DTO;
using NewBirdApp.Repository.Interfaces;

namespace NewBirdApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBirdRepository _birdRepository;

        public OrdersController(IOrderRepository orderRepository, IBirdRepository birdRepository)
        {
            _orderRepository = orderRepository;
            _birdRepository = birdRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_orderRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Order order = _orderRepository.GetOne(id);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Bird b = _birdRepository.GetOne(order.BirdId);
            if (b.Id == order.BirdId)
            {
                if (b.Quantity == 0 || (b.Quantity - order.Amount) < 1)
                {
                    return BadRequest("There are only " + b.Quantity + " birds left.");
                }
            }

            _orderRepository.Create(order);
            Bird newBird = _birdRepository.GetOne(order.BirdId);
            newBird.Quantity = newBird.Quantity - order.Amount;
            return CreatedAtAction("GetOne", new { id = order.Id }, order);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Order order = _orderRepository.GetOne(id);
            if (order == null)
            {
                return NotFound();
            }

            _orderRepository.Delete(order);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [Route("/api/byName")]
        [HttpPost]
        public IActionResult FilterByName(OrderNameFilterDTO dto)
        {
            return Ok(_orderRepository.FilterByName(dto));
        }

        [Authorize(Roles = "Admin")]
        [Route("/api/byCity")]
        [HttpPost]
        public IActionResult FilterByCity(OrderCityFilterDTO dto)
        {
            return Ok(_orderRepository.FilterByCity(dto));
        }

        [Authorize(Roles = "Admin")]
        [Route("/api/byAmount")]
        [HttpPost]
        public IActionResult FilterByAmount(OrderAmountFilterDTO dto)
        {
            if(dto.Min > dto.Max)
            {
                return BadRequest();
            }
            return Ok(_orderRepository.FilterByAmount(dto));
        }
    }
}
