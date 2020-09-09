using System.ComponentModel.DataAnnotations;

namespace Sklep.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Podanie loginu jest wymagane")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Podanie hasła jest wymagane")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
         ErrorMessage = "Hasło musi zawierać conajmniej 8 znaków, wielką i małą literę, cyfrę i znak specjalny")]
        public string Password { get; set; }
    }
}
