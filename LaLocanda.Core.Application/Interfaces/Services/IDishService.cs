using LaLocanda.Core.Application.DTOs.User;
using LaLocanda.Core.Application.ViewModels.Dish;
using LaLocanda.Core.Application.ViewModels.Ingredient;
using LaLocanda.Core.Application.ViewModels.IngredientDish;
using LaLocanda.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Interfaces.Services
{
    public interface IDishService : IGenericService<Dish, DishViewModel, SaveDishViewModel>
    {
        Task DeleteIngredientFromDish(int ingId, int dishId);
        Task AddIngredientToDish(int dishId, int ingId);
        Task<DishViewModel> GetDishWithIngredients(int id);
        Task<List<DishViewModel>> GetAllWithIngredients();
        Task<List<IngredientDishViewModel>> GetAllIngredientIdsByDish(int dishId);
        Task<double> GetPriceById(int id);
    }
}