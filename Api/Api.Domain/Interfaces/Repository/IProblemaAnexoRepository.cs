using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Repository
{
    public interface IProblemaAnexoRepository : IRepository<ProblemaAnexoEntity>
    {
        Task<IEnumerable<ProblemaAnexoEntity>> GetByProjectAsync(string projectId);
    }
}
