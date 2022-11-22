using LaLocanda.Core.Application.DTOs.User;
using LaLocanda.Core.Application.ViewModels.User;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<RegisterResponse> AddAdmin(SaveUserViewModel saveViewModel);
        Task<RegisterResponse> AddBasic(SaveUserViewModel saveViewModel);
        Task<LoginResponse> Loggin(LoginViewModel login);
    }
}