using inventory_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace inventory_api.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> RegisterUser(UserRegister req);
        Task<User> LoginUser(LoginDTO req);

    }
}
