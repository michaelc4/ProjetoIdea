using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data.Implementations
{
    public class IdeiaAnexoImplementation : BaseRepository<IdeiaAnexoEntity>, IIdeiaAnexoRepository
    {
        public IdeiaAnexoImplementation(MyContext context) : base(context)
        {

        }

        public async Task<IEnumerable<IdeiaAnexoEntity>> GetByProjectAsync(string projectId)
        {
            try
            {
                var query = GetQuery().Where(x => x.IdeiaId.ToString() == projectId);
                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
