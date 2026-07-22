using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface IMedicoRepository
    {
        IEnumerable<Medico> GetAll();
        Medico? GetById(int id);
        void Add(Medico medico);
    }
}