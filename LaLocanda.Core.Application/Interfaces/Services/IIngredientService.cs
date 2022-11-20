using LaLocanda.Core.Application.DTOs.User;
using LaLocanda.Core.Application.ViewModels.Dish;
using LaLocanda.Core.Application.ViewModels.Ingredient;
using LaLocanda.Core.Domain.Entities;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Interfaces.Services
{
    public interface IIngredientService : IGenericService<Ingredient, IngredientViewModel, SaveIngredientViewModel>
    {
        
    }
}