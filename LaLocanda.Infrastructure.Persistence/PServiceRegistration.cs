using LaLocanda.Core.Application.Interfaces.Repositories;
using LaLocanda.Infrastructure.Persistence.Contexts;
using LaLocanda.Infrastructure.Persistence.Repositories;
using LaLocandaApi.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Infrastructure.Persistence
{
    public static class PServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RestaurantContext>(
                    options => options.UseSqlServer(
                        configuration.GetConnectionString("ApiConnection"),
                        m => m.MigrationsAssembly(typeof(RestaurantContext).Assembly.FullName)));

            //dependency injections
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IDishRepository, DishRepository>();
            services.AddTransient<IIngredientRepository, IngredientRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ITableRepository, TableRepository>();
            services.AddTransient<IIngredientDishRepository, IngredientDishRepository>();
            services.AddTransient<IOrderDishRepository, OrderDishRepository>();
        }
    }
}
