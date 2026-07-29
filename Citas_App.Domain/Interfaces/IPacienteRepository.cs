using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces;

public interface IPacienteRepository
{
    List<Paciente> GetAll();
    Paciente? GetById(int id);
    void Add(Paciente paciente);
}