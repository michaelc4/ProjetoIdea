using Api.Domain.Entities;
using Api.Domain.Presenters;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class EntityToPresenterProfile : Profile
    {
        public EntityToPresenterProfile()
        {
            CreateMap<UsuarioEntity, UsuarioPresenter>();
            CreateMap<PagedResultPresenter<UsuarioEntity>, PagedResultPresenter<UsuarioPresenter>>();
        }
    }
}
