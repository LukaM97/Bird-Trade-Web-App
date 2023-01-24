using Microsoft.EntityFrameworkCore;
using NewBirdApp.Repository.Interfaces;
using NewBirdApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewBirdApp.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
