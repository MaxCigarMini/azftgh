using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using ZAMETKI_FINAL.Abstraction;
using ZAMETKI_FINAL.Services;
using ZAMETKI_FINAL.Model;


namespace ZAMETKI_FINAL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructure(builder.Configuration)
                            .AddSwagger();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            // Сначала аутентификация.
            app.UseAuthentication();
            // Потом авторизация.
            app.UseAuthorization();
            // И только потом контроллеры.
            app.MapControllers();


            app.Run();
        }      
    }
}
