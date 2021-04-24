using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;

namespace Api.Data.Implementations
{
    public class ProblemaAnexoImplementation : BaseRepository<ProblemaAnexoEntity>, IProblemaAnexoRepository
    {
        public ProblemaAnexoImplementation(MyContext context) : base(context)
        {

        }
    }
}
