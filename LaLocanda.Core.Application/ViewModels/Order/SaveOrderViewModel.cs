using LaLocanda.Core.Application.ViewModels.CommonViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.ViewModels.Order
{
    public class SaveOrderViewModel : AuditableBaseViewModel
    {
        [JsonIgnore]
        public override int Id { get; set; }

        [Required(ErrorMessage ="Debe proporcionar el Id de la mesa donde se hizo la orden")]
        public int TableId { get; set; }
        public List<int> DishIds { get; set; }

        [JsonIgnore]
        public double TotalPrice { get; set; }

        [JsonIgnore]
        public int Status { get; set; }
    }
}
