using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class VoluntarioPutDto : BasePutDto
    {
        public Guid UsuarioId { get; set; }

        public Guid? IdeiaId { get; set; }

        public Guid? ProblemaId { get; set; }
    }
}
