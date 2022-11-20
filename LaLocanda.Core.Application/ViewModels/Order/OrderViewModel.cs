using LaLocanda.Core.Application.ViewModels.CommonViewModel;
using LaLocanda.Core.Application.ViewModels.Dish;
using LaLocanda.Core.Application.ViewModels.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.ViewModels.Order
{
    public class OrderViewModel : AuditableBaseViewModel
    {
        public int TableId { get; set; }
        public double TotalPrice { get; set; }
        public int Status { get; set; }

        #region Navigation Props
        public TableViewModel Table { get; set; }

        public ICollection<DishViewModel> Dishes { get; set; }
        #endregion
    }
}
