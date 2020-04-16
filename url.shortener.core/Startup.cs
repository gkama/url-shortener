using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using url.shortener.data;
using url.shortener.services;

namespace url.shortener.core
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public IWebHostEnvironment _env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddScoped<FakeManager>();

            if (_env.IsDevelopment())
                services.AddDbContext<UrlContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            else
                services.AddDbContext<UrlContext>(o => o.UseNpgsql(_configuration.GetConnectionString("PostgreSQL")));

            services.AddHealthChecks();
            services.AddLogging();

            services.AddControllers();
            services.AddMvcCore()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.WriteIndented = true;
                    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
        }

        public void Configure(IApplicationBuilder app, IServiceProvider services)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                services.GetRequiredService<FakeManager>()
                    .UseFakeContext();
            }

            app.UseHealthChecks("/ping");

            app.UseRouting();
            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });
        }
    }
}
