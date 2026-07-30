// CitasApp.Infrastructure/Repositories/CsvMedicoRepository.cs
// Adapter de salida — implementa IMedicoRepository leyendo un archivo CSV

using Citas_App.Domain.Interfaces;
using Citas_App.Domain.Models;

namespace Citas_App.Infrastructure.Repositories
{
    public class CsvMedicoRepository : IMedicoRepository
    {
        private readonly string _filePath;

        public CsvMedicoRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "Id,Nombre,Apellido,Especialidad,NumeroLicencia\n");
        }

        // ── Helpers ───────────────────────────────────────────────────-──────────

        private List<Medico> LeerTodos()
        {
            var lista = new List<Medico>();

            foreach (var linea in File.ReadAllLines(_filePath).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var p = linea.Split(',');
                if (p.Length < 5) continue;

                lista.Add(new Medico
                {
                    Id = int.Parse(p[0]),
                    Nombre = p[1],
                    Apellido = p[2],
                    Especialidad = p[3],
                    NumeroLicencia = p[4]
                });
            }

            return lista;
        }

        private static string Limpiar(string texto) =>
            (texto ?? string.Empty).Replace(",", ";");

        // ── Port ────────────────────────────────────────────────────────────────

        public List<Medico> GetAll() => LeerTodos();

        public Medico? GetById(int id) =>
            LeerTodos().FirstOrDefault(m => m.Id == id);

        public void Add(Medico medico)
        {
            var medicos = LeerTodos();
            medico.Id = medicos.Count > 0 ? medicos.Max(m => m.Id) + 1 : 1;

            File.AppendAllText(_filePath,
                $"{medico.Id},{Limpiar(medico.Nombre)},{Limpiar(medico.Apellido)}," +
                $"{Limpiar(medico.Especialidad)},{Limpiar(medico.NumeroLicencia)}\n");
        }
    }
}
