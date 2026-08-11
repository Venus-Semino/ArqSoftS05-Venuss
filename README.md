# CITAAPP

![.NET Core](https://img.shields.io/badge/.NET%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![ASP.NET MVC](https://img.shields.io/badge/ASP.NET%20MVC-0058e6?style=for-the-badge&logo=asp.net&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)

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

## Cómo ejecutar el proyecto en local

Al no requerir un motor de base de datos externo como **SQL Server** o **PostgreSQL**, el proyecto puede ejecutarse de forma rápida sin configuraciones adicionales de bases de datos.

### Opción A: Desde la terminal (CLI)

Esta opción es recomendada para entornos **Linux, macOS y Windows**.

1. Clona el repositorio:

   ```bash
   git clone <URL_DEL_REPOSITORIO>
   ```

2. Navega hasta la carpeta raíz del proyecto, donde se encuentra el archivo `CitaApp.csproj`.

3. Restaura las dependencias:

   ```bash
   dotnet restore
   ```

4. Ejecuta la aplicación:

   ```bash
   dotnet run
   ```

5. La terminal mostrará la URL donde se está ejecutando la aplicación. Generalmente será una dirección similar a:

   ```text
   http://localhost:5000
   ```

   o:

   ```text
   https://localhost:5001
   ```

6. Abre la URL indicada en tu navegador.

### Opción B: Desde Visual Studio

1. Abre el archivo de solución `CitaApp.slnx` o el proyecto `CitaApp.csproj` en **Visual Studio**.

2. Verifica que esté instalada la carga de trabajo:

   **Desarrollo web y ASP.NET**

3. Ejecuta el proyecto utilizando el botón **Run** o presionando `F5`.

4. Visual Studio compilará el proyecto y abrirá la aplicación en el navegador configurado.

## Pruebas

Para ejecutar las pruebas y validaciones del proyecto desde la línea de comandos, utiliza:

```bash
dotnet test
```

Este comando compila la solución y ejecuta las pruebas automatizadas configuradas en el proyecto.

## Registro de cambios de esta revisión

### Estructura MVC completada

Se implementó la estructura final de **ASP.NET Core MVC**, incluyendo los controladores:

* `CitaController`
* `MedicoController`
* `PacienteController`

Cada controlador cuenta con sus respectivas vistas desarrolladas mediante **Razor (`.cshtml`)**.

### Inyección de dependencias

Se configuró la **inyección de dependencias** en `Program.cs` para registrar los repositorios JSON mediante sus respectivas interfaces:

* `ICitaRepository` → `CitaJsonRepository`
* `IMedicoRepository` → `MedicoJsonRepository`
* `IPacienteRepository` → `PacienteJsonRepository`

Esto permite desacoplar los controladores de las implementaciones concretas de persistencia.

### Vistas responsivas

Se integró **Bootstrap 5** dentro de:

```text
wwwroot/lib/bootstrap/
```

Esto permite que la interfaz se adapte a diferentes tamaños de pantalla, incluyendo dispositivos móviles y equipos de escritorio.

### Validaciones del lado del cliente

Se integró `jquery.validate.unobtrusive` para realizar validaciones en los formularios de creación y edición antes de enviar la información al servidor.

### Archivos JSON iniciales

Se generaron los archivos JSON base dentro del directorio `Data/`:

```text
Data/
├── medicos.json
├── pacientes.json
└── citas.json
```

Estos archivos permiten que el sistema cuente con una estructura de almacenamiento inicial y evita errores relacionados con la lectura de archivos durante la primera ejecución.
