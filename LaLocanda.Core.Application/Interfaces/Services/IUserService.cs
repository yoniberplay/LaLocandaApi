using LaLocanda.Core.Application.DTOs.User;
using LaLocanda.Core.Application.ViewModels.User;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<RegisterResponse> AddAdmin(SaveUserViewModel saveViewModel);
        Task<RegisterResponse> AddWaiter(SaveUserViewModel saveViewModel);
        Task<LoginResponse> Login(LoginViewModel login);
    }
}