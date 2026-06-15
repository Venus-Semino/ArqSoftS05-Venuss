using CitaApp.Interfaces;
using CitaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CitaApp.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaRepository _citaRepo;
        private readonly IPacienteRepository _pacienteRepo;
        private readonly IMedicoRepository _medicoRepo;

        public CitaController(ICitaRepository citaRepo, IPacienteRepository pacienteRepo, IMedicoRepository medicoRepo)
        {
            _citaRepo = citaRepo;
            _pacienteRepo = pacienteRepo;
            _medicoRepo = medicoRepo;
        }

        public IActionResult Index()
        {
            ViewBag.Pacientes = _pacienteRepo.GetAll().ToList();
            ViewBag.Medicos = _medicoRepo.GetAll().ToList();
            var citas = _citaRepo.GetAll().ToList();
            return View(citas);
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            ViewBag.Pacientes = _pacienteRepo.GetAll().ToList();
            ViewBag.Medicos = _medicoRepo.GetAll().ToList();
            var citas = _citaRepo.GetByPacienteId(pacienteId).ToList();
            return View(citas);
        }

        // GET: Cita/Crear
        public IActionResult Crear()
        {
            // Creamos listas con formato "Nombre Apellido" para los menús desplegables
            var listaPacientes = _pacienteRepo.GetAll().Select(p => new { Id = p.Id, NombreCompleto = $"{p.Nombre} {p.Apellido}" });
            var listaMedicos = _medicoRepo.GetAll().Select(m => new { Id = m.Id, NombreCompleto = $"{m.Nombre} {m.Apellido}" });

            ViewBag.Pacientes = new SelectList(listaPacientes, "Id", "NombreCompleto");
            ViewBag.Medicos = new SelectList(listaMedicos, "Id", "NombreCompleto");
            return View();
        }

        // POST: Cita/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Cita cita)
        {
            if (ModelState.IsValid)
            {
                _citaRepo.Add(cita);
                return RedirectToAction(nameof(Index));
            }

            var listaPacientes = _pacienteRepo.GetAll().Select(p => new { Id = p.Id, NombreCompleto = $"{p.Nombre} {p.Apellido}" });
            var listaMedicos = _medicoRepo.GetAll().Select(m => new { Id = m.Id, NombreCompleto = $"{m.Nombre} {m.Apellido}" });

            ViewBag.Pacientes = new SelectList(listaPacientes, "Id", "NombreCompleto", cita.PacienteId);
            ViewBag.Medicos = new SelectList(listaMedicos, "Id", "NombreCompleto", cita.MedicoId);
            return View(cita);
        }
    }
}