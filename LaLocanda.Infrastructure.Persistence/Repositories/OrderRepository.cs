using LaLocanda.Infrastructure.Persistence.Contexts;
using LaLocanda.Core.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaLocanda.Core.Domain.Entities;

namespace LaLocanda.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly LaLocandaContext _dbContext;

        public OrderRepository(LaLocandaContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
