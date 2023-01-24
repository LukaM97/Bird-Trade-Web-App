using NewBirdApp.Models;
using NewBirdApp.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace NewBirdApp.Repository.Interfaces
{
    public interface IBirdRepository
    {
        IQueryable<Bird> GetAll();
        Bird GetOne(int id);
        void Create(Bird bird);
        void Update(Bird bird);
        void Delete(Bird bird);
        public int AddBird(int id);
        IEnumerable<BirdsSumDTO> GetSumOfSoldBirds();
    }
}
