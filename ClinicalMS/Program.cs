using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Conexion a base de datos
// Obtengo la cadena de texto
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// ------- Command ---------
builder.Services.AddScoped<IEncounterCommand, EncounterCommand>();
builder.Services.AddScoped<IAntecedentCommand, AntecedentCommand>();

// ------- Query ---------
builder.Services.AddScoped<IEncounterQuery, EncounterQuery>();
builder.Services.AddScoped<IAttachmentQuery, AttachmentQuery>();
builder.Services.AddScoped<IAntecedentQuery, AntecedentQuery>();


// ------- Services Encounter --------
builder.Services.AddScoped<ISearchEncounterService, SearchEncounterService>();
builder.Services.AddScoped<ISignEncouterService, SignEncounterService>();
builder.Services.AddScoped<IGetEncounterRangeService, GetEncounterRangeService>();
builder.Services.AddScoped<ICreateEncounterService, CreateEncounterService>();

// ------- Services Antecedent --------
builder.Services.AddScoped<IDeleteAntecedentService, DeleteAntecedentService>();

// ------- Services Attachment --------
builder.Services.AddScoped<IGetAttachmentByPatientService, GetAttachmentByPatientService>();

// ========== JWT Authentication (para validar tokens de otros microservicios) ==========
var jwtKey = builder.Configuration["JwtSettings:key"] ?? "dev-super-secret-key-change-me";

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Aplicar migraciones automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error al aplicar migraciones de base de datos");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
