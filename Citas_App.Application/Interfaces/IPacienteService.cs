using Citas_App.Domain.Models;

namespace Citas_App.Application.Interfaces;

public interface IPacienteService
{
    List<Paciente> GetAll();
    Paciente? GetById(int id);
    void Add(Paciente paciente);
}
