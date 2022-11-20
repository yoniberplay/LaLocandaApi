using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Interfaces.Services
{
    public interface IGenericService<T,VM,SaveVM>
        where T : class
        where VM : class
        where SaveVM:class
    {
        Task<SaveVM> Add(SaveVM saveVM);
        Task Update(SaveVM saveVM, int id);
        Task Delete(int id);
        Task<List<VM>> GetAllViewModel();
        Task<SaveVM> GetByIdSaveViewModel(int id);
        Task<VM> GetByIdViewModel(int id);
    }
}
