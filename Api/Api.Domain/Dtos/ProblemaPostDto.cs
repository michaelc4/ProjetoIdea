﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Presenters
{
    public class ProblemaPostDto
    {
        public string DesProblema { get; set; }

        [StringLength(1, ErrorMessage = "Tipo benefício deve ter no máximo {1} caracteres.")]
        public string IndTipoBeneficio { get; set; }

        [StringLength(1, ErrorMessage = "Tipo solução deve ter no máximo {1} caracteres.")]
        public string IndTipoSolucao { get; set; }

        public int NumProbabilidadeInvestir { get; set; }

        public Guid UsuarioId { get; set; }

        public List<ProblemaAnexoPostDto> Anexos { get; set; }
    }
}
