using System.Text.Json;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class MedicoJsonRepository : IMedicoRepository
    {
        private readonly string _filePath = "Data/medicos.json";

        public void Add(Medico medico)
        {
            var medicos = GetAll().ToList();

            medico.Id = medicos.Any() ? medicos.Max(m => m.Id) + 1 : 1;
            medicos.Add(medico);

            string json = JsonSerializer.Serialize(medicos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public IEnumerable<Medico> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Medico>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Medico>>(json) ?? new List<Medico>();
        }

        public Medico? GetById(int id)
        {
            return GetAll().FirstOrDefault(m => m.Id == id);
        }
    }
}