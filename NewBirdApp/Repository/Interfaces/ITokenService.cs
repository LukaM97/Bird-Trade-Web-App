using NewBirdApp.Models;
using System.Threading.Tasks;

namespace NewBirdApp.Repository.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
