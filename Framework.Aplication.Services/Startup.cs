using Framework.Domain.Core.Factory;
using Framework.Domain.Core.Repositories;
using Framework.Domain.Core.Service;
using Framework.Infrastructure.Repositories.Relational;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;
using Framework.Aplication.Services.Configurations;

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
            this.ConfigureJWT(services);

            services.AddDbContext<PrincipalDbContext>(options =>
            {
                // Utilizar banco em memória
                //options.UseInMemoryDatabase("MemoryDataBase");
                var conectionString = this.Configuration.GetConnectionString("PrincipalRelationalConnection");

                options.UseSqlServer(conectionString);
            });

            services.AddScoped(typeof(IRepository<>), typeof(RelationalRepository<>));

            services.Scan(scan =>
            {
                scan.FromApplicationDependencies()
                    .AddClasses(filter => filter.AssignableTo<IRepository>())
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()

                    .AddClasses(filter => filter.AssignableTo<IFactory>())
                        .AsImplementedInterfaces()
                        .AsSelf()
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


            services.AddCors();
            services.AddMvc();
        }

        private void ConfigureJWT(IServiceCollection services)
        {
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(Configuration.GetSection("TokenConfigurations")).Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            //// Ativa o uso do token como forma de autorizar o acesso
            //// a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(policy => policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

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
