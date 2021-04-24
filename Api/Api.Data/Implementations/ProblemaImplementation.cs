using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;

namespace Api.Data.Implementations
{
    public class ProblemaImplementation : BaseRepository<ProblemaEntity>, IProblemaRepository
    {
        public ProblemaImplementation(MyContext context) : base(context)
        {

        }
    }
}
