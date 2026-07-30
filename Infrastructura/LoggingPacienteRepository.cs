using Citas_App.Domain.Interfaces;
using Citas_App.Domain.Models;

namespace Citas_App.Infrastructure.Repositories;

public class LoggingPacienteRepository : IPacienteRepository
{
    private readonly IPacienteRepository _inner;

    public LoggingPacienteRepository(IPacienteRepository inner)
    {
        _inner = inner;
    }

    public List<Paciente> GetAll()
    {
        Log("ObtenerTodos — inicio");
        var resultado = _inner.GetAll();
        Log($"ObtenerTodos — {resultado.Count} registros");
        return resultado;
    }

    public Paciente? GetById(int id)
    {
        Log($"ObtenerPorId({id}) — inicio");
        var resultado = _inner.GetById(id);
        Log($"ObtenerPorId({id}) — {(resultado != null ? "encontrado" : "no encontrado")}");
        return resultado;
    }

    public void Add(Paciente paciente)
    {
        Log($"Agregar({paciente.Nombre} {paciente.Apellido}) — inicio");
        _inner.Add(paciente);
        Log($"Agregar({paciente.Nombre} {paciente.Apellido}) — completado");
    }

    private static void Log(string mensaje) =>
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {mensaje}");
}
