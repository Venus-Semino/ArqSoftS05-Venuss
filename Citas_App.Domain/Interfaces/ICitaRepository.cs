using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces;

public interface ICitaRepository
{
    List<Cita> GetAll();
    Cita? GetById(int id);
    void Add(Cita cita);
    List<Cita> ObtenerPorPaciente(int pacienteId);
    List<Cita> ObtenerPorMedico(int medicoId);
    void ActualizarEstado(int id, string estado);
}