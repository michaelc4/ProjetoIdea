using System;

namespace Api.Domain.Entities
{
    public class ProblemaEntity : BaseEntity
    {
        public string DesProblema { get; set; }
        public string IndTipoBeneficio { get; set; }
        public string IndTipoSolucao { get; set; }
        public int NumProbabilidadeInvestir { get; set; }
        public string IndAtivo { get; set; }
        public string IndAprovado { get; set; }
        public UsuarioEntity Usuario { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
