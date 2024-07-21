# SentinelAPI

SentinelAPI es una API RESTful construida en F# utilizando Giraffe y Entity Framework Core. Esta API permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en una entidad llamada `Sentinel`.

## ¿Por qué F#?

F# es un lenguaje de programación funcional que ofrece varias ventajas, lo que lo convierte en una opción interesante para construir aplicaciones web y APIs. Algunas de las razones por las cuales F# es una excelente elección son:

1. **Concisión y Legibilidad**: F# permite escribir código conciso y expresivo, lo que facilita la lectura y el mantenimiento del código.
2. **Inmutabilidad por Defecto**: Las estructuras de datos inmutables son la norma en F#, lo que ayuda a evitar errores comunes relacionados con el estado mutable.
3. **Funciones de Orden Superior**: F# soporta funciones de orden superior, lo que facilita la creación de abstracciones y la composición de funciones.
4. **Tipado Fuerte y Estático**: El sistema de tipos de F# es fuerte y estáticamente tipado, lo que ayuda a atrapar errores en tiempo de compilación en lugar de en tiempo de ejecución.
5. **Conciencia de la Concurrencia**: F# facilita la escritura de código concurrente y paralelo, lo que puede mejorar el rendimiento de las aplicaciones.
6. **Interoperabilidad**: F# se ejecuta en el runtime de .NET, lo que permite utilizar bibliotecas y herramientas de C# y otros lenguajes del ecosistema .NET.

Estas características hacen que F# sea ideal para construir APIs robustas y eficientes. La combinación de F# con Giraffe y Entity Framework Core en SentinelAPI proporciona una base sólida para el desarrollo web funcional y la gestión de datos.

## Tecnologías Utilizadas

- F#
- Giraffe
- Entity Framework Core
- SQL Server

## Configuración del Proyecto

### Prerrequisitos

- [.NET SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Instalación

1. Clonar el repositorio:

    ```bash
    git clone https://github.com/tu-usuario/SentinelAPI.git
    cd SentinelAPI
    ```

2. Restaurar los paquetes NuGet:

    ```bash
    dotnet restore
    ```

3. Crear la base de datos y aplicar las migraciones:

    - Asegúrate de tener un servidor SQL Server en funcionamiento.
    - Configura la cadena de conexión en el archivo `appsettings.json`:

    ```json
    {
        "ConnectionStrings": {
            "DefaultConnection": "Server=tu-servidor;Database=SentinelDB;Trusted_Connection=True;"
        }
    }
    ```

    - Ejecuta la aplicación para aplicar las migraciones:

    ```bash
    dotnet run
    ```

### Ejecución

Para ejecutar la aplicación:

```bash
dotnet run
