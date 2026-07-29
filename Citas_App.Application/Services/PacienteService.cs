using CitasApp.Application.Interfaces;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services;

public class PacienteService : IPacienteService
{
    private readonly IPacienteRepository _repo;

    public PacienteService(IPacienteRepository repo) => _repo = repo;

    public List<Paciente> GetAll() => _repo.GetAll();
    public Paciente? GetById(int id) => _repo.GetById(id);
    public void Add(Paciente paciente) => _repo.Add(paciente);
}
