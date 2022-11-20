using LaLocanda.Core.Application.ViewModels.CommonViewModel;
using LaLocanda.Core.Application.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.ViewModels.Table
{
    public class SaveTableViewModel : AuditableBaseViewModel
    {
        [JsonIgnore]
        public override int Id { get; set; }

        [Required(ErrorMessage ="Debe proporcionar la cantidad maxima de personas que acepta la mesa")]
        public int MaxDiners { get; set; }
        
        [Required(ErrorMessage ="Debe proporcionar una descripcion a la mesa")]
        public string Description { get; set; }

        [JsonIgnore]
        public int Status { get; set; }
    }
}
