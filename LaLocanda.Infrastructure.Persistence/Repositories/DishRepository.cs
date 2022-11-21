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
    public class DishRepository : GenericRepository<Dish>, IDishRepository
    {
        private readonly LaLocandaContext _dbContext;

        public DishRepository(LaLocandaContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
