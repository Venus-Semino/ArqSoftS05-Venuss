# ADR-02: Incorporación de API REST y Swagger para Exposición de Datos

| Campo  | Valor |
|--------|-------|
| Autor  | [Tu Nombre] |
| Fecha  | DD/MM/AAAA |
| Estado | `Aceptado` |

---

## Contexto

El sistema *CitasApp* fue construido inicialmente con una interfaz gráfica acoplada usando vistas de Razor (MVC). Sin embargo, surgió la necesidad de permitir que clientes externos (como interfaces de consola, scripts de terminal o archivos HTML/JS independientes) pudieran consumir la lógica de negocio y los datos del sistema (ej. consultas de citas por paciente o la lógica de la calculadora). Se requería una forma estandarizada de comunicación que respetara la separación de capas de la Arquitectura Hexagonal.

---

## Decisión

Se decidió implementar **Adaptadores de Entrada basados en ASP.NET Core Web API (RESTful)** y documentar dichos endpoints utilizando **Swagger (OpenAPI)**.

### ¿Por qué?

- **REST** es el estándar absoluto de la industria para la integración de sistemas web. Su naturaleza "stateless" (sin estado) y el uso de verbos HTTP estándar (GET, POST) facilitan su consumo mediante herramientas nativas como `fetch` en JavaScript o `curl` en la terminal, resolviendo nuestro problema de interoperabilidad.
- **Swagger** genera una interfaz gráfica interactiva automáticamente a partir del código. Esto elimina la necesidad de redactar documentación manual propensa a desactualizarse y permite probar los endpoints de manera inmediata.

### Alternativas consideradas

| Alternativa | Por qué la descarté |
|-------------|---------------------|
| **SOAP / XML** | Es una tecnología heredada, demasiado verbosa (pesada) y compleja de implementar y consumir desde interfaces frontend modernas comparado con JSON. |
| **GraphQL** | Ofrece gran flexibilidad para consultas dinámicas, pero la curva de aprendizaje y la sobrecarga de configuración técnica es excesiva para los endpoints simples requeridos en este momento. |
| **gRPC** | Excelente para rendimiento interno, pero no es fácilmente consumible desde navegadores web estándar sin proxies adicionales. |

---

## Consecuencias

**✅ Lo que gano:**
- **Técnicamente:** El sistema ahora expone su dominio a cualquier plataforma externa mediante JSON. Se valida el concepto de *Puertos y Adaptadores* al tener múltiples entradas (MVC y API REST) apuntando a los mismos Servicios de Aplicación.
- **Proceso:** Swagger agiliza las pruebas de desarrollo; ya no es necesario configurar colecciones complejas en Postman para validar si un endpoint funciona.

**⚠️ Lo que sacrifico o asumo:**
- **Riesgo:** Requiere configuración estricta de políticas de seguridad CORS para permitir que clientes frontend externos puedan consumir la API sin ser bloqueados por el navegador.
- **Limitación técnica:** Se deben gestionar cuidadosamente los DTOs (Data Transfer Objects) para evitar exponer información sensible del Dominio directamente en las respuestas JSON.

## Clausura de Inteligencia Artificial
En este proyecto se implemento la inteligencia con el fin de ayudar a resolver problemas de codigo y darle un formato profesional al trabajo
