using Api.Domain.Entities;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IIdeiaAnexoService<IdeiaAnexoEntity, IdeiaAnexoPresenter, IdeiaAnexoPostDto, IdeiaAnexoPutDto>, IdeiaAnexoService>();
            serviceCollection.AddTransient<IIdeiaService<IdeiaEntity, IdeiaPresenter, IdeiaPostDto, IdeiaPutDto>, IdeiaService>();
            serviceCollection.AddTransient<IProblemaAnexoService<ProblemaAnexoEntity, ProblemaAnexoPresenter, ProblemaAnexoPostDto, ProblemaAnexoPutDto>, ProblemaAnexoService>();
            serviceCollection.AddTransient<IProblemaService<ProblemaEntity, ProblemaPresenter, ProblemaPostDto, ProblemaPutDto>, ProblemaService>();
            serviceCollection.AddTransient<IUsuarioService<UsuarioEntity, UsuarioPresenter, UsuarioPostDto, UsuarioPutDto>, UsuarioService>();
            serviceCollection.AddTransient<ILoginService, LoginService>();
        }
    }
}
