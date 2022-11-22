using LaLocanda.Core.Application.Interfaces.Repositories;
using LaLocanda.Core.Application.Interfaces.Services;
using LaLocanda.Core.Application.ViewModels.Dish;
using LaLocanda.Core.Application.ViewModels.Table;
using LaLocanda.Core.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Services
{
    public class TableService : GenericService<Table, TableViewModel, SaveTableViewModel>, ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public TableService(ITableRepository tableRepository, IMapper mapper) : base(tableRepository, mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public async Task ChangeStatus(int tableId, int status)
        {
            Table t=await _tableRepository.GetByIdAsync(tableId);
            t.Status = status;
            await _tableRepository.UpdateAsync(t, tableId);
        }
    }
}
