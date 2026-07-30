namespace Citas_App.Domain.Factories;

using Citas_App.Domain.Models;

public class CitaFactory
{
    public Cita Construir(int pacienteId, int medicoId, DateOnly fecha, TimeOnly hora, string motivo)
    {
        return new Cita
        {
            PacienteId = pacienteId,
            MedicoId = medicoId,
            Fecha = fecha.ToString("yyyy-MM-dd"),
            FechaHora = hora.ToString("hh:mm tt"),
            Motivo = motivo,
            Estado = "Pendiente"
        };
    }
}
