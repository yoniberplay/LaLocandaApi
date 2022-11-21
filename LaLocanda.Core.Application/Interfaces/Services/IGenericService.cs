using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Interfaces.Services
{
    public interface IGenericService<Entidad,ViewModel,SaveViewModel>
        where Entidad : class
        where ViewModel : class
        where SaveViewModel : class
    {
        Task<SaveViewModel> Add(SaveViewModel saveVM);
        Task Update(SaveViewModel saveVM, int id);
        Task Delete(int id);
        Task<List<ViewModel>> GetAllViewModel();
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task<ViewModel> GetByIdViewModel(int id);
    }
}
