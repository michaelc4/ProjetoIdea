using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain.Dtos
{
    public class BasePutDto
    {
        [Required(ErrorMessage = "Id é um campo obrigatório.")]
        public string Id { get; set; }
    }
}
