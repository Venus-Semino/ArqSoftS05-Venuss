using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CitaApp.Models;
using CitaApp.Interfaces;

namespace CitaApp.Repositories
{
    public class MedicoJsonRepository : IMedicoRepository
    {
        private readonly string _filePath = "Data/medicos.json";

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