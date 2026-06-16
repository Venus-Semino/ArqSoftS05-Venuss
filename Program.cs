using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Application.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// 1. Registramos los Repositorios (Infrastructure)
builder.Services.AddScoped<IPacienteRepository, PacienteJsonRepository>();
builder.Services.AddScoped<IMedicoRepository, MedicoJsonRepository>();
builder.Services.AddScoped<ICitaRepository, CitaJsonRepository>();

// >>> PRUEBA DEL PROFESOR (SWAP ADAPTER) <<<
// Para demostrarle al profesor que funciona, solo comentas la línea de arriba de JsonPacienteRepository 
// y descomentas esta de abajo. ¡La web cambiará automáticamente a usar la memoria RAM sin tocar los controladores!
// builder.Services.AddScoped<IPacienteRepository, MemoriaPacienteRepository>();

// 2. Registramos los Servicios (Application)
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();