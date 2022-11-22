using LaLocanda.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Domain.Entities
{
    public class Ingredient:AuditableBaseEntity
    {
        public string Name { get; set; }

        public ICollection<Dish> Dishes { get; set; }
        public List<IngredientDish> IngredientDishes { get; set; }
    }
}
