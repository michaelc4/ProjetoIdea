using System;

namespace Api.Domain.Entities
{
    public class IdeiaAnexoEntity : BaseEntity
    {
        public string DesAnexo { get; set; }
        public string IndTipoArquivo { get; set; }
        public string DesNomeOriginal { get; set; }
        public IdeiaEntity Ideia { get; set; }
        public Guid IdeiaId { get; set; }
    }
}
