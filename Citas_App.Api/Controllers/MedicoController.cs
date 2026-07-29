using CitasApp.Application.Services;
using Microsoft.AspNetCore.Mvc;
namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicosController : ControllerBase
    {
        private readonly MedicoService
            _service;
        public MedicosController(MedicoService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var medico = _service.GetById(id);
            return medico == null ? NotFound() : Ok(medico);
        }
    }
}