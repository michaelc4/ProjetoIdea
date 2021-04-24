using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;

namespace Api.Data.Implementations
{
    public class IdeiaAnexoImplementation : BaseRepository<IdeiaAnexoEntity>, IIdeiaAnexoRepository
    {
        public IdeiaAnexoImplementation(MyContext context) : base(context)
        {
           
        }
    }
}
