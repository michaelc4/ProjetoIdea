using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;

namespace Api.Data.Implementations
{
    public class IdeiaImplementation : BaseRepository<IdeiaEntity>, IIdeiaRepository
    {
        public IdeiaImplementation(MyContext context) : base(context)
        {

        }
    }
}
