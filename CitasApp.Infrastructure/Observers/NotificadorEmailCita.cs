using System;
using CitasApp.Domain.Interfaces; // Apuntamos a la interfaz correcta del Dominio
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Observers
{
        public class NotificadorEmailCita : ICitaObserver
    {
        public void NotificarCitaCreada(Cita cita)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[EMAIL ENVIADO]¡Nueva cita agendada! Paciente {cita.PacienteId} para el {cita.Fecha} a las {cita.Hora}.");
            Console.ResetColor();
        }
    }
}