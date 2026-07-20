using System.Diagnostics;
using CitasApp.Application.Services;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PacienteService _pacienteService;
        private readonly MedicoService _medicoService;
        private readonly CitaService _citaService;

        public HomeController(PacienteService pacienteService,
                              MedicoService medicoService,
                              CitaService citaService)
        {
            _pacienteService = pacienteService;
            _medicoService = medicoService;
            _citaService = citaService;
        }

        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                if (User.IsInRole("Medico"))
                    return RedirectToAction("DashboardMedico");
                else
                    return RedirectToAction("DashboardPaciente");
            }
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "Medico")]
        public IActionResult DashboardMedico()
        {
            ViewBag.TotalCitas = _citaService.GetAll().Count();
            ViewBag.TotalMedicos = _medicoService.GetAll().Count();
            ViewBag.UltimasCitas = _citaService.GetAll().Take(5).ToList();
            ViewBag.Pacientes = _pacienteService.GetAll().ToList();
            ViewBag.Medicos = _medicoService.GetAll().ToList();
            return View();
        }

        [Authorize(Roles = "Paciente")]
        public IActionResult DashboardPaciente()
        {
            ViewBag.TotalCitas = _citaService.GetAll().Count();
            ViewBag.UltimasCitas = _citaService.GetAll().Take(5).ToList();
            ViewBag.Pacientes = _pacienteService.GetAll().ToList();
            ViewBag.Medicos = _medicoService.GetAll().ToList();
            return View();
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}