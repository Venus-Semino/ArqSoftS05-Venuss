using CitasApp.Application.Interfaces;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services;

public class MedicoService : IMedicoService
{
    private readonly IMedicoRepository _repo;

    public MedicoService(IMedicoRepository repo) => _repo = repo;

    public List<Medico> GetAll() => _repo.GetAll();
    public Medico? GetById(int id) => _repo.GetById(id);
    public void Add(Medico medico) => _repo.Add(medico);
}
