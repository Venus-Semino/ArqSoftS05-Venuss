using System.Security.Claims;
using CitasApp.Application.Services;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Aplications.DTOs; // Necesario para tu CitaViewModel
using CitasApp.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CitasApp.Tests.Controllers
{
    // --------------------------------------------------------------------
    // Adapters "fake" en memoria adaptados a TUS interfaces en inglés.
    // --------------------------------------------------------------------

    public class CitaRepositoryFake : ICitaRepository
    {
        private readonly List<Cita> _citas;

        public CitaRepositoryFake(List<Cita> citas) => _citas = citas;

        public List<Cita> GetAll() => _citas;

        public List<Cita> GetByPacienteId(int pacienteId)
            => _citas.Where(c => c.PacienteId == pacienteId).ToList();

        public Cita? GetById(int id) => _citas.FirstOrDefault(c => c.Id == id);

        public void Add(Cita cita) => _citas.Add(cita);

        IEnumerable<Cita> ICitaRepository.GetAll()
        {
            return GetAll();
        }

        IEnumerable<Cita> ICitaRepository.GetByPacienteId(int pacienteId)
        {
            return GetByPacienteId(pacienteId);
        }
    }

    public class PacienteRepositoryFake : IPacienteRepository
    {
        private readonly List<Paciente> _pacientes;

        public PacienteRepositoryFake(List<Paciente> pacientes) => _pacientes = pacientes;

        public void Add(Paciente paciente)
        {
            throw new NotImplementedException();
        }

        public List<Paciente> GetAll() => _pacientes;

        public Paciente? GetById(int id) => _pacientes.FirstOrDefault(p => p.Id == id);

        IEnumerable<Paciente> IPacienteRepository.GetAll()
        {
            return GetAll();
        }
    }

    public class MedicoRepositoryFake : IMedicoRepository
    {
        private readonly List<Medico> _medicos;

        public MedicoRepositoryFake(List<Medico> medicos) => _medicos = medicos;

        public void Add(Medico medico)
        {
            throw new NotImplementedException();
        }

        public List<Medico> GetAll() => _medicos;

        public Medico? GetById(int id) => _medicos.FirstOrDefault(m => m.Id == id);

        IEnumerable<Medico> IMedicoRepository.GetAll()
        {
            return GetAll();
        }
    }

    // --------------------------------------------------------------------
    // Observer "fake" requerido por tu CitaService
    // --------------------------------------------------------------------
    public class CitaObserverFake : ICitaObserver
    {
        public void NotificarCitaCreada(Cita cita)
        {
            // No hace nada en las pruebas, solo cumple el contrato para que compile
        }
    }

    // --------------------------------------------------------------------
    // Pruebas — solo el camino del administrador
    // --------------------------------------------------------------------

    public class CitaControllerAdminTests
    {
        private CitaController CrearControllerConDatosDePrueba(
            out List<Cita> citasEsperadas)
        {
            // Arrange — datos de prueba en memoria
            citasEsperadas = new List<Cita>
            {
                new Cita { Id = 1, PacienteId = 10, MedicoId = 1, Estado = "Pendiente" },
                new Cita { Id = 2, PacienteId = 20, MedicoId = 1, Estado = "Confirmada" },
                new Cita { Id = 3, PacienteId = 10, MedicoId = 1, Estado = "Pendiente" }
            };

            var pacientes = new List<Paciente>
            {
                new Paciente { Id = 10, Email = "paciente1@correo.com", Nombre = "Juan", Apellido = "Pérez" },
                new Paciente { Id = 20, Email = "paciente2@correo.com", Nombre = "María", Apellido = "López" }
            };

            var medicos = new List<Medico>
            {
                new Medico { Id = 1, Nombre = "Dr. Simi" }
            };

            // Creamos los repositorios fake
            var citaRepoFake = new CitaRepositoryFake(citasEsperadas);
            var pacienteRepoFake = new PacienteRepositoryFake(pacientes);
            var medicoRepoFake = new MedicoRepositoryFake(medicos);
            var observerFake = new CitaObserverFake();

            // Servicios reales inyectando los fakes (¡AQUÍ inyectamos los 4 a tu CitaService!)
            var citaService = new CitaService(citaRepoFake, pacienteRepoFake, medicoRepoFake, observerFake);

            // Asumiendo que tus servicios de Paciente y Médico solo piden su propio repo
            var pacienteService = new PacienteService(pacienteRepoFake);
            var medicoService = new MedicoService(medicoRepoFake);

            var controller = new CitaController(citaService, pacienteService, medicoService);

            // Simular usuario admin logueado
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, "jorge@admin.com") };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            return controller;
        }

        [Fact]
        public void Index_ConCuentaAdmin_RegresaTodasLasCitasSinFiltrar()
        {
            // Arrange
            var controller = CrearControllerConDatosDePrueba(out var citasEsperadas);

            // Act
            var resultado = controller.Index() as ViewResult;

            // Evaluamos contra tu CitaViewModel en lugar de Cita
            var modelo = resultado?.Model as List<CitaViewModel>;

            // Assert
            Assert.NotNull(modelo);
            Assert.Equal(citasEsperadas.Count, modelo.Count);
            Assert.Equal(citasEsperadas[0].Id, modelo[0].Id); // Verificamos que trajo la primera cita
        }

        [Fact]
        public void Index_ConCuentaAdmin_IncluyeCitasDeMasDeUnPaciente()
        {
            // Arrange
            var controller = CrearControllerConDatosDePrueba(out _);

            // Act
            var resultado = controller.Index() as ViewResult;
            var modelo = resultado?.Model as List<CitaViewModel>;

            // Assert — el admin debe ver citas de distintos pacientes, evaluando el NombrePaciente de tu ViewModel
            Assert.NotNull(modelo);
            var pacientesDistintos = modelo.Select(c => c.NombrePaciente).Distinct().Count();
            Assert.True(pacientesDistintos > 1);
        }

        [Fact]
        public void Index_ConCuentaAdmin_CargaCatalogosDePacientesYMedicosEnViewBag()
        {
            // Arrange
            var controller = CrearControllerConDatosDePrueba(out _);

            // Act
            controller.Index();

            // Assert
            Assert.NotNull(controller.ViewBag.Pacientes);
            Assert.NotNull(controller.ViewBag.Medicos);
        }
    }
}