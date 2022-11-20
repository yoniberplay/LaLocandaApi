using LaLocanda.Core.Application.Enums;
using LaLocanda.Core.Application.Interfaces.Services;
using LaLocanda.Core.Application.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaLocandaApi.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles ="Waiter,SuperAdmin")]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IDishService _dishService;

        public OrderController(IOrderService orderService, IDishService dishService)
        {
            _orderService = orderService;
            _dishService = dishService;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SaveOrderViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(vm);
                }

                if (vm.DishIds.Count == 0)
                {
                    ModelState.AddModelError("zerDishes", "Debes añadir al menos un plato");
                    return BadRequest(ModelState);
                }

                foreach (var id in vm.DishIds)
                {
                    var dish = await _dishService.GetByIdSaveViewModel(id);
                    if (dish == null)
                    {
                        ModelState.AddModelError("dishNotExists", $"No existe un platillo con el id {id}");
                        return BadRequest(ModelState);
                    }
                }

                double subTotal = 0;
                foreach (var id in vm.DishIds)
                {
                    var dish = await _dishService.GetByIdViewModel(id);
                    subTotal += dish.Price;
                }

                vm.TotalPrice = subTotal;
                vm.Status = (int)OrderStatus.InProcess;
                var order = await _orderService.Add(vm);

                foreach (var id in vm.DishIds)
                {
                    await _orderService.AddDishToOrder(order.Id, id);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, SaveOrderViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(vm);
                }

                var order = await _orderService.GetByIdViewModel(id);
                if (order == null)
                {
                    ModelState.AddModelError("orderNotExists", $"No existe una orden con el id {id}");
                    return BadRequest(ModelState);
                }

                if (vm.DishIds.Count == 0)
                {
                    ModelState.AddModelError("zeroDishes", "Debes añadir al menos un platillo");
                    return BadRequest(ModelState);
                }

                foreach (var dishId in vm.DishIds)
                {
                    var dish = await _dishService.GetByIdSaveViewModel(dishId);
                    if (dish == null)
                    {
                        ModelState.AddModelError("dishNotExists", $"No existe un platillo con el id {dishId}");
                        return BadRequest(ModelState);
                    }
                }

                List<int> forAdd = new();
                List<int> forDelete = new();
                double amountToAdd = 0;
                double amountToSubstract = 0;

                var dishByOrder = await _orderService.GetAllDishesIdsByOrder(id);

                foreach (int dishId in vm.DishIds)
                {
                    if (!dishByOrder.Any(i => i.DishId == dishId))
                    {
                        forAdd.Add(dishId);
                        amountToAdd += await _dishService.GetPriceById(dishId);
                    }
                }

                foreach (var dish in dishByOrder)
                {
                    if (!vm.DishIds.Contains(dish.DishId))
                    {
                        forDelete.Add(dish.DishId);
                        amountToSubstract += await _dishService.GetPriceById(dish.DishId);
                    }
                }

                foreach (var del in forDelete)
                {
                    await _orderService.DeleteDishFromOrder(id, del);
                }

                vm.TotalPrice += amountToAdd;
                vm.TotalPrice -= amountToSubstract;

                vm.Id = id;
                await _orderService.Update(vm, id);

                foreach (var add in forAdd)
                {
                    await _orderService.AddDishToOrder(id, add);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            } 
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet("List")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<OrderViewModel> orders = await _orderService.GetAllWithDishes();

                if (orders.Count == 0)
                    return NotFound();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var order = await _orderService.GetByIdSaveViewModel(id);

                if (order == null)
                    return NotFound();

                return Ok(await _orderService.GetOrderWithDishes(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var order = await _orderService.GetByIdSaveViewModel(id);

                if (order == null)
                    return NotFound();

                await _orderService.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
