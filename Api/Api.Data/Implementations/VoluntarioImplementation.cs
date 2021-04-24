using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;

namespace Api.Data.Implementations
{
    public class VoluntarioImplementation : BaseRepository<VoluntarioEntity>, IVoluntarioRepository
    {
        public VoluntarioImplementation(MyContext context) : base(context)
        {

        }
    }
}
