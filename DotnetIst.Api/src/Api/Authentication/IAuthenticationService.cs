using Api.Models;

namespace Api.Authentication
{
    public interface IAuthenticationService
    {
        string GetToken(Customer customer);
    }
}