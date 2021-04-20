using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Api.Data.Implementations
{
    public class UsuarioImplementation : BaseRepository<UsuarioEntity>, IUsuarioRepository
    {
        private DbSet<UsuarioEntity> _dataset;

        public UsuarioImplementation(MyContext context) : base(context)
        {
            _dataset = context.Set<UsuarioEntity>();
        }

        public async Task<UsuarioEntity> FindByLogin(string email)
        {
            return await _dataset.FirstOrDefaultAsync(u => u.DesEmail.Equals(email));
        }
    }
}
