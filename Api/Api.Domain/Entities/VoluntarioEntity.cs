using System;

namespace Api.Domain.Entities
{
    public class VoluntarioEntity : BaseEntity
    {
        public UsuarioEntity Usuario { get; set; }
        public Guid UsuarioId { get; set; }
        public IdeiaEntity Ideia { get; set; }
        public Guid? IdeiaId { get; set; }
        public ProblemaEntity Problema { get; set; }
        public Guid? ProblemaId { get; set; }
    }
}
