using System.ComponentModel.DataAnnotations;

namespace SE.WebApp.MVC.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; } = null!;
    }

    public class UserLogin
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string? Email { get; set; } = null!;

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string? Password { get; set; } = null!;
    }

    public class UserResponseLogin
    {
        public string AcessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken userToken { get; set; }
    }

    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaims> userClaims { get; set; }
    }

    public class UserClaims
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
