using System.Text.Json;
using CitaApp.Models;
using CitaApp.Interfaces;

namespace CitaApp.Repositories
{
    public class PacienteJsonRepository : IPacienteRepository
    {
        private readonly string _filePath = "Data/pacientes.json";

        public IEnumerable<Paciente> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Paciente>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Paciente>>(json) ?? new List<Paciente>();
        }

        public Paciente? GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        public void Add(Paciente paciente)
        {
            var pacientes = GetAll().ToList();

            paciente.Id = pacientes.Any() ? pacientes.Max(p => p.Id) + 1 : 1;
            pacientes.Add(paciente);

            string json = JsonSerializer.Serialize(pacientes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}