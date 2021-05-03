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
            CreateMap<IdeiaEntity, IdeiaPresenter>();
            CreateMap<PagedResultPresenter<IdeiaEntity>, PagedResultPresenter<IdeiaPresenter>>();
            CreateMap<LikeEntity, LikePresenter>();
            CreateMap<PagedResultPresenter<LikeEntity>, PagedResultPresenter<LikePresenter>>();
            CreateMap<ProblemaAnexoEntity, ProblemaAnexoPresenter>();
            CreateMap<PagedResultPresenter<ProblemaAnexoEntity>, PagedResultPresenter<ProblemaAnexoPresenter>>();
            CreateMap<ProblemaEntity, ProblemaPresenter>();
            CreateMap<PagedResultPresenter<ProblemaEntity>, PagedResultPresenter<ProblemaPresenter>>();
            CreateMap<UsuarioEntity, UsuarioPresenter>();
            CreateMap<PagedResultPresenter<UsuarioEntity>, PagedResultPresenter<UsuarioPresenter>>();
            CreateMap<VoluntarioEntity, VoluntarioPresenter>();
            CreateMap<PagedResultPresenter<VoluntarioEntity>, PagedResultPresenter<VoluntarioPresenter>>();
        }
    }
}
