using System;

namespace Api.Domain.Entities
{
    public class IdeiaEntity : BaseEntity
    {
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
        public UsuarioEntity Usuario { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
