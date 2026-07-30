using Citas_App.Domain.Interfaces;
using Citas_App.Domain.Models;

namespace Citas_App.Infrastructure.Repositories;

public class MemoriaPacienteRepository : IPacienteRepository
{
    private readonly List<Paciente> _pacientes = new()
    {
        new Paciente { Id = 1, Nombre = "Juan", Apellido = "Pérez", Email = "juan.perez@mail.com", Telefono = "8091234567" },
        new Paciente { Id = 2, Nombre = "María", Apellido = "Gómez", Email = "maria.gomez@mail.com", Telefono = "8092345678" },
        new Paciente { Id = 3, Nombre = "Luis", Apellido = "Ramírez", Email = "luis.ramirez@mail.com", Telefono = "8093456789" }
    };

    public List<Paciente> GetAll() => _pacientes;

    public Paciente? GetById(int id) => _pacientes.FirstOrDefault(p => p.Id == id);

    public void Add(Paciente paciente)
    {
        paciente.Id = _pacientes.Count == 0 ? 1 : _pacientes.Max(p => p.Id) + 1;
        _pacientes.Add(paciente);
    }
}
