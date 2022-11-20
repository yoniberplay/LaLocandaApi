using LaLocanda.Core.Application.DTOs.User;
using LaLocanda.Core.Application.ViewModels.Dish;
using LaLocanda.Core.Application.ViewModels.Ingredient;
using LaLocanda.Core.Application.ViewModels.IngredientDish;
using LaLocanda.Core.Application.ViewModels.Order;
using LaLocanda.Core.Application.ViewModels.OrderDish;
using LaLocanda.Core.Application.ViewModels.Table;
using LaLocanda.Core.Application.ViewModels.User;
using LaLocanda.Core.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Mappings
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            CreateMap<RegisterResponse, UserViewModel>()
                .ReverseMap()
                .ForMember(d => d.HasError, o => o.Ignore())
                .ForMember(d => d.Error, o => o.Ignore());

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ReverseMap();

            CreateMap<LoginRequest, LoginViewModel>()
                .ReverseMap();

            CreateMap<Dish, SaveDishViewModel>()
                .ForMember(d => d.IngredientIds, o => o.Ignore())
                .ReverseMap()
                .ForMember(d => d.Created, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.Modified, o => o.Ignore())
                .ForMember(d => d.ModifiedBy, o => o.Ignore())
                .ForMember(d => d.Ingredients, o => o.Ignore())
                .ForMember(d => d.IngredientDishes, o => o.Ignore())
                .ForMember(d => d.Orders, o => o.Ignore())
                .ForMember(d => d.OrderDishes, o => o.Ignore())
                ;

            CreateMap<Dish, DishViewModel>()
                .ReverseMap()
                .ForMember(d => d.Created, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.Modified, o => o.Ignore())
                .ForMember(d => d.ModifiedBy, o => o.Ignore())
                .ForMember(d => d.IngredientDishes, o => o.Ignore())
                .ForMember(d => d.OrderDishes, o => o.Ignore())
                ;

            CreateMap<Ingredient, SaveIngredientViewModel>()
                .ReverseMap()
                .ForMember(d => d.Created, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.Modified, o => o.Ignore())
                .ForMember(d => d.ModifiedBy, o => o.Ignore())
                .ForMember(d => d.IngredientDishes, o => o.Ignore())
                .ForMember(d => d.Dishes, o => o.Ignore())
                ;

            CreateMap<Ingredient, IngredientViewModel>()
                .ReverseMap()
                .ForMember(d => d.Created, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.Modified, o => o.Ignore())
                .ForMember(d => d.ModifiedBy, o => o.Ignore())
                .ForMember(d => d.IngredientDishes, o => o.Ignore())
                .ForMember(d => d.Dishes, o => o.Ignore())
                ;

            CreateMap<Order, SaveOrderViewModel>()
                .ForMember(d => d.DishIds, o => o.Ignore())
                .ReverseMap()
                .ForMember(d => d.Created, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.Modified, o => o.Ignore())
                .ForMember(d => d.ModifiedBy, o => o.Ignore())
                .ForMember(d => d.Table, o => o.Ignore())
                .ForMember(d => d.Dishes, o => o.Ignore())
                .ForMember(d => d.OrderDishes, o => o.Ignore())
                ;

            CreateMap<Order, OrderViewModel>()
                .ReverseMap()
                .ForMember(d => d.Created, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.Modified, o => o.Ignore())
                .ForMember(d => d.ModifiedBy, o => o.Ignore())
                .ForMember(d => d.OrderDishes, o => o.Ignore())
                ;

            CreateMap<Table, SaveTableViewModel>()
                .ReverseMap()
                .ForMember(d => d.Created, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.Modified, o => o.Ignore())
                .ForMember(d => d.ModifiedBy, o => o.Ignore())
                .ForMember(d => d.Orders, o => o.Ignore())
                ;

            CreateMap<Table, TableViewModel>()
                .ReverseMap()
                .ForMember(d => d.Created, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.Modified, o => o.Ignore())
                .ForMember(d => d.ModifiedBy, o => o.Ignore())
                ;

            CreateMap<IngredientDish, IngredientDishViewModel>()
                .ReverseMap()
                .ForMember(d => d.Created, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.Modified, o => o.Ignore())
                .ForMember(d => d.ModifiedBy, o => o.Ignore())
                .ForMember(d => d.JIngredient, o => o.Ignore())
                .ForMember(d => d.JDish, o => o.Ignore())
                ;

            CreateMap<OrderDish, OrderDishViewModel>()
                .ReverseMap()
                .ForMember(d => d.Created, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.Modified, o => o.Ignore())
                .ForMember(d => d.ModifiedBy, o => o.Ignore())
                .ForMember(d => d.JOrder, o => o.Ignore())
                .ForMember(d => d.JDish, o => o.Ignore())
                ;
        }
    }
}
