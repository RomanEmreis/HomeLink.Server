using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using static HomeLink.Server.Constants;

namespace HomeLink.Server.Extensions {
    internal static class MiddlewareExtensions {
        internal static IServiceCollection AddSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(c =>
                c.SwaggerDoc(APP_VERSION, new OpenApiInfo { Title = APP_NAME, Version = APP_VERSION })
             );

        internal static IApplicationBuilder UseSwaggerExt(this IApplicationBuilder app) =>
            app.UseSwagger()
               .UseSwaggerUI(c =>
                   c.SwaggerEndpoint(SWAGGER_JSON, APP_NAME_WITH_VERSION)
                );
    }
}
