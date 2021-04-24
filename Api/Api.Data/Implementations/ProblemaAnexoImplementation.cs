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
    public class ProblemaAnexoImplementation : BaseRepository<ProblemaAnexoEntity>, IProblemaAnexoRepository
    {
        public ProblemaAnexoImplementation(MyContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ProblemaAnexoEntity>> GetByProjectAsync(string projectId)
        {
            try
            {
                var query = GetQuery().Where(x => x.ProblemaId.ToString() == projectId);
                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
