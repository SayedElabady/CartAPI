using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts.Requests;
using WebApplication.Contracts.Response;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationUserRequest request)
        {
            var authResult = await _usersService.Register(request.Email, request.Password, request.Name);
            if (!authResult.Success)
            {
                return new BadRequestResult();
            }

            return Ok(new RegistrationResponse
            {
                Token = authResult.Token
            });
        }
    }
}