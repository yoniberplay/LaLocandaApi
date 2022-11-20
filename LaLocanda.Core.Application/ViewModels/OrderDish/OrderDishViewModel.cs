using LaLocanda.Core.Application.ViewModels.CommonViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.ViewModels.OrderDish
{
    public class OrderDishViewModel : AuditableBaseViewModel
    {
        public int DishId { get; set; }
        public int OrderId { get; set; }
    }
}
