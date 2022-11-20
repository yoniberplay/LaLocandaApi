using LaLocanda.Core.Application.ViewModels.CommonViewModel;
using LaLocanda.Core.Application.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.ViewModels.Table
{
    public class TableViewModel: AuditableBaseViewModel
    {
        public int MaxDiners { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        #region Navigation Props
        public ICollection<OrderViewModel> Orders { get; set; }
        #endregion
    }
}
