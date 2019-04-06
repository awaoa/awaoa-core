using Awaoa.Core.Configuration;
using Awaoa.Core.Repositories;
using Awaoa.Core.Services;
using Awaoa.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Awaoa.Core.Extensions
{
    public static class AwaoaCoreServiceCollectionExtensions
    {
        private static readonly string defaultBlobStorageConfigurationSection = $"{AwaoaDefaults.Name}:BlobStorage";

        public static IServiceCollection AddAwaoaDefaultServices<TDbContext>(this IServiceCollection services, IConfiguration configuration) where TDbContext : DbContext
        {
            //Register unit of work
            services.AddAwaoaDefaultUnitOfWork<TDbContext>();

            //Register generic repository
            services.AddAwaoaDefaultRepository();

            services.AddAwaoaDefaultBlobStorageService(configuration);

            services.ConfigureAwaoaDefaultOptions(configuration);

            return services;
        }

        public static IServiceCollection AddAwaoaDefaultUnitOfWork<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();
            return services;
        }

        public static IServiceCollection AddAwaoaDefaultRepository(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IRepository<,>), typeof(GenericRepository<,>));
            return services;
        }

        public static IServiceCollection AddAwaoaDefaultBlobStorageService(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddAwaoaBlobStorageService(configuration, defaultBlobStorageConfigurationSection);
        }

        public static IServiceCollection AddAwaoaBlobStorageService(this IServiceCollection services, IConfiguration configuration, string section)
        {
            if (string.IsNullOrEmpty(section))
            {
                section = defaultBlobStorageConfigurationSection;
            }
            services.Configure<AzureBlobStorageOptions>(options => configuration.GetSection(section));

            return services.AddSingleton<IBlobStorageService, AzureBlobService>();
        }

        public static IServiceCollection ConfigureAwaoaDefaultOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services.ConfigureAwaoaCoreOptions(configuration, AwaoaDefaults.Name);
        }

        public static IServiceCollection ConfigureAwaoaCoreOptions(this IServiceCollection services, IConfiguration configuration, string section)
        {
            // mapping options
            if (string.IsNullOrEmpty(section))
            {
                section = AwaoaDefaults.Name;
            }

            return services.Configure<AristotleOptions>(options => configuration.GetSection(section));
        }
    }
}