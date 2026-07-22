using System;
using System.Collections.Generic;
using System.Linq;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    // DECORATOR — agrega logging sin modificar el repositorio original
    public class LoggingPacienteRepository : IPacienteRepository
    {
        private readonly IPacienteRepository _inner;

        public LoggingPacienteRepository(IPacienteRepository inner)
        {
            _inner = inner;
        }

        public IEnumerable<Paciente> GetAll()
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] LOG: ObtenerTodos Pacientes — inicio");
            var resultado = _inner.GetAll().ToList();
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] LOG: ObtenerTodos Pacientes — {resultado.Count} registros obtenidos");
            return resultado;
        }

        public Paciente? GetById(int id)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] LOG: GetById({id}) Paciente — inicio");
            var resultado = _inner.GetById(id);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] LOG: GetById({id}) Paciente — {(resultado != null ? "encontrado" : "no encontrado")}");
            return resultado;
        }

        public void Add(Paciente paciente)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] LOG: Add() Paciente — Intentando guardar a {paciente.Nombre}");
            _inner.Add(paciente);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] LOG: Add() Paciente — Guardado exitosamente");
        }
    }
}