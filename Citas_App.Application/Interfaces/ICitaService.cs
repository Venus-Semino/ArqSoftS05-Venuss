using CitasApp.Application.DTOs;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Interfaces;

public interface ICitaService
{
    List<CitaViewModel> GetAll();
    CitaViewModel? GetById(int id);
    void Add(Cita cita);
    List<CitaViewModel> ObtenerPorPaciente(int pacienteId);
    List<CitaViewModel> ObtenerPorMedico(int medicoId);
    void ActualizarEstado(int id, string estado);
}
