// CitasApp.Infrastructure/Repositories/CsvPacienteRepository.cs
// Adapter de salida — implementa IPacienteRepository leyendo un archivo CSV

using Citas_App.Domain.Interfaces;
using Citas_App.Domain.Models;

namespace Citas_App.Infrastructure.Repositories
{
    public class CsvPacienteRepository : IPacienteRepository
    {
        private readonly string _filePath;

        public CsvPacienteRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "Id,Nombre,Apellido,Email,Telefono\n");
        }

        // ─d─ Helpers ─────────────────────────────────────────────────────────────

        private List<Paciente> LeerTodos()
        {
            var lista = new List<Paciente>();

            foreach (var linea in File.ReadAllLines(_filePath).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var p = linea.Split(',');
                if (p.Length < 5) continue;

                lista.Add(new Paciente
                {
                    Id = int.Parse(p[0]),
                    Nombre = p[1],
                    Apellido = p[2],
                    Email = p[3],
                    Telefono = p[4]
                });
            }

            return lista;
        }

        private static string Limpiar(string texto) =>
            (texto ?? string.Empty).Replace(",", ";");

        // ── Port ────────────────────────────────────────────────────────────────

        public List<Paciente> GetAll() => LeerTodos();

        public Paciente? GetById(int id) =>
            LeerTodos().FirstOrDefault(p => p.Id == id);

        public void Add(Paciente paciente)
        {
            var pacientes = LeerTodos();
            paciente.Id = pacientes.Count > 0 ? pacientes.Max(p => p.Id) + 1 : 1;

            File.AppendAllText(_filePath,
                $"{paciente.Id},{Limpiar(paciente.Nombre)},{Limpiar(paciente.Apellido)}," +
                $"{Limpiar(paciente.Email)},{Limpiar(paciente.Telefono)}\n");
        }
    }
}
