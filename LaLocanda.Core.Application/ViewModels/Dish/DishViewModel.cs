using LaLocanda.Core.Application.Enums;
using LaLocanda.Core.Application.ViewModels.CommonViewModel;
using LaLocanda.Core.Application.ViewModels.Ingredient;
using LaLocanda.Core.Application.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.ViewModels.Dish
{
    public class DishViewModel: AuditableBaseViewModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int People { get; set; }
        public int Category { get; set; }

        #region Navigation Props
        public ICollection<IngredientViewModel> Ingredients { get; set; }
        public ICollection<OrderViewModel> Orders { get; set; }
        #endregion
    }
}
