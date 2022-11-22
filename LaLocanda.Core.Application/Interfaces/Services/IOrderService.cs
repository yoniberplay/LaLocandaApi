using LaLocanda.Core.Application.ViewModels.Order;
using LaLocanda.Core.Application.ViewModels.OrderDish;
using LaLocanda.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Interfaces.Services
{
    public interface IOrderService : IGenericService<Order, OrderViewModel, SaveOrderViewModel>
    {
        Task<OrderViewModel> GetOrderWithDishes(int id);
        Task<List<OrderDishViewModel>> GetAllDishes(int orderId);
        Task DropDishOrder(int orderId, int dishId);
        Task<List<OrderViewModel>> GetOrders(int tableId);
        Task AddDish(int orderId, int dishId);
        Task<List<OrderViewModel>> GetAllWithDishes();
    }
}