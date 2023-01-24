using Microsoft.EntityFrameworkCore;
using NewBirdApp.Models;
using NewBirdApp.Models.DTO;
using NewBirdApp.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace NewBirdApp.Repository
{
    public class BirdRepository : IBirdRepository
    {
        private readonly AppDbContext _context;

        public BirdRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Bird> GetAll()
        {
            return _context.Birds.Include(g => g.Gender).OrderBy(b => b.Name);
        }

        public Bird GetOne(int id)
        {
            return _context.Birds.FirstOrDefault(b => b.Id == id);
        }

        public void Create(Bird bird)
        {
            _context.Birds.Add(bird);
            _context.SaveChanges();
        }

        public void Update(Bird bird)
        {
            _context.Entry(bird).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Bird bird)
        {
            _context.Remove(bird);
            _context.SaveChanges();
        }

        public int AddBird(int id)
        {
            GetOne(id).Quantity += 1;
            _context.SaveChanges();
            return GetOne(id).Quantity;
        }

        public IEnumerable<BirdsSumDTO> GetSumOfSoldBirds()
        {

            var lista = _context.Birds.GroupBy(b => b.Id)
                .Select(r => new BirdsSumDTO
                {
                    BirdId = r.Key,
                    Bird = _context.Birds.Where(k => k.Id == r.Key).Select(k => k.Name).Single(),
                    SumOfSoldBirds = _context.Orders.Where(p => p.BirdId == r.Key).Select(p => p.Amount).Sum()
                }).ToList().OrderBy(r => r.Bird);

            return lista;
        }
    }
}
