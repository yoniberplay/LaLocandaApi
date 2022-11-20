using LaLocanda.Core.Application.ViewModels.Order;
using LaLocanda.Core.Application.ViewModels.OrderDish;
using LaLocanda.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Interfaces.Services
{
    public interface IOrderService : IGenericService<Order, OrderViewModel, SaveOrderViewModel>
    {
        Task<List<OrderViewModel>> GetOrdersByTable(int tableId);
        Task AddDishToOrder(int orderId, int dishId);
        Task<List<OrderDishViewModel>> GetAllDishesIdsByOrder(int orderId);
        Task DeleteDishFromOrder(int orderId, int dishId);
        Task<OrderViewModel> GetOrderWithDishes(int id);
        Task<List<OrderViewModel>> GetAllWithDishes();
    }
}