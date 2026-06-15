using System.Collections.Generic;
using CitaApp.Models;

namespace CitaApp.Interfaces
{
    public interface ICitaRepository
    {
        IEnumerable<Cita> GetAll();
        IEnumerable<Cita> GetByPacienteId(int pacienteId);
        void Add(Cita cita);
    }
}