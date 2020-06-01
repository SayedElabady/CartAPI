namespace WebApplication.Contracts.Requests
{
    public class RegistrationUserRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
    }
}