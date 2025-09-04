# Project.Kafka

Microservicio de ejemplo con .NET 8 + Apache Kafka (Confluent Cloud) usando Clean Architecture.

## Arquitectura del Proyecto

- **Project.Kafka.Api**: API REST que produce órdenes a Kafka
- **Project.Kafka.Common**: Configuración y constantes compartidas
- **Project.Kafka.Core**: Entidades y contratos de dominio
- **Project.Kafka.Infrastructure**: Implementación de base de datos (EF Core SQLite), Kafka y repositorios
- **Project.Kafka.Worker**: Worker que consume mensajes de Kafka y los guarda en base de datos

---

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Cuenta en [Confluent Cloud](https://confluent.cloud/) (Kafka)
- SQLite (opcional para ver la base de datos)
- (Opcional) `dotnet-ef` global tool:

```bash
dotnet tool install --global dotnet-ef
```

---

## ⚙️ Configuración inicial

### 1. Clonar el repositorio

```bash
git clone https://github.com/luisfbn/Project.Kafka.git
cd project.kafka
```

### 2. Crear archivo de configuración

Copia el archivo de ejemplo:

```bash
cp Project.Kafka.Api/appsettings.json Project.Kafka.Api/appsettings.Development.json
cp Project.Kafka.Worker/appsettings.json Project.Kafka.Worker/appsettings.Development.json
```

Edita el archivo y coloca tus credenciales de Kafka:

```json
{
  "AppSettings": {
    "Environment": "Development",
    "Database": {
      "ConnectionString": "Data Source=../Database/orders.db"
    },
    "Kafka": {
      "BootstrapServers": "",
      "SaslUsername": "",
      "SaslPassword": ""
    }
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## ▶️ Ejecución

### 1. Ejecutar API

```bash
cd Project.Kafka.Api
dotnet run
```

Abrirá Swagger en: https://localhost:7165/swagger  
Desde ahí puedes enviar órdenes a Kafka.

### 2. Ejecutar Worker

```bash
cd Project.Kafka.Worker
dotnet run
```

Consumirá las órdenes del topic y las guardará en la base de datos.

---

## 📄 Endpoints

- `POST /orders` Envía una orden al topic Kafka

```json
{
  "product": "Teclado",
  "quantity": 2
}
```

---

## 🧪 Pruebas

- Probar envío desde Swagger o bien desde el archivo `Project.Kafka.Api.http` en el proyecto `Project.Kafka.Api`

---

## ☁️ Notas de Confluent Cloud

- Si aún no tienes cuenta, regístrate aquí: https://confluent.cloud/
- Asegúrate de crear una API Key, habilitar SASL/PLAIN y permitir conexiones externas.

---

## 📁 Estructura de Carpetas

```plaintext
📁 Project.Kafka
├── 📁 Api
├── 📁 Common
├── 📁 Core
├── 📁 Infrastructure
├── 📁 Worker
└── 📁 Database (se crea localmente, no se incluye en el repo)
```

---

## 📬 Contacto

luisfbn
