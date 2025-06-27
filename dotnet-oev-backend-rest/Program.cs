using System.Reflection;
using dotnet_oev_backend_rest.Data;
using dotnet_oev_backend_rest.Middleware;
using dotnet_oev_backend_rest.Repositories.UnitOfWork;
using dotnet_oev_backend_rest.Services.Implementations;
using dotnet_oev_backend_rest.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Conexión a la base de datos MySQL 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 39));

// Registro de la base de datos con MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, serverVersion)
);

// 1. Registro de AutoMapper para que encuentre los perfiles de mapeo
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// 2. Registro de la Unidad de Trabajo y los servicios
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<ILessonService, LessonService>();
// No se necesita registrar los repositorios individualmente si se usa Unit of Work


builder.Services.AddControllers();

// Swagger config clásico
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OEV Prime API",
        Version = "v1",
        Description = "API for OEV Prime application"
    });
});

var app = builder.Build();

// Registrar el middleware de manejo global de excepciones
app.UseGlobalExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OEV Prime API V1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz: http://localhost:5071/
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();