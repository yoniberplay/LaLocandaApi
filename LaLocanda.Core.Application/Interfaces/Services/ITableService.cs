using LaLocanda.Core.Application.DTOs.User;
using LaLocanda.Core.Application.ViewModels.Dish;
using LaLocanda.Core.Application.ViewModels.Table;
using LaLocanda.Core.Domain.Entities;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Interfaces.Services
{
    public interface ITableService : IGenericService<Table, TableViewModel, SaveTableViewModel>
    {
        Task ChangeTableStatus(int tableId, int status);
    }
}