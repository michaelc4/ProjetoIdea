using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Presenters
{
    public class IdeiaAnexoPostDto
    {
        public Guid IdeiaId { get; set; }

        public string DesAnexo { get; set; }

        [StringLength(1, ErrorMessage = "Tipo arquivo deve ter no máximo {1} caracteres.")]
        public string IndTipoArquivo { get; set; }

        [StringLength(50, ErrorMessage = "Nome original deve ter no máximo {1} caracteres.")]
        public string DesNomeOriginal { get; set; }
    }
}
