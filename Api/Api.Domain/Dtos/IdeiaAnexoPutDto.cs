using Api.Domain.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class IdeiaAnexoPutDto : BasePutDto
    {
        public Guid IdeiaId { get; set; }

        public string DesAnexo { get; set; }

        [StringLength(1, ErrorMessage = "Tipo arquivo deve ter no máximo {1} caracteres.")]
        public string IndTipoArquivo { get; set; }

        [StringLength(50, ErrorMessage = "Nome original deve ter no máximo {1} caracteres.")]
        public string DesNomeOriginal { get; set; }
    }
}
