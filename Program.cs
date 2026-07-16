using CitasApp.Application.Services;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Infrastructure.Observers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CitasApp.Infrastructure.Data.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ─────────────────────────────────────────────────────────────────────────────
// ── 1. PATRONES DE DISEÑO (GoF) ──────────────────────────────────────────────

var entorno = builder.Environment.EnvironmentName;

// ▶ Patrones Factory y Decorator combinados
builder.Services.AddScoped<IPacienteRepository>(provider =>
{
    // La Factory crea el repositorio base (CSV o Memoria)
    var repoBase = RepositoryFactory.CrearPacienteRepository(entorno);
    // El Decorator lo envuelve para agregarle Logs en consola
    return new LoggingPacienteRepository(repoBase);
});

// ▶ Patrón Factory para Médicos y Citas
builder.Services.AddScoped<IMedicoRepository>(_ => RepositoryFactory.CrearMedicoRepository(entorno));
builder.Services.AddScoped<ICitaRepository>(_ => RepositoryFactory.CrearCitaRepository(entorno));

// ▶ Patrón Observer
builder.Services.AddScoped<ICitaObserver, NotificadorEmailCita>();

// ─────────────────────────────────────────────────────────────────────────────
// ── 2. Servicios de aplicación ───────────────────────────────────────────────
// ─────────────────────────────────────────────────────────────────────────────
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();

// ── 3. MVC y Configuración Web ────────────────────────────────────────────────
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontendExterno", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("PermitirFrontendExterno");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();