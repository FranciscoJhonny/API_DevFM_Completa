using DevFM.Api.Extensions;
using DevFM.Business.Intefaces;
using DevFM.Business.Notificacoes;
using DevFM.Business.Services;
using DevFM.Data.Context;
using DevFM.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DevFM.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<APIDbContext>();
            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<IPessoaRepository, PessoaRepository>();

            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            services.AddScoped<IPessoaService, PessoaService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
