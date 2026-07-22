using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface ICitaObserver
    {
        void NotificarCitaCreada(Cita cita);
    }
}