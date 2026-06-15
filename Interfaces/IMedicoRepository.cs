using System.Collections.Generic;
using CitaApp.Models;

namespace CitaApp.Interfaces
{
    public interface IMedicoRepository
    {
        IEnumerable<Medico> GetAll();
        Medico? GetById(int id);
    }
}