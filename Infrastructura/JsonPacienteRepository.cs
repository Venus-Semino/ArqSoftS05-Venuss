using System.Text.Json;
using Citas_App.Domain.Interfaces;
using Citas_App.Domain.Models;
using Microsoft.AspNetCore.Hosting;

namespace Citas_App.Infrastructure.Repositories;
//sd
public class JsonPacienteRepository : IPacienteRepository
{
    private readonly string _filePath;

    public JsonPacienteRepository(IWebHostEnvironment env)
    {
        var root = env.WebRootPath ?? Path.Combine(env.ContentRootPath, "data");
        _filePath = env.WebRootPath != null
            ? Path.Combine(root, "data", "Pacientes.json")
            : Path.Combine(root, "Pacientes.json");
    }

    public List<Paciente> GetAll()
    {
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Paciente>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Paciente>();
    }

    public Paciente? GetById(int id) => GetAll().FirstOrDefault(p => p.Id == id);

    public void Add(Paciente paciente)
    {
        var pacientes = GetAll();
        paciente.Id = pacientes.Max(p => p.Id) + 1;
        pacientes.Add(paciente);
        File.WriteAllText(_filePath, JsonSerializer.Serialize(pacientes, new JsonSerializerOptions { WriteIndented = true }));
    }
}
