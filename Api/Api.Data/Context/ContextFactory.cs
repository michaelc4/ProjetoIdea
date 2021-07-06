using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Api.Data.Context
{
    class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var connectionString = "server=dbapiinova.us-east-2.rds.amazonaws.com;port=3306;database=dba;uid=dba;password=root";
            //dotnet ef migrations add UsuarioMigration_ChangeProblema_Table
            //dotnet ef database update
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)), mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend).EnableRetryOnFailure());
            return new MyContext(optionsBuilder.Options);
        }
    }
}
