# CitasApp - Sistema de Gestión Médica y API REST

## Descripción
CitasApp es una aplicación web y API REST desarrollada en ASP.NET Core MVC (.NET 10). Originalmente construida bajo un modelo tradicional, el proyecto ha sido **refactorizado hacia una Arquitectura Hexagonal (Puertos y Adaptadores)** y enriquecido con **Patrones de Diseño GoF**. Permite administrar información de pacientes, médicos y citas médicas de manera altamente escalable.

## Funcionalidades y Patrones de Diseño (GoF)
El proyecto destaca por la implementación de patrones de diseño de la industria para resolver problemas de arquitectura:
- **Factory Pattern:** Un `RepositoryFactory` decide qué motor de base de datos instanciar (CSV o Memoria) dependiendo del entorno (`Development` vs `Production`).
- **Decorator Pattern:** Un `LoggingPacienteRepository` envuelve al repositorio original para imprimir *logs* en la consola durante las operaciones CRUD, sin modificar el código base (Open/Closed Principle).
- **Observer Pattern:** Un sistema de notificaciones (`ICitaObserver`) reacciona y envía correos electrónicos (simulados en consola) de forma totalmente desacoplada cada vez que se registra una nueva cita médica.
- **Swap Adapter:** Capacidad de cambiar el motor de persistencia cambiando una sola línea de código, garantizando el aislamiento del Dominio.

## 🛠️ Arquitectura y Estructura del Proyecto
El flujo respeta la Arquitectura Hexagonal (`Web/API → Application → Domain ← Infrastructure`):
- **Domain:** Entidades puras y Puertos (Interfaces `ICitaObserver`, `IPacienteRepository`).
- **Application (Servicios):** Orquesta los casos de uso (`CitaService`). Inyecta el Observer para disparar eventos sin acoplarse a la infraestructura.
- **Infrastructure:** Adaptadores de Salida (Repositorios, Patrones Factory, Decorators y Observers físicos).
- **Web / API:** Adaptadores de Entrada (Controladores API MVC).
- **Docs (ADRs):** *Architecture Decision Records*, justificando las decisiones de la API REST y los Patrones de Diseño.

## Modelado de Datos
La estructura de nuestras entidades de dominio (Pacientes, Médicos y Citas) y cómo se relacionan entre sí está documentada visualmente.
**[Ver Diagrama de Clases interactivo](ClassDiagram.md)**

## Tecnologías Utilizadas
- ASP.NET Core MVC & Web API (.NET 10)
- **Patrones GoF** (Factory, Decorator, Observer)
- C# (LINQ, Inyección de Dependencias)
- HTML5, CSS3, Bootstrap 5

``
## Cláusura de IA
Este proyecto fue desarrollado con fines académicos para poner en práctica los conceptos de Arquitectura de Software y el patrón de diseño Repositorio en aplicaciones ASP.NET Core MVC.
De igual manera se miplementó uso de inteligencia artificial para resolver errores de compilación.
