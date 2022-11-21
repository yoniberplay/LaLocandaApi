using LaLocanda.Infrastructure.Persistence.Contexts;
using LaLocanda.Core.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaLocanda.Core.Domain.Entities;
using LaLocanda.Infrastructure.Persistence.Repositories;

namespace LaLocandaApi.Infrastructure.Persistence.Repositories
{
    public class TableRepository : GenericRepository<Table>, ITableRepository
    {
        private readonly LaLocandaContext _dbContext;

        public TableRepository(LaLocandaContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
