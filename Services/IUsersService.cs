using System.Threading.Tasks;
using WebApplication.Contracts;

namespace WebApplication.Services
{
    public interface IUsersService
    {
        public Task<AuthenticationResult> Register(string email, string password, string name);
    }
}