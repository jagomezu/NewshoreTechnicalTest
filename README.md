# Prueba técnica de ingreso a Newshore

# Objetivo

Microservicio para calcular rutas de viaje desde un origen a un servicio.

En esta versión, se implementa microservicio con API expuesta para calcular rutas de viaje desde un origen a un servicio.

La respuesta permite visualizar todas las posibles rutas desde el origen al destino solicitados, incluyendo vuelos directos, vuelos con escalas, trayectos de ida únicamente, y trayectos de ida y regreso.

#Proceso de instalación

1. Publicar base de datos desde proyecto Newshore.TechnicalTest.DataBase.

2. Ajustar parámetros de configuración en archivo appsettings.json en proyecto Newshore.TechnicalTest.Api.
	
	* Cadena de conexión a base de datos.
	
	* URL de API de obtención de rutas. Se utiliza la opción Rutas múltiples y de retorno.
	
	* Cantidad máxima de viajes a utilizar en cada ruta (Se usa el doble de este valor en las rutas de ida y vuelta).

#Implementación realizada
1. Proyecto de base de datos de SQL Server

2. Microservicio en .Net Core 6 con capa de aplicación (API), capa de dominio (Business), capa de infraestructura (DataAccess) y capa transversal

3. Inyección de dependencias

4. MediatR para ejecución de comandos

5. CQRS

6. Manejo de Entity Framework

7. Logs de aplicación con SeriLog

8. Inicio de implementación de pruebas unitarias con XUnit.
	
#Implementación pendiente

1. Continuar implementación de pruebas unitarias.

2. Iniciar implementación de pruebas de integración.