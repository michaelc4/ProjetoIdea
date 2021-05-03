using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Presenters
{
    public class LikePutDto
    {
        [Required(ErrorMessage = "Id é um campo obrigatório.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "IP é um campo obrigatório.")]
        public string IpUsr { get; set; }

        public Guid? IdeiaId { get; set; }

        public Guid? ProblemaId { get; set; }
    }
}
