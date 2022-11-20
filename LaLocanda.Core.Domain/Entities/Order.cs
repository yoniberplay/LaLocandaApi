using LaLocanda.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Domain.Entities
{
    public class Order : AuditableBaseEntity
    {
        public int TableId { get; set; }
        public double TotalPrice { get; set; }
        public int Status { get; set; }

        #region Navigation Props
        public Table Table { get; set; }

        public ICollection<Dish> Dishes { get; set; }
        public List<OrderDish> OrderDishes { get; set; }
        #endregion
    }
}
