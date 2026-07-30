using Citas_App.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace Citas_App.Infrastructure.Repositories;

public static class RepositoryFactory
{
    public static IPacienteRepository CrearPacienteRepository(string entorno, IWebHostEnvironment env)
    {
        return entorno == "Production"
            ? new MemoriaPacienteRepository()
            : new JsonPacienteRepository(env);
    }
}
