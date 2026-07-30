using Citas_App.Application.DTOs;
using Citas_App.Domain.Models;

namespace Citas_App.Application.Interfaces;

public interface ICitaService
{
    List<CitaViewModel> GetAll();
    CitaViewModel? GetById(int id);
    void Add(Cita cita);
    List<CitaViewModel> ObtenerPorPaciente(int pacienteId);
    List<CitaViewModel> ObtenerPorMedico(int medicoId);
    void ActualizarEstado(int id, string estado);
}
