using Api.Domain.Entities;
using Api.Domain.Presenters;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<IdeiaAnexoPostDto, IdeiaAnexoEntity>();
            CreateMap<IdeiaAnexoPutDto, IdeiaAnexoEntity>();
            CreateMap<ProblemaAnexoPostDto, ProblemaAnexoEntity>();
            CreateMap<ProblemaAnexoPutDto, ProblemaAnexoEntity>();
            CreateMap<ProblemaPostDto, ProblemaEntity>();
            CreateMap<ProblemaPutDto, ProblemaEntity>();
            CreateMap<UsuarioPostDto, UsuarioEntity>();
            CreateMap<UsuarioPutDto, UsuarioEntity>();
        }
    }
}
