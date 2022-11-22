using LaLocanda.Core.Application.Interfaces.Services;
using LaLocanda.Core.Application.ViewModels.Dish;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaLocandaApi.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DishController : BaseApiController
    {
        private readonly IDishService _dishService;
        private readonly IIngredientService _ingService;

        public DishController(IDishService dishService,IIngredientService ingredientService)
        {
            _dishService = dishService;
            _ingService = ingredientService;
        }

        
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SaveDishViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(vm);
                }

                if (vm.IngredientIds.Count == 0)
                {
                    ModelState.AddModelError("zeroIngredients", "Debes añadir al menos un ingrediente");
                    return BadRequest(ModelState);
                }

                foreach(var id in vm.IngredientIds)
                {
                    var ingredient = await _ingService.GetByIdSaveViewModel(id);
                    if (ingredient == null)
                    {
                        ModelState.AddModelError("ingredientNotExists", $"No existe un ingrediente con el id {id}");
                        return BadRequest(ModelState);
                    }
                }

                var dish = await _dishService.Add(vm);
                if (dish == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, dish);
                }

                foreach (var id in vm.IngredientIds)
                {
                    await _dishService.AddToDish(dish.Id, id);
                }               

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, SaveDishViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(vm);
                }

                var dish = await _dishService.GetByIdViewModel(id);
                if (dish == null)
                {
                    ModelState.AddModelError("dishNotExists", $"No existe un plato con el id {id}");
                    return BadRequest(ModelState);
                }

                if (vm.IngredientIds.Count == 0)
                {
                    ModelState.AddModelError("zeroIngredients", "Debes añadir al menos un ingrediente");
                    return BadRequest(ModelState);
                }

                foreach (var ingId in vm.IngredientIds)
                {
                    var ingredient = await _ingService.GetByIdSaveViewModel(ingId);
                    if (ingredient == null)
                    {
                        ModelState.AddModelError("ingredientNotExist", $"No existe un ingrediente con el id {ingId}");
                        return BadRequest(ModelState);
                    }
                }

                List<int> forAdd = new();
                List<int> forDelete = new();

                var ingsByDish = await _dishService.GetAllIngreDish(id);

                foreach (int ingId in vm.IngredientIds)
                {
                    if (!ingsByDish.Any(i => i.IngredientId == ingId))
                        forAdd.Add(ingId);
                }

                foreach (var ing in ingsByDish)
                {
                    if (!vm.IngredientIds.Contains(ing.IngredientId))
                        forDelete.Add(ing.IngredientId);
                }

                foreach (var del in forDelete)
                {
                    await _dishService.DeleteOnDish(del, id);
                }

                vm.Id = id;
                await _dishService.Update(vm, id);

                foreach (var add in forAdd)
                {
                    await _dishService.AddToDish(id, add);
                }

                return Ok(await _dishService.GetDish(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
        [HttpGet("List")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<DishViewModel> dishes = await _dishService.GetAll();

                if (dishes.Count == 0)
                    return NotFound();

                return Ok(dishes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var dish = await _dishService.GetByIdSaveViewModel(id);

                if (dish == null)
                    return NotFound();

                return Ok(await _dishService.GetDish(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
