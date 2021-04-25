using System;
using System.Collections.Generic;

namespace Api.Domain.Presenters
{
    public class IdeiaPresenter
    {
        public string Id { get; set; }
        public string DesIdeia { get; set; }
        public string DesMotivoInvestir { get; set; }
        public string IndInteresseCompartilhar { get; set; }
        public string IndNivelDesenvolvimento { get; set; }
        public string IndNivelSigilo { get; set; }
        public string DesComentario { get; set; }
        public int NumPotencialDisrupcao { get; set; }
        public int NumPessoasImpactadas { get; set; }
        public int NumPotencialGanho { get; set; }
        public int NumValorInvestimento { get; set; }
        public int NumImpactoAmbiental { get; set; }
        public int NumPontuacaoGeral { get; set; }
        public string IndAtivo { get; set; }
        public string IndAprovado { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public UsuarioPresenter Usuario { get; set; }
        public Guid UsuarioId { get; set; }
        public IEnumerable<IdeiaAnexoPresenter> Anexos { get; set; }
    }
}
