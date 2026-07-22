using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Repositories
{
    public static class RepositoryFactory
    {
        public static IPacienteRepository CrearPacienteRepository(string entorno)
        {
            return entorno switch
            {
                "Production" => new CsvPacienteRepository(), // En produccion usa CSV
                _ => new CsvPacienteRepository() // En desarrollo usa CSV (o pon JsonPacienteRepository)
            };
        }

        public static IMedicoRepository CrearMedicoRepository(string entorno)
        {
            return entorno switch
            {
                "Production" => new CsvMedicoRepository(),
                _ => new CsvMedicoRepository()
            };
        }

        public static ICitaRepository CrearCitaRepository(string entorno)
        {
            return entorno switch
            {
                "Production" => new CsvCitaRepository(),
                _ => new CsvCitaRepository()
            };
        }
    }
}