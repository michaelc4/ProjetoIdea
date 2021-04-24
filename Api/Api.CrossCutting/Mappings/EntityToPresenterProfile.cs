using Api.Domain.Entities;
using Api.Domain.Presenters;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class EntityToPresenterProfile : Profile
    {
        public EntityToPresenterProfile()
        {
            CreateMap<IdeiaAnexoEntity, IdeiaAnexoPresenter>();
            CreateMap<PagedResultPresenter<IdeiaAnexoEntity>, PagedResultPresenter<IdeiaAnexoPresenter>>();
            CreateMap<UsuarioEntity, UsuarioPresenter>();
            CreateMap<PagedResultPresenter<UsuarioEntity>, PagedResultPresenter<UsuarioPresenter>>();
        }
    }
}
