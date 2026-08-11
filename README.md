# CITAAPP

Sistema de gestión de citas médicas para consultorios y clínicas. Construido bajo la arquitectura MVC (Modelo-Vista-Controlador) en ASP.NET Core, utilizando un enfoque práctico de persistencia de datos mediante archivos locales JSON, ideal para entornos ligeros y de rápido despliegue.

### Arquitectura de la solución

```text
CitaApp
├── Models/              Entidades de negocio principales (Cita, Medico, Paciente)
├── Interfaces/          Contratos para el acceso a datos (Patrón Repositorio)
├── Repositories/        Implementación de lectura y escritura directa en archivos JSON
├── Controllers/         Gestión de peticiones HTTP, validaciones y conexión con las vistas
├── Data/                Directorio que actúa como base de datos local (.json)
└── Views/               Frontend responsivo en ASP.NET Core MVC (Razor + Bootstrap)
```
## Manejo y persistencia de datos

A diferencia de un sistema tradicional basado en SQL, este proyecto utiliza el **patrón Repository** para abstraer el almacenamiento y gestionar la persistencia de los datos mediante archivos JSON.

| Entidad                   | Interfaz              | Repositorio JSON         | Archivo físico        |
| ------------------------- | --------------------- | ------------------------ | --------------------- |
| **Catálogo de Médicos**   | `IMedicoRepository`   | `MedicoJsonRepository`   | `Data/medicos.json`   |
| **Registro de Pacientes** | `IPacienteRepository` | `PacienteJsonRepository` | `Data/pacientes.json` |
| **Agenda de Citas**       | `ICitaRepository`     | `CitaJsonRepository`     | `Data/citas.json`     |

Esta implementación permite mantener separada la lógica de negocio de la forma en que se almacenan los datos, facilitando el mantenimiento y una posible migración futura hacia otro sistema de persistencia.

