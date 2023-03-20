using System.Data.SQLite;
using ApplicationLayer;
using ApplicationLayer.IRepositories;
using Dapper;
using InfrastructureLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace InfrastructureLayer
{
    public static class OnionRegistration
    {
        public static void AddOnion(this IServiceCollection services, string connectionString)
        {
            SqlMapper.AddTypeHandler(new SqliteHelper());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));
            
            services.AddTransient(_ =>
            {
                var connection = new SQLiteConnection(connectionString);
                var compiler = new SqliteCompiler();
                return new QueryFactory(connection, compiler);
            });

            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}