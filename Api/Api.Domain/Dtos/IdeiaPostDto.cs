using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Presenters
{
    public class IdeiaPostDto
    {
        public string DesIdeia { get; set; }

        public string DesMotivoInvestir { get; set; }

        [StringLength(1, ErrorMessage = "Interesse em compartilhar deve ter no máximo {1} caracteres.")]
        public string IndInteresseCompartilhar { get; set; }

        [StringLength(1, ErrorMessage = "Nível de desenvolvimento deve ter no máximo {1} caracteres.")]
        public string IndNivelDesenvolvimento { get; set; }

        [StringLength(1, ErrorMessage = "Nível sigilo deve ter no máximo {1} caracteres.")]
        public string IndNivelSigilo { get; set; }

        public string DesComentario { get; set; }

        public Guid UsuarioId { get; set; }

        public List<IdeiaAnexoPostDto> Anexos { get; set; }
    }
}
