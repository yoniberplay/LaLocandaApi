using LaLocanda.Core.Application.Interfaces.Services;
using LaLocanda.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Dependency Injection

            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IDishService, DishService>();
            services.AddTransient<IIngredientService, IngredientService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
