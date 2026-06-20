#  CitasApp - Sistema de Gestión Médica y API REST

##  Descripción
CitasApp es una aplicación web y API REST desarrollada en ASP.NET Core MVC (.NET 10). Originalmente construida bajo un modelo tradicional, el proyecto ha sido **refactorizado hacia una Arquitectura Hexagonal (Puertos y Adaptadores)**. Permite administrar información de pacientes, médicos y citas médicas, demostrando el aislamiento total de la lógica de negocio respecto a la infraestructura y a las interfaces de consumo.

## Funcionalidades
- Registro, visualización y detalle de **Pacientes** y **Médicos** mediante interfaz web.
- Creación y administración de **Citas Médicas**.
- **API REST Integrada:** Endpoints para la consulta de citas filtradas por paciente y una calculadora de operaciones matemáticas.
- **Documentación Interactiva (Swagger):** La API está documentada siguiendo el estándar OpenAPI, permitiendo probar los endpoints directamente desde el navegador.
- **Swap Adapter (Persistencia Intercambiable):** Capacidad de cambiar el motor de base de datos entre Archivos JSON, Archivos CSV y Memoria RAM cambiando una sola línea de código, sin afectar la lógica.

## 🛠️ Arquitectura y Estructura del Proyecto
El proyecto cumple estrictamente con el flujo de la Arquitectura Hexagonal (`Web/API → Application → Domain ← Infrastructure`):

- ** Domain (Núcleo):** Contiene las entidades puras y los Puertos (Interfaces de repositorios). No tiene dependencias externas.
- ** Application (Servicios):** Orquesta los casos de uso (`PacienteService`, `MedicoService`, `CitaService`). Se comunica exclusivamente mediante las interfaces del Dominio.
- ** Infrastructure (Adaptadores de Salida):** Implementa las interfaces para persistir datos (`JsonRepository`, `CsvRepository`).
- ** Web / API (Adaptadores de Entrada):** Contiene los Controladores MVC (Vistas) y los Controladores API (`ControllerBase`). Habla únicamente con la capa de Aplicación.
- ** Docs (ADRs):** Contiene los *Architecture Decision Records*, donde se justifica técnica y formalmente cada decisión estructural del sistema.

## Tecnologías Utilizadas
- ASP.NET Core MVC & Web API (.NET 10)
- **Swagger / Swashbuckle** (Documentación OpenAPI)
- C# (LINQ, Inyección de Dependencias)
- xUnit (Pruebas Unitarias)
- HTML5, CSS3, Bootstrap 5

##  Documentación de la API (Swagger)
Para explorar y probar los endpoints implementados de forma profesional:
1. Ejecuta el proyecto.
2. Navega a la ruta `/swagger` en tu navegador (ej. `https://localhost:<TU_PUERTO>/swagger`).
3. Desde la interfaz gráfica de Swagger podrás interactuar con los endpoints de Citas y de la Calculadora sin necesidad de clientes externos.

También puedes probar la API desde la terminal utilizando `curl`:
```bash
curl.exe -k -s "https://localhost:<TU_PUERTO>/api/calculadora/sumar?num1=15&num2=5"
