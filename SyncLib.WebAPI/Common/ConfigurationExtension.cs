using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SyncLib.BusinessLogic;
using SyncLib.BusinessLogic.Interface;
using SyncLib.BusinessLogic.MappingProfile;
using SyncLib.Repository.Database;
using SyncLib.Repository.Interface;
using System.Text;

namespace SyncLib.WebAPI.Common
{
    public static class ConfigurationExtension
    {

        /// <summary>
        /// Adding All services at once
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddAllServices(IServiceCollection services, IConfiguration configuration)
        {
            GeneralConfigurations(services);
            ConfigureJwtAuthentication(services, configuration);
            ConfigureRepository(services);
            ConfigureAutoMapperProfiles(services);
            ConfigureSerivces(services);
            ConfigureDatabaseSQLContext(services, configuration);
        }

        /// <summary>
        /// Configuraing General Configurations
        /// </summary>
        /// <param name="services"></param>
        public static void GeneralConfigurations(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "The Hall of Learning", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer",
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }


        /// <summary>
        /// Configuring Services (Business Logics)
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSerivces(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IGeneralService, GeneralService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
        }

        /// <summary>
        /// Configuring Repository
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository), typeof(Repository.Repository));
        }

        /// <summary>
        /// Configuring Connection Strings
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureDatabaseSQLContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SyncLib_DbContext>(Options =>
            {
                Options.UseSqlServer(
                    configuration.GetConnectionString("HallOfLearning"),
                    sqlServerOptions => sqlServerOptions
                        .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                );
            });
        }


        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["ApplicationSettings:Jwt:SecretKey"].ToString())),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
        }
        /// <summary>
        /// Configuring Middlewares
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureMiddleWares(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        public static void ConfigureAutoMapperProfiles(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(AutoMapperProfiles));
        }
    }
}
