using LaLocanda.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Domain.Entities
{
    public class Dish:AuditableBaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int CantPeople { get; set; }
        public int Category { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
        public List<IngredientDish> IngredientDishes { get; set; }

        public ICollection<Order> Orders { get; set; }
        public List<OrderDish> OrderDishes { get; set; }
    }
}
