using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LikePostDto
    {
        [Required(ErrorMessage = "Key é um campo obrigatório.")]
        public string Key { get; set; }

        [Required(ErrorMessage = "IP é um campo obrigatório.")]
        public string IpUsr { get; set; }

        public Guid? IdeiaId { get; set; }

        public Guid? ProblemaId { get; set; }
    }
}
