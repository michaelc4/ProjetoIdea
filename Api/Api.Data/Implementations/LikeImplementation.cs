using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;

namespace Api.Data.Implementations
{
    public class LikeImplementation : BaseRepository<LikeEntity>, ILikeRepository
    {
        public LikeImplementation(MyContext context) : base(context)
        {

        }
    }
}
