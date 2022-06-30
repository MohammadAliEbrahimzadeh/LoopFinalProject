using LoopMainProject.Business.Base;
using LoopMainProject.Business.Contract;
using LoopMainProject.DataAccess;
using LoopMainProject.DataAccess.Context;
using LoopMainProject.DataAccess.Contract;
using LoopMainProject.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;




namespace LoopMainProject.Host
{
    internal static class DependencyInjectionExtension
    {
        internal static IServiceCollection InjectSieve(this IServiceCollection services) =>
            services.AddScoped<ISieveProcessor, SieveProcessor>();

        internal static IServiceCollection InjectUserService(this IServiceCollection services) =>
          services.AddScoped<IUserService, UserService>();

        internal static IServiceCollection InjectPostService(this IServiceCollection services) =>
             services.AddScoped<IPostService, PostService>();

        internal static IServiceCollection InjectVotingService(this IServiceCollection services) =>
        services.AddScoped<IVotingService, VotingService>();


        internal static IServiceCollection InjectUnitOfWork(this IServiceCollection services) =>
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        

        internal static IServiceCollection InjectContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContextPool<LoopMainProjectContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(configuration.GetConnectionString("LoopContext"));
            });

    }
}
