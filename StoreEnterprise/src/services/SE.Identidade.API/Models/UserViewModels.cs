
using Microsoft.AspNetCore.Identity;

namespace SE.Identidade.API.Models
{
    public class UserRegister : IdentityUser
    {
        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        //public string Email { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        //public string Password { get; set; }

        //[Compare("Password", ErrorMessage = "As senha não conferem")]
        //public string ConfirmPassword { get; set; }
    }

    //public record UserLogin
    //{
    //    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    //    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    //    public string Email { get; set; }

    //    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    //    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    //    public string Password { get; set; }
    //}
}
