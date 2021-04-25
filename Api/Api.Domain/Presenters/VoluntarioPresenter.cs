using System;
using System.Collections.Generic;

namespace Api.Domain.Presenters
{
    public class VoluntarioPresenter
    {
        public string Id { get; set; }
        public UsuarioPresenter Usuario { get; set; }
        public Guid UsuarioId { get; set; }
        public IdeiaPresenter Ideia { get; set; }
        public Guid? IdeiaId { get; set; }
        public ProblemaPresenter Problema { get; set; }
        public Guid? ProblemaId { get; set; }
    }
}
