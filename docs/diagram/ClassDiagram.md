# Diagrama de Clases - CitasApp

A continuación se muestra el modelo de datos utilizado en la aplicación, representando las entidades principales y sus relaciones directas.

```mermaid
classDiagram
    direction UP

    class Paciente {
        +string Nombre
        +string Apellido
        +string Telefono
        +string Email
    }

    class Medico {
        +string Nombre
        +string Apellido
        +string Especialidad
    }

    class Cita {
        +int PacienteId
        +int MedicoId
        +string Fecha
        +string Hora
        +string Motivo
        +string Estado
    }

    %% Relaciones
    Cita "*" --> "1" Paciente : Pertenece a
    Cita "*" --> "1" Medico : Atendida por