// CitasApp.Infrastructure/Repositories/SqliteCitaRepository.cs
// Adapter de salida — implementa ICitaRepository usando SQLite
//
// Comparte el mismo archivo .db con los demás repositorios Sqlite.
// Pasa la misma ruta dbPath desde Program.cs.

using Citas_App.Domain.Interfaces;
using Citas_App.Domain.Models;
using Microsoft.Data.Sqlite;

namespace Citas_App.Infrastructure.Repositories
{
    public class SqliteCitaRepository : ICitaRepository
    {
        private readonly string _connectionString;

        public SqliteCitaRepository(string dbPath)
        {
            _connectionString = $"Data Source={dbPath}";
            InicializarTabla();
        }

        private void InicializarTabla()
        {
            using var conn = Conectar();
            Ejecutar(conn, @"
                CREATE TABLE IF NOT EXISTS Citas (
                    Id         INTEGER PRIMARY KEY AUTOINCREMENT,
                    PacienteId INTEGER NOT NULL,
                    MedicoId   INTEGER NOT NULL,
                    Fecha      TEXT    NOT NULL,   -- yyyy-MM-dd
                    Hora       TEXT    NOT NULL,   -- HH:mm
                    Motivo     TEXT,
                    Estado     TEXT    NOT NULL DEFAULT 'Pendiente'
                );");
        }

        // ── Helpers ─────────────────────────────────────────────────────────────

        private SqliteConnection Conectar()
        {
            var conn = new SqliteConnection(_connectionString);
            conn.Open();
            return conn;
        }

        private static void Ejecutar(SqliteConnection conn, string sql)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        private static Cita LeerFila(SqliteDataReader r) => new Cita
        {
            Id = r.GetInt32(0),
            PacienteId = r.GetInt32(1),
            MedicoId = r.GetInt32(2),
            Fecha = r.GetString(3),
            FechaHora = r.GetString(4),
            Motivo = r.IsDBNull(5) ? string.Empty : r.GetString(5),
            Estado = r.GetString(6)
        };

        // ── Port ────────────────────────────────────────────────────────────────

        public List<Cita> GetAll()
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText =
                "SELECT Id, PacienteId, MedicoId, Fecha, Hora, Motivo, Estado FROM Citas;";

            var lista = new List<Cita>();
            using var r = cmd.ExecuteReader();
            while (r.Read()) lista.Add(LeerFila(r));
            return lista;
        }

        public Cita? GetById(int id)
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText =
                "SELECT Id, PacienteId, MedicoId, Fecha, Hora, Motivo, Estado " +
                "FROM Citas WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$id", id);

            using var r = cmd.ExecuteReader();
            return r.Read() ? LeerFila(r) : null;
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId)
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText =
                "SELECT Id, PacienteId, MedicoId, Fecha, Hora, Motivo, Estado " +
                "FROM Citas WHERE PacienteId = $pid;";
            cmd.Parameters.AddWithValue("$pid", pacienteId);

            var lista = new List<Cita>();
            using var r = cmd.ExecuteReader();
            while (r.Read()) lista.Add(LeerFila(r));
            return lista;
        }

        public List<Cita> ObtenerPorMedico(int medicoId)
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText =
                "SELECT Id, PacienteId, MedicoId, Fecha, Hora, Motivo, Estado " +
                "FROM Citas WHERE MedicoId = $mid;";
            cmd.Parameters.AddWithValue("$mid", medicoId);

            var lista = new List<Cita>();
            using var r = cmd.ExecuteReader();
            while (r.Read()) lista.Add(LeerFila(r));
            return lista;
        }

        public void ActualizarEstado(int id, string estado)
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Citas SET Estado = $estado WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$estado", estado);
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
        }

        public void Add(Cita cita)
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Citas (PacienteId, MedicoId, Fecha, Hora, Motivo, Estado)
                VALUES ($pid, $mid, $fecha, $hora, $motivo, $estado);";
            cmd.Parameters.AddWithValue("$pid", cita.PacienteId);
            cmd.Parameters.AddWithValue("$mid", cita.MedicoId);
            cmd.Parameters.AddWithValue("$fecha", cita.Fecha ?? string.Empty);
            cmd.Parameters.AddWithValue("$hora", cita.FechaHora ?? string.Empty);
            cmd.Parameters.AddWithValue("$motivo", cita.Motivo ?? string.Empty);
            cmd.Parameters.AddWithValue("$estado", cita.Estado ?? "Pendiente");
            cmd.ExecuteNonQuery();
        }

        public void ConfirmarCita(int id)
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText =
                "UPDATE Citas SET Estado = 'Confirmada' WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
