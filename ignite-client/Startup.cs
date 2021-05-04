using Apache.Ignite.Core;
using Apache.Ignite.Core.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ignite_client.Model;
using Apache.Ignite.Core.Client.Cache;

namespace ignite_client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var igniteSettings = new IgniteSettings();
            Configuration.GetSection(nameof(IgniteSettings)).Bind(igniteSettings);

            services.AddSingleton((s) => new IgniteClientConfiguration
            {
                Endpoints = igniteSettings.Endpoints,
                SslStreamFactory = new SslStreamFactory
                    {                                
                        CertificatePath = igniteSettings.CertificatePath,
                        CertificatePassword = igniteSettings.CertificatePassword,
                        SkipServerCertificateValidation = igniteSettings.SkipServerCertificateValidation
                    }
            });

            services.AddScoped<IIgniteClient>(s => Ignition.StartClient(s.GetRequiredService<IgniteClientConfiguration>()));

            services.AddScoped<ICacheClient<System.DateTime, Rates>>(s => {
                var igniteClient = s.GetRequiredService<IIgniteClient>();

                return igniteClient.GetOrCreateCache<System.DateTime, Rates>($"ignite_client.common.{nameof(Rates)}");
            });            

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ignite_client", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ignite_client v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
