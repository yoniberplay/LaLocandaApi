using LaLocanda.Core.Application.Interfaces.Repositories;
using LaLocanda.Core.Application.Interfaces.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Services
{
    public class GenericService<T, VM, SaveVM> : IGenericService<T, VM, SaveVM>
        where T : class
        where VM : class
        where SaveVM : class
    {
        private readonly IGenericRepository<T> _repo;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<T> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public virtual async Task<SaveVM> Add(SaveVM saveVM)
        {
            T t = _mapper.Map<T>(saveVM);
            t = await _repo.AddAsync(t);
            SaveVM vm = _mapper.Map<SaveVM>(t);

            return vm;
        }

        public virtual async Task Update(SaveVM saveVM, int id)
        {
            T t = _mapper.Map<T>(saveVM);
            await _repo.UpdateAsync(t, id);
        }

        public virtual async Task<List<VM>> GetAllViewModel()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<VM>>(entities);
        }

        public virtual async Task<SaveVM> GetByIdSaveViewModel(int id)
        {
            T t = await _repo.GetByIdAsync(id);
            SaveVM saveVM = _mapper.Map<SaveVM>(t);
            return saveVM;
        }

        public virtual async Task<VM> GetByIdViewModel(int id)
        {
            T t = await _repo.GetByIdAsync(id);
            VM vm = _mapper.Map<VM>(t);
            return vm;
        }

        public virtual async Task Delete(int id)
        {
            T t = await _repo.GetByIdAsync(id);
            await _repo.DeleteAsync(t);
        }
    }
}
