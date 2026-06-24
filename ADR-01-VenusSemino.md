# ADR-03: Implementación de Patrones de Diseño (GoF) en Infraestructura y Aplicación

| Campo  | Valor |
|--------|-------|
| Autor  | Venus Semino |
| Fecha  | 23/06/2026 |
| Estado | `Aceptado` |

---

## Contexto

A medida que el sistema *CitasApp* creció, surgió la necesidad de resolver tres problemas arquitectónicos sin romper los principios de la Arquitectura Hexagonal ni los principios SOLID:
1. **Creación dinámica:** Se necesitaba instanciar diferentes repositorios dependiendo del entorno (Desarrollo o Producción) sin llenar el `Program.cs` de condicionales complejos.
2. **Registro de actividades (Logging):** Se requería registrar en consola cada vez que se interactuaba con la base de datos de pacientes, pero sin modificar el código interno de los repositorios existentes (Principio de Abierto/Cerrado).
3. **Notificaciones acopladas:** Al agendar una cita, se debía enviar una notificación por correo electrónico. Colocar la lógica del envío de correos directamente en el servicio de la aplicación violaría la separación de responsabilidades, acoplando el Dominio con la Infraestructura.

---

## Decisión

Se decidió implementar tres patrones de diseño del **Gang of Four (GoF)**:
- **Factory Pattern:** Mediante la clase estática `RepositoryFactory`, se centralizó la lógica de creación de repositorios según el entorno.
- **Decorator Pattern:** Se creó `LoggingPacienteRepository` para "envolver" al repositorio original, añadiendo los logs antes y después de la ejecución de los métodos base.
- **Observer Pattern:** Se definió la interfaz `ICitaObserver` en el Dominio y se implementó `NotificadorEmailCita` en Infraestructura para reaccionar de forma asíncrona a la creación de citas.

### ¿Por qué?

Estos patrones permiten añadir comportamientos complejos y gestionar la creación de objetos manteniendo el código altamente desacoplado. El Decorator permite cumplir con el principio OCP (Open/Closed Principle), y el Observer garantiza que la capa de Aplicación solo notifique que "algo pasó", delegando el "cómo reaccionar" a la capa de Infraestructura.

### Alternativas consideradas

| Alternativa | Por qué la descarté |
|-------------|---------------------|
| **Lógica procedural directa (Sin patrones)** | Llenar los servicios y repositorios originales con `if/else` y `Console.WriteLine` rompe la responsabilidad única y hace el código inmanejable a largo plazo. |
| **Action Filters / Middlewares (Para Logging)** | Excelente para registrar peticiones HTTP, pero no permite registrar de forma granular lo que ocurre específicamente en la capa de persistencia (repositorios). |
| **Librería MediatR (Para el Observer)** | Aunque es un estándar en la industria para eventos de dominio, agregaría una sobrecarga innecesaria de librerías de terceros para un requerimiento que se puede solucionar nativamente. |

---

## Consecuencias

**Lo que gano:**
- **Técnicamente:** El sistema es completamente modular. Puedo quitar los logs o cambiar el sistema de notificaciones sin tocar una sola línea de la lógica de negocio (`CitaService`).
- **Proceso:** Facilita las pruebas (testing), ya que cada componente (fábrica, decorador, observador) se puede probar de forma aislada.

**Lo que sacrifico o asumo:**
- **Curva de aprendizaje:** Aumenta la complejidad estructural del proyecto. Los nuevos desarrolladores que entren al equipo deberán comprender cómo interactúan los patrones GoF y la Inyección de Dependencias en el contenedor de ASP.NET Core.