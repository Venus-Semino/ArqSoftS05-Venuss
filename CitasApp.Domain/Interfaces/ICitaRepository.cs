using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface ICitaRepository
    {
        IEnumerable<Cita> GetAll();
        Cita? GetById(int id);
        IEnumerable<Cita> GetByPacienteId(int pacienteId);
        void Add(Cita cita);
    }
}