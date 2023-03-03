# ApiGenerica

Version del proyecto 1.0
Net core 6.0 soporte a largo plazo
Aplicacion Web ASP.NET Core (Modelo - Vista - Controlador)

## Descripcion del proyecto

Este proyecto compone una serie de proyectos para poder utilizar microservicios y apis de tal manera que interectuan en el mismo esquema.
Se compone por:

## API General
El api que administra los controllers recibe las peticios http, administra la config del proyecto, administra TokenMiddleware para lass peticiones y tiene el archivo con whitelist
de los endpoint que no requieren token, devuelve un objeto serializado para cada peticion http en formato Json de la siguiente forma:
    {
	    "message": "Peticion Exitosa",
        "statusCode": 0,
        "data": [{}]
    }

## AccesControl

Este es el proyecto que controla toda la parte de los tokens, recibe la peticion de crearlosm validar tiempo de vida, y si se requiere esta preparado par funcionar con REDIS, para guardar los token 
y permitir solo una conexion por usuario, manage de tokens, desactivarlos etc.
Tiene configurado los TokenMiddleware para verificar que las peticiones pasen primero para identificar si estan en exclusion de token o requieren validacion de credenciales 
 Nuggets instalados:

     DeviceDetector.NET.NetCore
     Microsoft.AspNetCore.Http
     Microsoft.IdentityModel.Tokens
     Newtonsoft.Json
     Microsoft.NETCore.App
     System.IdentityModel.Tokens.Jwt

## ServicesApi

Es donde adminstra los services y las interfaces para poder realizar la logica de programacion ( se  planea dividirlo en Blogig y metodos genericos)
(No se instalaron Nuggets hasta el momento).

## ModelsApi

Es el proyecto que contiene todas las clases de modelos necesarios para funcionar en el API, se tiene desde el objeto de response hasta los modelos de las diferentes peticiones que se realicen.
(No se instalaron Nuggets hasta el momento).

## DAO

Se crea para administrar las conexiones a la BD, se crean metodos genericos para las principales operaciones con Psql en oracle, se planea ir aumentando los metodos pero se entiende el objetivo.
Se crea clase OracleDataReaderExtensions para automatizar la lectura del cursor que devuelven los SP de oracle (los sp tiene una estructura explicita de response)
Se crean clases para poder agregar parametros exclusivos que los SP necesitan para ejecutarse.
Se crea clase ModelResponseBD para manipular la respuesta explicita de los SP de oracle.

    Nuggets Instalados:
    Microsoft.Extensions.Configuration.Abstractions
    Microsoft.Extensions.Options
    Oracle.ManagedDataAccess.Core
    System.Configuration.ConfigurationManager

## Creador
Este proyecto es creado por el Ing. Javier Rayon Martinez.
Lo libero bajo licencia (CC BY-NC-SA) (Se pide que si es utilizado de forma total o parcial), se comunique a mi persona y se otorgue el reconocimiento pertinente.
Autorizo el uso bajo esta licencia para fines de practica o educacion, no para uso comercial (salvo previa autorizacion de mi persona).

## "Recolectar Datos es el primer paso a la sabiduria, pero compartir informacion es el primer paso a la comunidad" (Comercial IBM con referencia a Linux)
