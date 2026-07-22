using CitasApp.Aplications.DTOs;
using CitasApp.Application.Interfaces;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services;

public class CitaService : ICitaService
{
    private readonly ICitaRepository _citaRepo;
    private readonly IPacienteRepository _pacienteRepo;
    private readonly IMedicoRepository _medicoRepo;
    private readonly ICitaObserver _observer;

    public CitaService(ICitaRepository citaRepo, IPacienteRepository pacienteRepo, IMedicoRepository medicoRepo, ICitaObserver observer)
    {
        _citaRepo = citaRepo;
        _pacienteRepo = pacienteRepo;
        _medicoRepo = medicoRepo;
        _observer = observer;
    }

    private CitaViewModel ConstruirViewModel(Cita cita, IEnumerable<Paciente> pacientes, IEnumerable<Medico> medicos)
    {
        var paciente = pacientes.FirstOrDefault(p => p.Id == cita.PacienteId);
        var medico = medicos.FirstOrDefault(m => m.Id == cita.MedicoId);

        return new CitaViewModel
        {
            Id = cita.Id,
            NombrePaciente = paciente != null ? $"{paciente.Nombre} {paciente.Apellido}" : "Desconocido",
            NombreMedico = medico?.Nombre ?? "Desconocido",
            Fecha = cita.Fecha.ToString(),
            FechaHora = cita.Hora.ToString(),
            Motivo = cita.Motivo,
            Estado = cita.Estado
        };
    }

    public List<CitaViewModel> GetAll()
    {
        var citas = _citaRepo.GetAll();
        var pacientes = _pacienteRepo.GetAll();
        var medicos = _medicoRepo.GetAll();

        return citas.Select(c => ConstruirViewModel(c, pacientes, medicos)).ToList();
    }

    public CitaViewModel? GetById(int id)
    {
        var cita = _citaRepo.GetById(id);
        if (cita == null) return null;

        var pacientes = _pacienteRepo.GetAll();
        var medicos = _medicoRepo.GetAll();

        return ConstruirViewModel(cita, pacientes, medicos);
    }

    public List<CitaViewModel> GetByPacienteId(int pacienteId)
    {
        var citas = _citaRepo.GetByPacienteId(pacienteId);
        var pacientes = _pacienteRepo.GetAll();
        var medicos = _medicoRepo.GetAll();

        return citas.Select(c => ConstruirViewModel(c, pacientes, medicos)).ToList();
    }

    public void Add(Cita cita)
    {
        _citaRepo.Add(cita);
        _observer.NotificarCitaCreada(cita);
    }
}