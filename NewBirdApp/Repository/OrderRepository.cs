using Microsoft.EntityFrameworkCore;
using NewBirdApp.Models;
using NewBirdApp.Models.DTO;
using NewBirdApp.Repository.Interfaces;
using System.Linq;

namespace NewBirdApp.Repository
{
    public class OrderRepository:IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            this._context = context;
        }

        public IQueryable<Order> GetAll()
        {
            return _context.Orders.Include(o => o.Bird).OrderBy(o => o.SurnameOfCustomer).ThenBy(o => o.NameOfCustomer);
        }

        public Order GetOne(int id)
        {
            return _context.Orders.FirstOrDefault(b => b.Id == id);
        }

        public void Create(Order order)
        {
            _context.Orders.Add(order);
            foreach (Bird b in _context.Birds)
            {
                if (b.Id == order.BirdId)
                {
                    b.Quantity = b.Quantity - order.Amount;
                }
            }
            _context.SaveChanges();
        }

        public void Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Order order)
        {
            _context.Remove(order);
            _context.SaveChanges();
        }

        public IQueryable<Order> FilterByName(OrderNameFilterDTO dto)
        {
            return _context.Orders.Include(o => o.Bird).Where(o => o.NameOfCustomer == dto.NameOfCustomer).OrderBy(o => o.Amount);
        }

        public IQueryable<Order> FilterByCity(OrderCityFilterDTO dto)
        {
            return _context.Orders.Include(o => o.Bird).Where(o => o.CustomerCity == dto.CustomerCity).OrderBy(o => o.Amount);
        }

        public IQueryable<Order> FilterByAmount(OrderAmountFilterDTO dto)
        {
            return _context.Orders.Include(o => o.Bird).Where(o => o.Amount >= dto.Min && o.Amount <= dto.Max).OrderBy(o => o.Amount);
        }
    }
}
