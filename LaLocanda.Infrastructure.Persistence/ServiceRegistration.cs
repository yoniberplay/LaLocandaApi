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
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LaLocandaContext>(
                    options => options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        m => m.MigrationsAssembly(typeof(LaLocandaContext).Assembly.FullName)));

            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<LaLocandaContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<LaLocandaContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(LaLocandaContext).Assembly.FullName)));
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IDishRepository, DishRepository>();
            services.AddTransient<IIngredientRepository, IngredientRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ITableRepository, TableRepository>();
            services.AddTransient<IIngredientDishRepository, IngredientDishRepository>();
            services.AddTransient<IOrderDishRepository, OrderDishRepository>();
            #endregion
        }
    }
}
