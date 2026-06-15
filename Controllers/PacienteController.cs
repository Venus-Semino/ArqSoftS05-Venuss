using CitaApp.Interfaces;
using CitaApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitaApp.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteRepository _pacienteRepo;

        public PacienteController(IPacienteRepository pacienteRepo)
        {
            _pacienteRepo = pacienteRepo;
        }

        public IActionResult Index()
        {
            var pacientes = _pacienteRepo.GetAll();
            return View(pacientes);
        }

        public IActionResult Detalle(int id)
        {
            var paciente = _pacienteRepo.GetById(id);
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
                _pacienteRepo.Add(paciente);
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }
    }
}