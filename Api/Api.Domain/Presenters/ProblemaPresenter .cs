using System;
using System.Collections.Generic;

namespace Api.Domain.Presenters
{
    public class ProblemaPresenter
    {
        public string Id { get; set; }
        public string DesProblema { get; set; }
        public string IndTipoBeneficio { get; set; }
        public string IndTipoSolucao { get; set; }
        public int NumProbabilidadeInvestir { get; set; }
        public string IndAtivo { get; set; }
        public string IndAprovado { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public UsuarioPresenter Usuario { get; set; }
        public Guid UsuarioId { get; set; }
        public IEnumerable<ProblemaAnexoPresenter> Anexos { get; set; }
    }
}
