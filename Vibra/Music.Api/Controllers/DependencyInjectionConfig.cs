using Microsoft.Extensions.Options;
using ProvaMed.DomainModel.Interfaces.UoW;
using ProvaMed.Infra.UoW;
using Swashbuckle.AspNetCore.SwaggerGen;
using Vibra.DomainModel.Interfaces.Repositories;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DomainService;
using Vibra.Services;

namespace ProvaMed.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IBandService, BandService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IFavoriteBandService, FavoriteBandService>();
            services.AddScoped<IFavoriteSongService, FavoriteSongService>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IPlaylistService, PlaylistService>();
            services.AddScoped<ISongService, SongService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IUserService, UserService>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}