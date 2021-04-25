using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Presenters
{
    public class IdeiaPutDto
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

        public int NumPotencialDisrupcao { get; set; }

        public int NumPessoasImpactadas { get; set; }

        public int NumPotencialGanho { get; set; }

        public int NumValorInvestimento { get; set; }

        public int NumImpactoAmbiental { get; set; }

        public int NumPontuacaoGeral { get; set; }

        [StringLength(1, ErrorMessage = "Indicador ativo deve ter no máximo {1} caracteres.")]
        public string IndAtivo { get; set; }

        [StringLength(1, ErrorMessage = "Indicador aprovado deve ter no máximo {1} caracteres.")]
        public string IndAprovado { get; set; }

        public Guid UsuarioId { get; set; }

        public List<IdeiaAnexoPostDto> Anexos { get; set; }
    }
}
