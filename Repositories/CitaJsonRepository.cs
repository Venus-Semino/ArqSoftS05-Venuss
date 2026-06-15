using System.Text.Json;
using CitaApp.Models;
using CitaApp.Interfaces;

namespace CitaApp.Repositories
{
    public class CitaJsonRepository : ICitaRepository
    {
        private readonly string _filePath = "Data/citas.json";

        public IEnumerable<Cita> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Cita>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Cita>>(json) ?? new List<Cita>();
        }

        public IEnumerable<Cita> GetByPacienteId(int pacienteId)
        {
            return GetAll().Where(c => c.PacienteId == pacienteId);
        }

        public void Add(Cita cita)
        {
            var citas = GetAll().ToList();

            cita.Id = citas.Any() ? citas.Max(c => c.Id) + 1 : 1;
            citas.Add(cita);

            string json = JsonSerializer.Serialize(citas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}