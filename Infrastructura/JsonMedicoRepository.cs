using System.Text.Json;
using Citas_App.Domain.Interfaces;
using Citas_App.Domain.Models;
using Microsoft.AspNetCore.Hosting;

namespace Citas_App.Infrastructure.Repositories;
//
public class JsonMedicoRepository : IMedicoRepository
{
    private readonly string _filePath;

    public JsonMedicoRepository(IWebHostEnvironment env)
    {
        var root = env.WebRootPath ?? Path.Combine(env.ContentRootPath, "data");
        _filePath = env.WebRootPath != null
            ? Path.Combine(root, "data", "Medicos.json")
            : Path.Combine(root, "Medicos.json");
    }

    public List<Medico> GetAll()
    {
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Medico>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Medico>();
    }

    public Medico? GetById(int id) => GetAll().FirstOrDefault(m => m.Id == id);

    public void Add(Medico medico)
    {
        var medicos = GetAll();
        medico.Id = medicos.Max(m => m.Id) + 1;
        medicos.Add(medico);
        File.WriteAllText(_filePath, JsonSerializer.Serialize(medicos, new JsonSerializerOptions { WriteIndented = true }));
    }
}
