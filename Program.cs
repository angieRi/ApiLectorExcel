using ApiLectorExcel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
// Configuración para MySQL
builder.Services.AddDbContext<ApilectornetContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("connectionDB"),
        new MySqlServerVersion(new Version(8, 0, 41)),
        mysqlOptions =>
        {
            mysqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null); // Añade este tercer parámetro
            mysqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore);
        }));
// Agregar servicios CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Agregar servicios al contenedor.
builder.Services.AddControllers();

// Configurar Swagger/OpenAPI
// Configuración de Swagger
// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiLector", Version = "v1" });

    // Configura Swagger para manejar archivos de formulario
    c.MapType<IFormFile>(() => new OpenApiSchema { Type = "string", Format = "binary" });
});


builder.Services.AddScoped<IArchivoService, ArchivoService>();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiLector v1");
        c.RoutePrefix = string.Empty; // Esto hace que Swagger UI esté disponible en la raíz de la URL
    });
}

app.UseHttpsRedirection();

// Usar CORS
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

// Mapear controladores
app.MapControllers();

app.Run();
