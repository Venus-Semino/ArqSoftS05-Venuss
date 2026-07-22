using CitasApp.Aplications.DTOs;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Interfaces;

public interface ICitaService
{
    List<CitaViewModel> GetAll();
    CitaViewModel? GetById(int id);
    void Add(Cita cita);
}