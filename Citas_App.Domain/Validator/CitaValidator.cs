namespace CitasApp.Domain.Validators;

using CitasApp.Domain.Models;

public class CitaValidator
{
    public bool EsValida(Cita cita) =>
        cita.PacienteId > 0 &&
        cita.MedicoId > 0 &&
        !string.IsNullOrWhiteSpace(cita.Fecha) &&
        !string.IsNullOrWhiteSpace(cita.Motivo);

    public IEnumerable<string> ObtenerErrores(Cita cita)
    {
        if (cita.PacienteId <= 0) yield return "Paciente requerido.";
        if (cita.MedicoId <= 0) yield return "Médico requerido.";
        if (string.IsNullOrWhiteSpace(cita.Fecha)) yield return "Fecha requerida.";
        if (string.IsNullOrWhiteSpace(cita.Motivo)) yield return "Motivo requerido.";
    }
}
