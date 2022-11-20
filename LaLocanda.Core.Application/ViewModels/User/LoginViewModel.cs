using LaLocanda.Core.Application.ViewModels.CommonViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.ViewModels.User
{
    public class LoginViewModel : AuditableBaseViewModel
    {
        [Required(ErrorMessage ="Debe proporcionar un nombre de usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Debe proporcionar una contraseña")]
        public string Password { get; set; }
    }
}
