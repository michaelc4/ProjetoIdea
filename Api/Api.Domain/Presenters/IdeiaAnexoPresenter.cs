using System;

namespace Api.Domain.Presenters
{
    public class IdeiaAnexoPresenter
    {
        public string Id { get; set; }
        public string DesAnexo { get; set; }
        public string IndTipoArquivo { get; set; }
        public string DesNomeOriginal { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
