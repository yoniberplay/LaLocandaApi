using LaLocanda.Core.Application.Interfaces.Repositories;
using LaLocanda.Core.Application.Interfaces.Services;
using LaLocanda.Core.Application.ViewModels.Dish;
using LaLocanda.Core.Application.ViewModels.Ingredient;
using LaLocanda.Core.Application.ViewModels.IngredientDish;
using LaLocanda.Core.Domain.Entities;
using AutoMapper;


namespace LaLocanda.Core.Application.Services
{
    public class DishService : GenericService<Dish, DishViewModel, SaveDishViewModel>, IDishService
    {
        private readonly IDishRepository _dishRepository;
        private readonly IIngredientDishRepository _ingdishRepository;
        private readonly IMapper _mapper;

        public DishService(IDishRepository dishRepository, IMapper mapper, IIngredientDishRepository ingdishRepository) : base(dishRepository, mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
            _ingdishRepository = ingdishRepository;
        }

        public async Task<List<DishViewModel>> GetAll()
        {
            var dishes = await _dishRepository.GetAllWithIncludesAsync(new List<string> { "Ingredients" });
            return _mapper.Map<List<DishViewModel>>(dishes);
        }

        public async Task<DishViewModel> GetDish(int id)
        {
            var dish = await _dishRepository.GetByIdWithIncludeAsync(id, new List<string>(), new List<string> { "Ingredients" });
            return _mapper.Map<DishViewModel>(dish);
        }

        public async Task AddToDish(int dishId, int ingId)
        {
            IngredientDish ingredientDish = new()
            {
                DishId = dishId,
                IngredientId = ingId
            };
            await _ingdishRepository.AddAsync(ingredientDish);
        }

        public async Task DeleteOnDish(int ingId, int dishId)
        {
            IngredientDish ingDish = new()
            {
                IngredientId = ingId,
                DishId = dishId
            };

            await _ingdishRepository.DeleteAsync(ingDish);
        }

        public async Task<List<IngredientDishViewModel>> GetAllIngreDish(int dishId)
        {
            var ingredients = await _ingdishRepository.GetAllAsync();
            var ingByDish = ingredients.FindAll(i => i.DishId == dishId);
            return _mapper.Map<List<IngredientDishViewModel>>(ingByDish);
        }

        public async Task<double> GetPriceById(int id)
        {
            var dish = await _dishRepository.GetByIdAsync(id);
            return dish.Price;
        }
    }
}
