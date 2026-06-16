using CitasApp.Application.Services;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var dataFolder = Path.Combine(builder.Environment.WebRootPath, "data");
Directory.CreateDirectory(dataFolder);

// Rutas para CSV
//var csvPacientes = Path.Combine(dataFolder, "pacientes.csv");
//var csvMedicos = Path.Combine(dataFolder, "medicos.csv");
//var csvCitas = Path.Combine(dataFolder, "citas.csv");

// Ruta para SQLite (un solo archivo .db para las 3 tablas)
//var sqlitePath = Path.Combine(dataFolder, "citasapp.db");

// ─────────────────────────────────────────────────────────────────────────────
// AQUÍ enchufas el Adapter que quieres usar para cada entidad.
// Domain y Application NO se tocan — solo cambia este archivo.
// ─────────────────────────────────────────────────────────────────────────────

// ── 1. Elige tus Adapters ─────────────────────────────────────────────────────
// Descomenta el bloque que quieras usar y comenta los demás.
// ¡Las interfaces (Ports) no cambian!

// ▶ Bloque A — JSON (Como estaba antes)

builder.Services.AddScoped<IPacienteRepository, PacienteJsonRepository>();
builder.Services.AddScoped<IMedicoRepository, MedicoJsonRepository>();
builder.Services.AddScoped<ICitaRepository, CitaJsonRepository>();


// ▶ Bloque B — CSV ← ACTIVO AHORA
//builder.Services.AddScoped<IPacienteRepository, CsvPacienteRepository>();
//builder.Services.AddScoped<IMedicoRepository, CsvMedicoRepository>();
//builder.Services.AddScoped<ICitaRepository, CsvCitaRepository>();

// ▶ Bloque C — Memoria RAM (Prueba extra)
/*
builder.Services.AddScoped<IPacienteRepository, MemoriaPacienteRepository>();
// ...
*/

// ── 2. Servicios de aplicación (no cambian con el Adapter) ───────────────────
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();

// ── 3. MVC ────────────────────────────────────────────────────────────────────
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();