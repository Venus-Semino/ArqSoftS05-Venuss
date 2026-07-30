using System.Text.Json;
using Citas_App.Domain.Interfaces;
using Citas_App.Domain.Models;
using Microsoft.AspNetCore.Hosting;

namespace Citas_App.Infrastructure.Repositories;

public class JsonCitaRepository : ICitaRepository
{
    private readonly string _filePath;

    public JsonCitaRepository(IWebHostEnvironment env)
    {
        var root = env.WebRootPath ?? Path.Combine(env.ContentRootPath, "data");
        _filePath = env.WebRootPath != null
            ? Path.Combine(root, "data", "Citas.json")
            : Path.Combine(root, "Citas.json");
    }

    public List<Cita> GetAll()
    { //no se sube
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Cita>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Cita>();
    }

    public Cita? GetById(int id) => GetAll().FirstOrDefault(c => c.Id == id);

    public void Add(Cita cita)
    {
        var citas = GetAll();
        cita.Id = citas.Max(c => c.Id) + 1;
        citas.Add(cita);
        File.WriteAllText(_filePath, JsonSerializer.Serialize(citas, new JsonSerializerOptions { WriteIndented = true }));
    }

    public List<Cita> ObtenerPorPaciente(int pacienteId) =>
        GetAll().Where(c => c.PacienteId == pacienteId).ToList();

    public List<Cita> ObtenerPorMedico(int medicoId) =>
        GetAll().Where(c => c.MedicoId == medicoId).ToList();

    public void ActualizarEstado(int id, string estado)
    {
        var citas = GetAll();
        var cita = citas.FirstOrDefault(c => c.Id == id);
        if (cita == null) return;
        cita.Estado = estado;
        File.WriteAllText(_filePath, JsonSerializer.Serialize(citas, new JsonSerializerOptions { WriteIndented = true }));
    }
}
