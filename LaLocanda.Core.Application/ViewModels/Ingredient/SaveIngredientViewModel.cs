using LaLocanda.Core.Application.ViewModels.CommonViewModel;
using LaLocanda.Core.Application.ViewModels.Dish;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.ViewModels.Ingredient
{
    public class SaveIngredientViewModel : AuditableBaseViewModel
    {
        [JsonIgnore]
        public override int Id { get; set; }

        [Required(ErrorMessage = "Debe proporcionar un nombre para el ingrediente")]
        public string Name { get; set; }
    }
}
