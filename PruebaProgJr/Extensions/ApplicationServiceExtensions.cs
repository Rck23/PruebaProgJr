using API.Helpers;
using Core.Interfaces;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
   public static class ApplicationServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
               builder.AllowAnyOrigin() // Dominios (http://localhost:3000, http://Pagina.com)
               .AllowAnyMethod() // metodos como GET, POST
               .AllowAnyHeader()); // Aceptacion de "Content-Type", "Accept"
        });

        // IMPLEMENTACION DE TODOS LOS REPOSITORIOS
        public static void AddAplicacionServices(this IServiceCollection services)
        {
            // AQUI YA CONTIENE TODOS LOS REPOSITORIOS
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddValidationErrors(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => {

                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errors = ActionContext.ModelState.Where(u => u.Value.Errors.Count > 0)
                                                            .SelectMany(u => u.Value.Errors)
                                                            .Select(u => u.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidation()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }

    }
}
