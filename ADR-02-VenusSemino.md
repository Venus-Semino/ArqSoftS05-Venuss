# ADR-02: Implementación de Patrones de Diseño (GoF) en Repositorios

| Campo  | Valor |
|--------|-------|
| Autor  | Venus Semino |
| Fecha  | 26/06/2026 |
| Estado | `Aceptado` |

---

## Contexto

El sistema requería dos nuevas funcionalidades para gestionar la persistencia de datos:
1. Necesitábamos cambiar el origen de datos dinámicamente según el entorno (Desarrollo o Producción) sin llenar el código de inicialización de condicionales complejos.
2. Se solicitó agregar un registro de actividades (Logs) en la consola cada vez que se consultara la información de los pacientes, pero respetando el Principio de Abierto/Cerrado (OCP), es decir, sin modificar el código original de los repositorios.

---

## Decisión

Se decidió implementar los patrones de diseño **Factory** y **Decorator**.

### ¿Por qué?

- **Factory (`RepositoryFactory`):** Centraliza la lógica de instanciación. El sistema le pide a la fábrica un repositorio y esta decide si entregar la versión JSON, CSV o en Memoria dependiendo del entorno actual, liberando al `Program.cs` de esta responsabilidad.
- **Decorator (`LoggingPacienteRepository`):** Permite "envolver" el repositorio base. Intercepta las llamadas (como `ObtenerTodos`), imprime los logs requeridos con la fecha y hora, y luego delega la ejecución real al repositorio original. Esto agrega funcionalidad de forma limpia y modular.

### Alternativas consideradas

| Alternativa | Por qué la descarté |
|-------------|---------------------|
| Agregar `Console.WriteLine` directo en `PacienteJsonRepository` | Rompe el principio de Responsabilidad Única y el Principio Abierto/Cerrado. Si agregamos más repositorios (como SQL), tendríamos que duplicar los logs en todos ellos. |
| Inyección condicional manual en `Program.cs` | Genera un alto acoplamiento y un archivo de configuración difícil de mantener a medida que el sistema crece. |
| Uso de librerías de terceros (ej. Castle Windsor para interceptores) | Es una solución excesiva (sobreingeniería) para el alcance actual del requerimiento. |

---

## Consecuencias

** Lo que gano:**
- **Técnicamente:** Alta cohesión y bajo acoplamiento. Puedo activar o desactivar los logs simplemente quitando el Decorator de la Inyección de Dependencias, sin tocar la lógica de acceso a datos.
- **Proceso:** El código es más fácil de probar y mantener, ya que cada clase hace una sola cosa.

** Lo que sacrifico o asumo:**
- **Limitación técnica:** El Decorator solo envuelve los métodos definidos explícitamente en la interfaz `IPacienteRepository`.
- **Riesgo:** Introduce mayor complejidad estructural; requiere que los nuevos desarrolladores entiendan cómo interactúan las capas de interfaces antes de modificar el código.

---

## Evidencia de Pruebas (Actividad 26)

A continuación, se muestra la evidencia de la prueba realizada mediante la terminal (PowerShell), donde se valida que el **Decorator** intercepta correctamente la petición GET y el **Factory** proporciona los datos del entorno adecuado.

![Evidencia de Prueba 1 - Terminal](photos/Act26/ACT6_1.png)