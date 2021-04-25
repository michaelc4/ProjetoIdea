using Api.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Repository
{
    public interface IIdeiaAnexoRepository : IRepository<IdeiaAnexoEntity>
    {
        Task<IEnumerable<IdeiaAnexoEntity>> GetByProjectAsync(string projectId);
    }
}
