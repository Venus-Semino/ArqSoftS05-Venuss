using Citas_App.Domain.Models;

namespace Citas_App.Application.Interfaces;

public interface IMedicoService
{
    List<Medico> GetAll();
    Medico? GetById(int id);
    void Add(Medico medico);
}
