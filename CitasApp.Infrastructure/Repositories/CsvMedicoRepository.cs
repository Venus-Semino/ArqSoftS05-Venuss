using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class CsvMedicoRepository : IMedicoRepository
    {
        private readonly string _filePath = "Data/medicos.csv";

        public CsvMedicoRepository()
        {
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "Id,Nombre,Apellido,Especialidad,NumeroLicencia\n");
        }

        // ── Helpers ─────────────────────────────────────────────────────────────

        private List<Medico> LeerTodos()
        {
            var lista = new List<Medico>();
            if (!File.Exists(_filePath)) return lista;

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

        private void EscribirTodos(List<Medico> medicos)
        {
            var lineas = new List<string> { "Id,Nombre,Apellido,Especialidad,NumeroLicencia" };
            foreach (var m in medicos)
            {
                lineas.Add($"{m.Id},{Limpiar(m.Nombre)},{Limpiar(m.Apellido)},{Limpiar(m.Especialidad)},{Limpiar(m.NumeroLicencia)}");
            }
            File.WriteAllLines(_filePath, lineas);
        }

        private static string Limpiar(string texto) => (texto ?? string.Empty).Replace(",", ";");

        // ── Port Implementation ──────────────────────────────────────────────────

        public IEnumerable<Medico> GetAll() => LeerTodos();

        public Medico? GetById(int id) => LeerTodos().FirstOrDefault(m => m.Id == id);

        public void Add(Medico medico)
        {
            var medicos = LeerTodos();
            medico.Id = medicos.Count > 0 ? medicos.Max(m => m.Id) + 1 : 1;
            medicos.Add(medico);
            EscribirTodos(medicos);
        }
    }
}