using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceColection)
        {
            serviceColection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceColection.AddScoped(typeof(IUsuarioRepository), typeof(UsuarioImplementation));

            var connectionString = "server=dbapiinova.mysql.database.azure.com;port=3306;database=dbapiinova;uid=dbapiinova@dbapiinova;password=root@inova123";
            serviceColection.AddDbContext<MyContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)), mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)));
        }
    }
}
