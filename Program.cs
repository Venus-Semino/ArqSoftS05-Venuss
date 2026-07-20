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
    var repoBase = RepositoryFactory.CrearPacienteRepository(entorno);
    return new LoggingPacienteRepository(repoBase);
});

// ▶ Patrón Factory para Médicos y Citas
builder.Services.AddScoped<IMedicoRepository>(_ => RepositoryFactory.CrearMedicoRepository(entorno));
builder.Services.AddScoped<ICitaRepository>(_ => RepositoryFactory.CrearCitaRepository(entorno));

// ▶ Patrón Observer
builder.Services.AddScoped<ICitaObserver, NotificadorEmailCita>();

// ─────────────────────────────────────────────────────────────────────────────
// ── 2. Servicios de aplicación ───────────────────────────────────────────────
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();

// ── 3. MVC ───────────────────────────────────────────────────────────────────
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontendExterno", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// ── 4. Identity + SQLite (solo para usuarios/roles) ──────────────────────────
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Redirigir a login si no está autenticado
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
});

var app = builder.Build();

// ── 5. Aplicar migraciones automáticamente al iniciar ────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("PermitirFrontendExterno");

// IMPORTANTE: Authentication antes de Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();