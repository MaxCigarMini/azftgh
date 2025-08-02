using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using ZAMETKI_FINAL.Abstraction;
using ZAMETKI_FINAL.Configurations.DataBase;
using ZAMETKI_FINAL.Database;
using ZAMETKI_FINAL.Services;
using static ZAMETKI_FINAL.Abstraction.JWTOptions;

namespace ZAMETKI_FINAL
{
    public static class Composer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
            services.AddAuthorization();

            services.AddAutoMapper(typeof(Composer).Assembly);

            services.AddOptions<ApplicationDbContextSettings>()
                .Bind(configuration.GetRequiredSection(nameof(ApplicationDbContextSettings)))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.Configure<ApplicationDbContextSettings>(
                configuration.GetRequiredSection(nameof(ApplicationDbContextSettings)));



            services.AddDbContext<ApplicationDbContext>(options => 
            { 
                options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=1111;Database=postgres"); 
            });
            services.AddControllers();

            services.AddOptions<JwtOptions>()
                    .Bind(configuration.GetRequiredSection(nameof(JwtOptions)))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();


            services.AddSingleton<IUserInterface, UserService>();
            services.AddSingleton<INoteInterface, NoteService>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                // Настройка сваггера, чтобы в нем появилась кнопка авторизации,
                // которая позволит записать токен
                // и передавать его в заголовке с каждым последующим запросом.
                options.AddSecurityDefinition(
                    // Тип токена.
                    JwtBearerDefaults.AuthenticationScheme,
                    // Схема для передачи токена в сваггере.
                    new OpenApiSecurityScheme
                    {
                        // Где будем передавать токен.
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        // Имя для заголовка.
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        // Формат для Bearer токена.
                        BearerFormat = "JWT",
                    });
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme,
                    },
                },
                Array.Empty<string>()
            },
                    });
            });

            return services;
        }
    }
}
