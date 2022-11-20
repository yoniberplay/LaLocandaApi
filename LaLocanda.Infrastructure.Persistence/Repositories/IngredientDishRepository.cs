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
    public class IngredientDishRepository : GenericRepository<IngredientDish>, IIngredientDishRepository
    {
        private readonly RestaurantContext _dbContext;

        public IngredientDishRepository(RestaurantContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
