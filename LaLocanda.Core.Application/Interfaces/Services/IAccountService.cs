using LaLocanda.Core.Application.DTOs.User;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RegisterResponse> RegisterAdminAsync(RegisterRequest request);
        Task<RegisterResponse> RegisterBasicAsync(RegisterRequest request);
    }
}