using Framework.Domain.Core.Repositories;
using Framework.Domain.Core.Service;
using Framework.Infrastructure.Repositories.Relational;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;

namespace Framework.Aplication.Services
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PrincipalDbContext>(options =>
            {
                options.UseInMemoryDatabase("MemoryDataBase");
                //    options.UseSqlServer(this.Configuration.GetConnectionString("PrincipalRelationalConnection"));
            });

            services.AddScoped(typeof(IRepository<>), typeof(RelationalRepository<>));

            services.Scan(scan =>
            {
                scan.FromApplicationDependencies()
                    .AddClasses(filter => filter.AssignableTo<IRepository>())
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()

                    .AddClasses(filter => filter.AssignableTo<IService>())
                        .AsImplementedInterfaces()
                        .AsSelf()
                        .WithScopedLifetime();
            });

            services.AddMvc();

            services.AddSwaggerGen(setup => setup
                .SwaggerDoc(
                    "v1",
                    new Info
                    {
                        Title = "API Doc",
                        Version = "v1",
                    }));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Serviço de Usuários");
            });
        }
    }
}
