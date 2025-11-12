using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
