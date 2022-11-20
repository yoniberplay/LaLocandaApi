using LaLocanda.Core.Application.ViewModels.CommonViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.ViewModels.User
{
    public class SaveUserViewModel : AuditableBaseViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage ="Debe proporcionar un nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Debe proporcionar un apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Debe proporcionar un email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Debe proporcionar un nombre de usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Debe proporcionar una contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Debe repetir la contraseña")]
        [Compare(nameof(Password), ErrorMessage ="Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
    }
}
