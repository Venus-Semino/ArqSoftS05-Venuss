using System.Collections.Generic;
using CitaApp.Models;

namespace CitaApp.Interfaces
{
    public interface IPacienteRepository
    {
        IEnumerable<Paciente> GetAll();
        Paciente? GetById(int id);
        void Add(Paciente paciente);
    }
}