using HomeLink.Server.Background;
using HomeLink.Server.Extensions;
using HomeLink.Server.Services;
using HomeLink.Server.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
                .AddHostedService<UploadingQueueService>()
                .AddSingleton<IUploadingQueue, UploadingQueue>()
                .AddScoped<IDownloadingService, DownloadingService>()
                .AddScoped<IUploadingService, UploadingService>()
                .AddScoped<FileNameValidationFilter>()
                .AddScoped<UploadingDataValidationFilter>()
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