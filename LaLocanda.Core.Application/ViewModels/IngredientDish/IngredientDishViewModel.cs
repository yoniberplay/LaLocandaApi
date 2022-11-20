using LaLocanda.Core.Application.ViewModels.CommonViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.ViewModels.IngredientDish
{
    public class IngredientDishViewModel : AuditableBaseViewModel
    {
        public int DishId { get; set; }
        public int IngredientId { get; set; }
    }
}
