using HomeLink.Server.Background;
using HomeLink.Server.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HomeLink.Server {
    public class Startup {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services
                .AddSwagger()
                .AddHostedService<UploadingService>()
                .AddSingleton<IUploadingQueue, UploadingQueue>()
                .AddControllers()
                .AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHsts();
            }

            app
               .UseHttpsRedirection()
               .UseRouting()
               .UseAuthorization()
               .UseSwaggerExt()
               .UseEndpoints(endpoints => 
                   endpoints.MapControllers()
                );
        }
    }
}