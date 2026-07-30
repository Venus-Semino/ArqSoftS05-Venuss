using Citas_App.Domain.Models;

namespace Citas_App.Domain.Interfaces;

public interface IMedicoRepository
{
    List<Medico> GetAll();
    Medico? GetById(int id);
    void Add(Medico medico);
}