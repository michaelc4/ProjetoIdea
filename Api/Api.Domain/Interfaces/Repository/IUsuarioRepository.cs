using Api.Domain.Entities;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepository<UsuarioEntity>
    {
        Task<UsuarioEntity> FindByLogin(string email);
    }
}
