using LaLocanda.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Domain.Entities
{
    public class IngredientDish : AuditableBaseEntity
    {
        public int DishId { get; set; }
        public int IngredientId { get; set; }
        public Dish JDish { get; set; }
        public Ingredient JIngredient { get; set; }
    }
}
