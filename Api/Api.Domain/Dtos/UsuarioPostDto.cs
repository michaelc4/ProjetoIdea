using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class UsuarioPostDto
    {
        [StringLength(50, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
        public string DesNome { get; set; }

        public string DesImagem { get; set; }

        [Required(ErrorMessage = "Email é um campo obrigatório.")]
        [EmailAddress(ErrorMessage = "Email em formato inválido.")]
        [StringLength(100, ErrorMessage = "Email deve ter no máximo {1} caracteres.")]
        public string DesEmail { get; set; }

        [Required(ErrorMessage = "Senha é um campo obrigatório.")]
        [StringLength(20, ErrorMessage = "Senha deve ter no máximo {1} caracteres.")]
        public string DesSenha { get; set; }

        [StringLength(20, ErrorMessage = "Telefone deve ter no máximo {1} caracteres.")]
        public string DesTelefone { get; set; }
    }
}
