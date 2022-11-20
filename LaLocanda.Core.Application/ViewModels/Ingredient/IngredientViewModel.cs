using LaLocanda.Core.Application.ViewModels.CommonViewModel;
using LaLocanda.Core.Application.ViewModels.Dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.ViewModels.Ingredient
{
    public class IngredientViewModel : AuditableBaseViewModel
    {
        public string Name { get; set; }
    }
}
