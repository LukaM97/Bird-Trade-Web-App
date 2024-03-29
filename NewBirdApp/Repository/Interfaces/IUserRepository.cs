﻿using NewBirdApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewBirdApp.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        void Update(AppUser user);

    }
}
