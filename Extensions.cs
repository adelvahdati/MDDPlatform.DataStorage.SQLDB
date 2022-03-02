using MDDPlatform.DataStorage.SQLDB.Options;
using MDDPlatform.DataStorage.SQLDB.Repositories;
using MDDPlatform.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MDDPlatform.DataStorage.SQLDB
{
    public static class Extensions{
        public static IServiceCollection AddSqlDBContext<T>
            (this IServiceCollection services,IConfiguration configuration,string sectionName)  
            where T : DbContext
        {
            SqlDbOption sqlDbOption = new SqlDbOption();
            configuration.GetSection(sectionName).Bind(sqlDbOption);
            services.AddSingleton(sqlDbOption);
            services.AddDbContext<T>(opt => opt.UseSqlServer(sqlDbOption.ConnectionString));
            
            return services;    
        }

        public static IServiceCollection AddSqlDbRepository<TEntity, TId>
            (this IServiceCollection services,DbContext dbContext) 
            where TEntity : BaseEntity<TId>
        {
            
            services.AddScoped<ISqlDbRepository<TEntity,TId>>(sp=>
            {                
                return new SqlDbRepository<TEntity,TId>(dbContext);
            });

            return services;    
        }
    }
    
}