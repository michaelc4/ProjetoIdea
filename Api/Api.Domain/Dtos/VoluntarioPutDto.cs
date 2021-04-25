using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Presenters
{
    public class VoluntarioPutDto
    {
        [Required(ErrorMessage = "Id é um campo obrigatório.")]
        public string Id { get; set; }

        public Guid UsuarioId { get; set; }

        public Guid? IdeiaId { get; set; }

        public Guid? ProblemaId { get; set; }
    }
}
