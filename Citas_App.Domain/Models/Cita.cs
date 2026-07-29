using System.ComponentModel.DataAnnotations;

namespace CitasApp.Domain.Models;

public class Cita
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecciona un paciente.")]
    public int PacienteId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecciona un médico.")]
    public int MedicoId { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public string Fecha { get; set; }

    public string FechaHora { get; set; }

    [Required(ErrorMessage = "El motivo es obligatorio.")]
    public string Motivo { get; set; }

    public string Estado { get; set; }
}