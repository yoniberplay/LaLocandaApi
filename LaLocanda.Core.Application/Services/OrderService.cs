using LaLocanda.Core.Application.Interfaces.Repositories;
using LaLocanda.Core.Application.Interfaces.Services;
using LaLocanda.Core.Application.ViewModels.Order;
using LaLocanda.Core.Application.ViewModels.OrderDish;
using LaLocanda.Core.Domain.Entities;
using AutoMapper;


namespace LaLocanda.Core.Application.Services
{
    public class OrderService : GenericService<Order, OrderViewModel, SaveOrderViewModel>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDishRepository _orderdishRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IOrderDishRepository orderdishRepository) : base(orderRepository, mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderdishRepository = orderdishRepository;
        }

        public async Task<List<OrderViewModel>> GetOrdersByTable(int tableId)
        {
            var orders = await _orderRepository.GetAllAsync();
            var ordersByTable = orders.FindAll(o => o.TableId == tableId);
            return _mapper.Map<List<OrderViewModel>>(ordersByTable);
        }

        public async Task<List<OrderDishViewModel>> GetAllDishesIdsByOrder(int orderId)
        {
            var dishes = await _orderdishRepository.GetAllAsync();
            var dishByOrder = dishes.FindAll(i => i.OrderId == orderId);
            return _mapper.Map<List<OrderDishViewModel>>(dishByOrder);
        }

        public async Task<OrderViewModel> GetOrderWithDishes(int id)
        {
            var order = await _orderRepository.GetByIdWithIncludeAsync(id, new List<string>(), new List<string> { "Dishes" });
            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<List<OrderViewModel>> GetAllWithDishes()
        {
            var orders = await _orderRepository.GetAllWithIncludesAsync(new List<string> { "Dishes" });
            return _mapper.Map<List<OrderViewModel>>(orders);
        }

        public async Task AddDishToOrder(int orderId, int dishId)
        {
            OrderDish orderDish = new()
            {
                OrderId = orderId,
                DishId = dishId
            };
            await _orderdishRepository.AddAsync(orderDish);
        }

        public async Task DeleteDishFromOrder(int orderId, int dishId)
        {
            OrderDish orderDish = new()
            {
                OrderId = orderId,
                DishId = dishId
            };

            await _orderdishRepository.DeleteAsync(orderDish);
        }
    }
}
