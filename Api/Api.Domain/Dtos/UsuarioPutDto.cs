using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class UsuarioPutDto : BasePutDto
    {
        [StringLength(50, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
        public string DesNome { get; set; }

        public string DesImagem { get; set; }

        [StringLength(20, ErrorMessage = "Senha deve ter no máximo {1} caracteres.")]
        public string DesSenha { get; set; }

        [StringLength(20, ErrorMessage = "Telefone deve ter no máximo {1} caracteres.")]
        public string DesTelefone { get; set; }

        [StringLength(300, ErrorMessage = "Especialidade deve ter no máximo {1} caracteres.")]
        public string DesEspecialidade { get; set; }

        [StringLength(3000, ErrorMessage = "Experiência deve ter no máximo {1} caracteres.")]
        public string DesExperiencia { get; set; }
    }
}
