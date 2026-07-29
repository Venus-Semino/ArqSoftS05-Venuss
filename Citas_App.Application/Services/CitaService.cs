using CitasApp.Application.DTOs;
using CitasApp.Application.Interfaces;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services;

public class CitaService : ICitaService
{
    private readonly ICitaRepository _citaRepo;
    private readonly IPacienteRepository _pacienteRepo;
    private readonly IMedicoRepository _medicoRepo;

    public CitaService(ICitaRepository citaRepo, IPacienteRepository pacienteRepo, IMedicoRepository medicoRepo)
    {
        _citaRepo = citaRepo;
        _pacienteRepo = pacienteRepo;
        _medicoRepo = medicoRepo;
    }

    public List<CitaViewModel> GetAll()
    {
        var citas = _citaRepo.GetAll();
        var pacientes = _pacienteRepo.GetAll();
        var medicos = _medicoRepo.GetAll();

        return citas.Select(c => new CitaViewModel
        {
            Id = c.Id,
            NombrePaciente = pacientes.FirstOrDefault(p => p.Id == c.PacienteId)?.Nombre + " " +
                             pacientes.FirstOrDefault(p => p.Id == c.PacienteId)?.Apellido,
            NombreMedico = medicos.FirstOrDefault(m => m.Id == c.MedicoId)?.Nombre,
            Fecha = c.Fecha,
            FechaHora = c.FechaHora,
            Motivo = c.Motivo,
            Estado = c.Estado
        }).ToList();
    }

    public CitaViewModel? GetById(int id)
    {
        var cita = _citaRepo.GetById(id);
        if (cita == null) return null;

        var pacientes = _pacienteRepo.GetAll();
        var medicos = _medicoRepo.GetAll();
        var paciente = pacientes.FirstOrDefault(p => p.Id == cita.PacienteId);
        var medico = medicos.FirstOrDefault(m => m.Id == cita.MedicoId);

        return new CitaViewModel
        {
            Id = cita.Id,
            NombrePaciente = paciente?.Nombre + " " + paciente?.Apellido,
            NombreMedico = medico?.Nombre,
            Fecha = cita.Fecha,
            FechaHora = cita.FechaHora,
            Motivo = cita.Motivo,
            Estado = cita.Estado
        };
    }

    public void Add(Cita cita) => _citaRepo.Add(cita);

    public void ActualizarEstado(int id, string estado) => _citaRepo.ActualizarEstado(id, estado);

    public List<CitaViewModel> ObtenerPorPaciente(int pacienteId)
    {
        var citas = _citaRepo.ObtenerPorPaciente(pacienteId);
        var pacientes = _pacienteRepo.GetAll();
        var medicos = _medicoRepo.GetAll();

        return citas.Select(c => new CitaViewModel
        {
            Id = c.Id,
            NombrePaciente = pacientes.FirstOrDefault(p => p.Id == c.PacienteId)?.Nombre + " " +
                             pacientes.FirstOrDefault(p => p.Id == c.PacienteId)?.Apellido,
            NombreMedico = medicos.FirstOrDefault(m => m.Id == c.MedicoId)?.Nombre,
            Fecha = c.Fecha,
            FechaHora = c.FechaHora,
            Motivo = c.Motivo,
            Estado = c.Estado
        }).ToList();
    }

    public List<CitaViewModel> ObtenerPorMedico(int medicoId)
    {
        var citas = _citaRepo.ObtenerPorMedico(medicoId);
        var pacientes = _pacienteRepo.GetAll();
        var medicos = _medicoRepo.GetAll();

        return citas.Select(c => new CitaViewModel
        {
            Id = c.Id,
            NombrePaciente = pacientes.FirstOrDefault(p => p.Id == c.PacienteId)?.Nombre + " " +
                             pacientes.FirstOrDefault(p => p.Id == c.PacienteId)?.Apellido,
            NombreMedico = medicos.FirstOrDefault(m => m.Id == c.MedicoId)?.Nombre,
            Fecha = c.Fecha,
            FechaHora = c.FechaHora,
            Motivo = c.Motivo,
            Estado = c.Estado
        }).ToList();
    }
}
