# Consultorio de Seguros - README

Este proyecto es un pequeño consultorio de seguros desarrollado utilizando ASP.NET MVC. Proporciona funcionalidades para administrar información de seguros y asegurados, así como la asignación de seguros a los clientes. Además, incluye la opción de cargar información de asegurados de forma masiva mediante la carga de un archivo.

## Requerimientos

- ASP.NET MVC 5 o ASP.NET Core MVC
- .NET Framework 4.7+ o .NET Core 5+
- Entity Framework

## Funcionalidades

El consultorio de seguros cuenta con las siguientes funcionalidades:

### CRUD de Seguros

- Crear, leer, actualizar y eliminar información de seguros.
- La información del seguro incluye nombre, código, suma asegurada y prima.
- Validaciones de los datos ingresados.

### CRUD de Asegurados

- Crear, leer, actualizar y eliminar información de asegurados.
- La información del asegurado incluye cédula, nombre, teléfono y edad.
- Validaciones de los datos ingresados.

### Carga masiva de Asegurados

- Permite cargar información de asegurados de forma masiva mediante la carga de un archivo (txt o excel).
- Procesa el archivo y guarda la información en la base de datos.
- Validaciones de los datos del archivo.

### Asignación de Seguros a Clientes

- Permite asignar uno o más seguros existentes a los clientes.
- Relaciona los seguros seleccionados con los clientes correspondientes.

### Consulta de Seguros Asociados a un Cliente

- Permite consultar los seguros asociados a un cliente a través de su número de cédula.
- Muestra los detalles de los seguros asociados al cliente.

### Consulta de Clientes Asociados a un Seguro

- Permite consultar los clientes asociados a un seguro a través de su código.
- Muestra los detalles de los clientes asociados al seguro.

## Arquitectura y Tecnologías

El proyecto sigue una arquitectura MVC (Model-View-Controller) para la separación de responsabilidades y la organización del código.

- Frontend: Para el frontend, puedes utilizar cualquier tecnología de tu elección, como Angular 2+, ReactJS, jQuery + Bootstrap, etc. La elección de la tecnología frontend queda a tu criterio.
- Backend: Utiliza ASP.NET MVC 5 o ASP.NET Core MVC como el framework backend. Elige entre .NET Framework 4.7+ o .NET Core 5+ según tus preferencias y requisitos del proyecto.
- Base de Datos: Utiliza Entity Framework como el ORM (Object-Relational Mapping) para interactuar con la base de datos. Puedes utilizar cualquier motor de base de datos compatible con Entity Framework, como SQL Server, MySQL, PostgreSQL, etc.

## Instalación y Configuración

Sigue los pasos a continuación para instalar y configurar el proyecto:

1. Clona o descarga el repositorio del proyecto.

2. Abre el proyecto en tu entorno de desarrollo.

3. Configura la conexión a la base de datos en el archivo de configuración correspondiente (por ejemplo, `appsettings.json` en ASP.NET Core).

4. Abre una terminal y navega hasta el directorio raíz del proyecto.

5. Ejecuta los comandos para restaurar las dependencias y compilar el proyecto:
   ```
   dotnet restore
   dotnet build
   ```

6. Ejecuta los com

andos para crear la base de datos y aplicar las migraciones:
   ```
   dotnet ef database update
   ```

7. Inicia la aplicación:
   ```
   dotnet run
   ```

8. Abre tu navegador web y accede a la URL local donde se está ejecutando la aplicación.

## Contribución

Si deseas contribuir a este proyecto, puedes seguir los pasos a continuación:

1. Realiza un fork del repositorio.

2. Crea una rama (branch) con la nueva funcionalidad o corrección que deseas implementar.

3. Realiza los cambios necesarios en tu rama.

4. Realiza pruebas y verifica que todo funcione correctamente.

5. Envía un pull request con tus cambios.

6. Espera a que tus cambios sean revisados y fusionados con la rama principal.

## Conclusiones

El consultorio de seguros es un proyecto básico que demuestra el uso de ASP.NET MVC para desarrollar una aplicación de administración de seguros y asegurados. Puedes personalizar y ampliar las funcionalidades según tus necesidades específicas.

Si tienes alguna pregunta o enfrentas algún problema durante la configuración o desarrollo del proyecto, no dudes en buscar ayuda en los recursos de documentación de ASP.NET, Entity Framework o las comunidades de desarrollo relacionadas.

¡Disfruta desarrollando el consultorio de seguros!
