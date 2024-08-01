using API.Extensions;
using API.Helpers;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly()); // SERVICIO DE AUTOMAPPER 

builder.Services.ConfigureCors(); // ESTABLECIDO LOS CORS	

// CONFIGURACION DE SERILOG
var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .Enrich.FromLogContext()
                    .CreateLogger();

builder.Logging.AddSerilog(logger);

// CONEXION A SQL SERVER
builder.Services.AddDbContext<FailsContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"))
);


//IMPLEMENTAMOS EL SERVICIO QUE NOS PERMITE USAR LOS REPOSITORIOS EN CUALQUIER COMPONENTE
builder.Services.AddAplicacionServices();

// CONTROL DE VALIDACIONES MODELSTATE
builder.Services.AddValidationErrors();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    // INSTANCIAR EL CONTEXT, ROL, ETC.
    try
    {
        var context = service.GetRequiredService<FailsContext>();

        // MIGRAR 
        await context.Database.MigrateAsync();

        //LLAMAR A LA DATA
        await FailsContextData.LoadDataAsync(context, loggerFactory);
    }
    catch (Exception e)
    {

        loggerFactory.CreateLogger<Program>().LogError(e, "Error en la migración");
    }
};

// AGREGACION DE LA POLITICA DE CORS
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
