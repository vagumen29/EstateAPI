# EstateAPI

Consideraciones de la solución:

- Version .Net 7
- Para la base de datos no es necesario realizar creación de la misma, en el proyecto API se encuentra el archivo de configuración appsettings en donde se encuentra el
  ConnectionString y la conexuón DefaultConnection que tiene la configuracion de conexion a la base de datos. Para la ejecucion en un ambiente local se debe especificar
  el nombre del servidor local Sql Server en donde se va a crear la base de datos.
- De acuerdo con las especificaciones tecnicas de la prueba se implemento identity para el manejo de la autenticación. El usuario para la generacion del token de autenticacion
  es el siguiente: truiz@test.com password: T3st2023*
- Una vez se cree la base de datos esta se poblara con datos ficticios.


