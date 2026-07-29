namespace CitasApp.Domain.Models;

public class Medico
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Especialidad { get; set; }
    public string NumeroLicencia { get; set; }
    public string Email { get; set; } = string.Empty;
}