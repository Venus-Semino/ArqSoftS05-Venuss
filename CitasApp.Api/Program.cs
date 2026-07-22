using CitasApp.Application.Services;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Infrastructure.Observers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CitasApp.Infrastructure.Data.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// ── 1. Controladores y Swagger (Requisito de la entrega) ────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ── 2. PATRONES DE DISEÑO (GoF) ─────────────────────────────────────
var entorno = builder.Environment.EnvironmentName;

builder.Services.AddScoped<IPacienteRepository>(provider =>
{
    var repoBase = RepositoryFactory.CrearPacienteRepository(entorno);
    return new LoggingPacienteRepository(repoBase);
});

builder.Services.AddScoped<IMedicoRepository>(_ => RepositoryFactory.CrearMedicoRepository(entorno));
builder.Services.AddScoped<ICitaRepository>(_ => RepositoryFactory.CrearCitaRepository(entorno));

// ¡AQUÍ ESTÁ LA SOLUCIÓN AL ERROR! Registramos el Observer
builder.Services.AddScoped<ICitaObserver, NotificadorEmailCita>();

// ── 3. Servicios de aplicación ──────────────────────────────────────
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();