# Ingeniería de Software — Sistema de Gestión de Usuarios

Trabajo práctico de la materia Ingeniería de Software. Aplicación de escritorio en C# para la gestión de usuarios, roles, familias de permisos y bitácora de actividad, con arquitectura en capas y soporte multi-idioma.

## Funcionalidades

- Login de usuarios
- Alta, baja y modificación de usuarios
- Gestión de roles y familias de permisos
- Cambio de contraseña
- Registro de actividad (bitácora)
- Interfaz disponible en español, inglés y portugués

## Arquitectura

El proyecto está organizado en capas:

- **BE** (Business Entities): entidades del dominio
- **BLL** (Business Logic Layer): lógica de negocio y validaciones
- **DAL** (Data Access Layer): acceso y persistencia de datos
- **Interfaces**: contratos entre capas
- **IngenieriaSoftware** (UI): interfaz gráfica en Windows Forms

## Tecnologías

- C# / .NET Framework
- Windows Forms
- JSON (preferencias de idioma por usuario)

## Cómo ejecutarlo

1. Cloná el repositorio
2. Abrí `IngenieriaSoftware.sln` con Visual Studio
3. Restaurá los paquetes NuGet si Visual Studio lo solicita
4. Ejecutá el proyecto `IngenieriaSoftware` (F5)

## Estado

Proyecto académico desarrollado como trabajo práctico. Versión beta.
