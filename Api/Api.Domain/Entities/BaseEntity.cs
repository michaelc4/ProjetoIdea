using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? DataAtualizacao { get; set; }     
        public DateTime? DataCriacao
        {
            get { return _dataCriacao; }
            set { _dataCriacao = value == null ? DateTime.UtcNow : value; }
        }
        private DateTime? _dataCriacao;
    }
}
