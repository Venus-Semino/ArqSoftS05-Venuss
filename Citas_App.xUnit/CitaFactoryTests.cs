using Citas_App.Domain.Factories;
using Xunit;

namespace CitasApp.Tests;

public class CitaFactoryTests
{
    private readonly CitaFactory _factory = new();

    [Fact]
    public void Construir_ConDatosValidos_CreaCitaConEstadoPendiente()
    {
        // Arrange
        var fecha = new DateOnly(2026, 7, 20);
        var hora = new TimeOnly(10, 0);

        // Act
        var cita = _factory.Construir(pacienteId: 1, medicoId: 2, fecha: fecha, hora: hora, motivo: "Consulta");

        // Assert
        Assert.Equal("Pendiente", cita.Estado);
    }

    [Fact]
    public void Construir_ConDatosValidos_AsignaPacienteYMedicoCorrectos()
    {
        // Arrange & Act
        var cita = _factory.Construir(pacienteId: 3, medicoId: 5,
            fecha: new DateOnly(2026, 8, 1), hora: new TimeOnly(14, 30), motivo: "Revisión");

        // Assert
        Assert.Equal(3, cita.PacienteId);
        Assert.Equal(5, cita.MedicoId);
    }

    [Fact]
    public void Construir_ConDatosValidos_GuardaMotivoCorrectamente()
    {
        // Arrange
        const string motivo = "Dolor de cabeza";

        // Act
        var cita = _factory.Construir(1, 1, new DateOnly(2026, 7, 21), new TimeOnly(9, 0), motivo);

        // Assert
        Assert.Equal(motivo, cita.Motivo);
    }
}
