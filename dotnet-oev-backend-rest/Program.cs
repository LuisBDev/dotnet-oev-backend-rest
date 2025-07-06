using System.Reflection;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
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


// --- Configuración de AWS S3 ---


var awsAccessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
var awsSecretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
var awsRegion = RegionEndpoint.USEast2;

var awsCredentials = new BasicAWSCredentials(awsAccessKey, awsSecretKey);
var awsConfig = new AmazonS3Config
{
    RegionEndpoint = awsRegion
};

//  Registro el cliente de Amazon S3 en el contenedor de inyección de dependencias.
//  Uso de AddSingleton porque el cliente de S3 es thread-safe y está diseñado para ser reutilizado.
builder.Services.AddSingleton<IAmazonS3>(new AmazonS3Client(awsCredentials, awsConfig));

//  Registro del servicio personalizado para las operaciones de S3.
builder.Services.AddScoped<IS3Service, S3Service>();

// --- Configuración de AWS S3 ---


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
builder.Services.AddScoped<IUserLessonProgressService, UserLessonProgressService>();
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


// CORS habilitado para todos los orígenes, métodos y headers
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
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

// Activar CORS globalmente para todos los entornos
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();