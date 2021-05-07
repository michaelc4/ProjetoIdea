using System;

namespace Api.Domain.Entities
{
    public class LikeEntity : BaseEntity
    {
        public string IpUsr { get; set; }
        public IdeiaEntity Ideia { get; set; }
        public Guid? IdeiaId { get; set; }
        public ProblemaEntity Problema { get; set; }
        public Guid? ProblemaId { get; set; }
    }
}
