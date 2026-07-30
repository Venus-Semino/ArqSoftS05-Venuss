using Citas_App.Application.Interfaces;
using Citas_App.Domain.Interfaces;
using Citas_App.Domain.Models;

namespace Citas_App.Application.Services;

public class MedicoService : IMedicoService
{
    private readonly IMedicoRepository _repo;

    public MedicoService(IMedicoRepository repo) => _repo = repo;

    public List<Medico> GetAll() => _repo.GetAll();
    public Medico? GetById(int id) => _repo.GetById(id);
    public void Add(Medico medico) => _repo.Add(medico);
}
