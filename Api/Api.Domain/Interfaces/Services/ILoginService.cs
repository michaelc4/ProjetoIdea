using Api.Domain.Dtos;
using Api.Domain.Presenters;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        Task<object> CreateUser(UsuarioPostDto user);
        Task<object> FindByLogin(LoginDto user);
    }
}
