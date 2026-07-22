using Microsoft.AspNetCore.Mvc;
using CitasApp.Application.Services;
using CitasApp.Domain.Models;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitaController : ControllerBase
    {
        private readonly CitaService _citaService;

        // Inyectamos el servicio que armamos en Program.cs
        public CitaController(CitaService citaService)
        {
            _citaService = citaService;
        }

        // POST: api/cita
        [HttpPost]
        public IActionResult CrearCita([FromBody] Cita cita)
        {
            // Al llamar al Add, se activará el Decorator (Log) y el Observer (Email)
            _citaService.Add(cita);

            return Ok(new { Mensaje = "Cita creada exitosamente y notificaciones enviadas." });
        }
    }
}