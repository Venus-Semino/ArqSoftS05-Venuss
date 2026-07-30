using Citas_App.Domain.Models;

namespace Citas_App.Domain.Interfaces;

public interface IPacienteRepository
{
    List<Paciente> GetAll();
    Paciente? GetById(int id);
    void Add(Paciente paciente);
}