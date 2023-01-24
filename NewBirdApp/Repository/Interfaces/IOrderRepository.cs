using NewBirdApp.Models;
using NewBirdApp.Models.DTO;
using System.Linq;

namespace NewBirdApp.Repository.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetAll();
        Order GetOne(int id);
        void Create(Order order);
        void Update(Order order);
        void Delete(Order order);
        IQueryable<Order> FilterByName(OrderNameFilterDTO dto);
        IQueryable<Order> FilterByCity(OrderCityFilterDTO dto);
        IQueryable<Order> FilterByAmount(OrderAmountFilterDTO dto);
    }
}
