using LaLocanda.Core.Application.Interfaces.Repositories;
using LaLocanda.Core.Application.Interfaces.Services;
using LaLocanda.Core.Application.ViewModels.Dish;
using LaLocanda.Core.Application.ViewModels.Ingredient;
using LaLocanda.Core.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Services
{
    public class IngredientService : GenericService<Ingredient, IngredientViewModel, SaveIngredientViewModel>, IIngredientService
    {
        private readonly IIngredientRepository _ingRepository;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientRepository ingRepository, IMapper mapper) : base(ingRepository, mapper)
        {
            _ingRepository = ingRepository;
            _mapper = mapper;
        }
    }
}
