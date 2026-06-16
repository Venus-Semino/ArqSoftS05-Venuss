using Microsoft.AspNetCore.Mvc;
using CitasApp.Application.Services;
using CitasApp.Domain.Models;

namespace CitasApp.Web.Controllers
{
    public class PacienteController : Controller
    {
        private readonly PacienteService _pacienteService;

        public PacienteController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        public IActionResult Index()
        {
            var pacientes = _pacienteService.GetAll();
            return View(pacientes);
        }

        public IActionResult Detalle(int id)
        {
            var paciente = _pacienteService.GetById(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // GET: Paciente/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Paciente/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _pacienteService.Add(paciente);
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }
    }
}