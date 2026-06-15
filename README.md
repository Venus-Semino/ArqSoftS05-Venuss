# CitasApp

## Descripción

CitasApp es una aplicación web desarrollada en ASP.NET Core MVC (.NET 10) que permite administrar información relacionada con pacientes, médicos y citas médicas. El proyecto fue realizado como parte de la práctica de los conceptos vistos en la materia, aplicando una arquitectura basada en repositorios, inyección de dependencias y almacenamiento de datos mediante archivos JSON.

## Funcionalidades

* Registro y consulta de pacientes.
* Visualización de médicos disponibles y sus especialidades.
* Creación y administración de citas médicas.
* Búsqueda y filtrado de citas por paciente.
* Almacenamiento permanente de la información utilizando archivos JSON.

## Tecnologías utilizadas

* ASP.NET Core MVC (.NET 10)
* C#
* Archivos JSON para la persistencia de datos

## Estructura del proyecto

* **Controllers/**: Contiene los controladores encargados de manejar las solicitudes y la lógica de la aplicación.
* **Models/**: Incluye las clases que representan las entidades principales del sistema (Paciente, Médico y Cita).
* **Interfaces/**: Define los contratos utilizados por los repositorios.
* **Repositories/**: Implementa las operaciones de lectura y escritura de datos en archivos JSON.
* **Views/**: Contiene las vistas desarrolladas con Razor para la interfaz de usuario.
* **Data/**: Almacena los archivos JSON que funcionan como base de datos del proyecto.


```

Este proyecto fue desarrollado con fines académicos para poner en práctica los conceptos de Arquitectura de Software y el patrón de diseño Repositorio en aplicaciones ASP.NET Core MVC.
De igual manera se miplementó uso de inteligencia artificial para resolver errores de compilación
