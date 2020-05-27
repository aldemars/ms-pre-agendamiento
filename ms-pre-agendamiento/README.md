# ms-pre-agendamiento

## Instalación

### 1.- Dependencias
* [.Net Core 3.1](https://dot.net/core)
* [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/?view=azure-cli-latest)
* [Docker](https://docs.docker.com/get-docker/)
* docker-compose

#### 1.1- Login en Azure
```sh
az login [--allow-no-subscriptions]
         [--identity]
         [--only-show-errors]
         [--password]
         [--service-principal]
         [--tenant]
         [--use-cert-sn-issuer]
         [--use-device-code]
         [--username]
```
o simplemente el siguiente comando y seguir las instrucciones:
```sh
az login
```
## 2.-  Environment variables
To validate environment variables execute next [env-vars-check.sh](./etc/scripts/env-vars-check.sh) script:

```
$ bash ./etc/scripts/env-vars-check.sh
 ```
Environment variables Details:

| **Env var**                   | **Mandatory** | **Description** |
|:------------------------------|:-------------:|:----------------|
| ConnectionStrings__database   |  YES          | Defines ADO.NET Connection String |
| ConnectionStrings__AppConfig  |  No           | Defines AppConfiguration Connection string. If not defined app work with local FeatureManager properties  |

### 3.- Iniciar el servicio.

Para iniciar la aplicación y sus dependencias (servicios) en un single host machine:
* Start Local:`$ docker-compose up --build -d`
* Check Start Local: `$ curl http://localhost:8080/HealthCheck`
* Stop Local: `$ docker-compose down`

Notas:
- Utilizar `$ make up` y `$ make stop` como shortcuts para iniciar y detener la applicación.
- `docker-compose logs -f` permite visualizar logs de los contenedores.
- `docker-compose up db migrations` para disponibilizar localmente solo la base de datos y migraciones.
- `docker-compose help` para más referencias.

#### 3.1 dotnet CLI
actualizar appsettings.json con el string de conección a la base de datos
```json
"ConnectionStrings": {
    "database": "<Connection str>"
  }
```
para el ambiente dev, el string de conección se puede obtener:

```sh
az keyvault secret show --name 'ConnectionStrings' --vault-name 'dev-pre-agendamiento' --query value
```
y luego ejecutar;
```sh
$ dotnet run
```
#### 3.3 Usando docker
actualizar appsettings.json con el string de conección a la base de datos
```json
"ConnectionStrings": {
    "database": "<Connection str>"
  }
```
```sh
$ docker build ms-pre-agendamiento/ -t preagendamiento:local
$ docker run -d -p 5000:80 preagendamiento:local
```
#### 3.4 Usando kubectl
actualizar manifests/deployment.yml con el string de conección a la base de datos
y la imagen
```yaml
env:
    - name: ConnectionStrings__database
      value: "<Connection str>"
```
```sh
$ kubectl create -f manifests/deployment.yml
$ kubectl create -f manifests/service.yml
```