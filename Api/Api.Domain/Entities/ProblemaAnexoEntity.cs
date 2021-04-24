using System;

namespace Api.Domain.Entities
{
    public class ProblemaAnexoEntity : BaseEntity
    {
        public string DesAnexo { get; set; }
        public string IndTipoArquivo { get; set; }
        public string DesNomeOriginal { get; set; }
        public ProblemaEntity Problema { get; set; }
        public Guid ProblemaId { get; set; }
    }
}
