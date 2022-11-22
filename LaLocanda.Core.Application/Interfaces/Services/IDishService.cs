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
        Task<DishViewModel> GetDish(int id);
        Task DeleteOnDish(int ingId, int dishId);
        Task AddToDish(int dishId, int ingId);
        Task<List<IngredientDishViewModel>> GetAllIngreDish(int dishId);
        Task<List<DishViewModel>> GetAll();
        Task<double> GetPriceById(int id);
    }
}