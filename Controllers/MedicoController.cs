using CitaApp.Interfaces;
using CitaApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitaApp.Controllers
{
    public class MedicoController : Controller
    {
        private readonly IMedicoRepository _medicoRepo;

        // Inyectamos la interfaz del repositorio
        public MedicoController(IMedicoRepository medicoRepo)
        {
            _medicoRepo = medicoRepo;
        }

        public IActionResult Index()
        {
            // Usamos el repositorio para obtener los datos del JSON
            var medicos = _medicoRepo.GetAll();
            return View(medicos);
        }

        public IActionResult Detalle(int id)
        {
            // Usamos el repositorio para buscar por ID
            var medico = _medicoRepo.GetById(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }
    }
}