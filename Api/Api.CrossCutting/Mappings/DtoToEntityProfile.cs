using Api.Domain.Entities;
using Api.Domain.Presenters;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<UsuarioPostDto, UsuarioEntity>();
            CreateMap<UsuarioPutDto, UsuarioEntity>();
        }
    }
}
