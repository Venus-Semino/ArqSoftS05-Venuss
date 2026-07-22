using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class CsvCitaRepository : ICitaRepository
    {
        private readonly string _filePath = "Data/citas.csv";

        public CsvCitaRepository()
        {
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "Id,PacienteId,MedicoId,Fecha,Hora,Motivo,Estado\n");
        }

        // ── Helpers ─────────────────────────────────────────────────────────────

        private List<Cita> LeerTodos()
        {
            var lista = new List<Cita>();
            if (!File.Exists(_filePath)) return lista;

            foreach (var linea in File.ReadAllLines(_filePath).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var p = linea.Split(',');
                if (p.Length < 7) continue;

                lista.Add(new Cita
                {
                    Id = int.Parse(p[0]),
                    PacienteId = int.Parse(p[1]),
                    MedicoId = int.Parse(p[2]),
                    Fecha = DateOnly.ParseExact(p[3], "yyyy-MM-dd"),
                    Hora = TimeOnly.ParseExact(p[4], "HH:mm"),
                    Motivo = p[5],
                    Estado = p[6]
                });
            }

            return lista;
        }

        private void EscribirTodos(List<Cita> citas)
        {
            var lineas = new List<string> { "Id,PacienteId,MedicoId,Fecha,Hora,Motivo,Estado" };

            foreach (var c in citas)
            {
                lineas.Add($"{c.Id},{c.PacienteId},{c.MedicoId},{c.Fecha:yyyy-MM-dd},{c.Hora:HH:mm},{Limpiar(c.Motivo)},{Limpiar(c.Estado)}");
            }

            File.WriteAllLines(_filePath, lineas);
        }

        private static string Limpiar(string texto) => (texto ?? string.Empty).Replace(",", ";");

        // ── Port Implementation ──────────────────────────────────────────────────

        public IEnumerable<Cita> GetAll() => LeerTodos();

        public Cita? GetById(int id) => LeerTodos().FirstOrDefault(c => c.Id == id);

        public IEnumerable<Cita> GetByPacienteId(int pacienteId) => LeerTodos().Where(c => c.PacienteId == pacienteId);

        public void Add(Cita cita)
        {
            var citas = LeerTodos();
            cita.Id = citas.Count > 0 ? citas.Max(c => c.Id) + 1 : 1;
            citas.Add(cita);
            EscribirTodos(citas);
        }
    }
}