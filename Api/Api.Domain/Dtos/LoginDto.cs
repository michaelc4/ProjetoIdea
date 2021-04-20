using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email é um campo obrigatório para Login.")]
        [EmailAddress(ErrorMessage = "Email em formato inválido.")]
        [StringLength(100, ErrorMessage = "Email deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Senha deve ter no máximo {1} caracteres.")]
        public string Senha { get; set; }

        public string AuthToken { get; set; }

        public string IdToken { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        public string Provider { get; set; }
    }
}
