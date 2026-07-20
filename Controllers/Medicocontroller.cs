using CitasApp.Application.Services;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    public class MedicoController : Controller
    {
        private readonly MedicoService _medicoService;

        public MedicoController(MedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        public IActionResult Index()
        {
            var medicos = _medicoService.GetAll();
            return View(medicos);
        }

        public IActionResult Detalle(int id)
        {
            var medico = _medicoService.GetById(id);
            if (medico == null) return NotFound();
            return View(medico);
        }

        // GET: Medico/Crear
        [Authorize(Roles = "Medico")]
        public IActionResult Crear() => View();

        // POST: Medico/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Medico")]
        public IActionResult Crear(Medico medico)
        {
            if (ModelState.IsValid)
            {
                _medicoService.Add(medico);
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }
    }
}