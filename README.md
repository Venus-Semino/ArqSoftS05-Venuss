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
**[DIAGRAMA DE CLASES](doc/diagram/ClassDiagramñ.md)**

## Evidencias de Ejecución y Pruebas

A continuación se presentan las capturas que validan el funcionamiento de los endpoints y los patrones requeridos en las actividades.

### 1. Pruebas de la Calculadora API
Ejecución de los métodos matemáticos a través del navegador:

**Suma y Resta:**
![Prueba de Suma](doc/photos/suma.png)
![Prueba de Resta](doc/photos/resta.png)

**Multiplicación y División:**
![Prueba de Multiplicación](doc/photos/multiplicación.png)
![Prueba de División](doc/photos/división.png)

### 2. Pruebas de Endpoints API (Pacientes, Médicos, Citas)
Validación de los verbos GET HTTP retornando los JSON correspondientes:

![API Pacientes](doc/photos/api_pacientes.png)
![API Médicos](doc/photos/api_medicos.png)
![API Citas](doc/photos/api_citas.png)

### 3. Ejecución de Patrones (Factory, Decorator y Observer) - Actividad 26
Evidencia de la consola de Visual Studio demostrando la inyección de los repositorios y la ejecución asíncrona de los patrones GoF al interactuar con el sistema:

![Consola Actividad 26](doc/photos/Act26/ACT26_1.png)
![Ejecución de Patrones](doc/photos/FactoryDecoratorObserver.png)

---
## Deuda Técnica
Al revisar el proyecto , se identificaron áreas de mejora y deuda técnica que podrían abordarse en futuras iteraciones:
1. Accidental: en citaService se había convertido en una God Class, acumulando demasiada lógica de negocio. Se recomienda dividirlo en servicios más pequeños y especializados. Para futuro se va a dividir para quitar la nececidad de modificarlo direcamente ahí.
2. De infraestructura: Al querer agregar un paciente, servicio o medico, la persona lo hace manualmente , lo que puede llevar a errores. Se recomienda implementar un sistema de validación y automatización para la creación de entidades.

---

## Tecnologías Utilizadas
- ASP.NET Core MVC & Web API (.NET 10)
- **Patrones GoF** (Factory, Decorator, Observer)
- C# (LINQ, Inyección de Dependencias)
- HTML5, CSS3, Bootstrap 5

``
## Cláusura de IA
Este proyecto fue desarrollado con fines académicos para poner en práctica los conceptos de Arquitectura de Software y el patrón de diseño Repositorio en aplicaciones ASP.NET Core MVC.
De igual manera se miplementó uso de inteligencia artificial para resolver errores de compilación.
