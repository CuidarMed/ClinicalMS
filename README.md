# ClinicalMS

ClinicalMS es el microservicio encargado de **gestionar la historia clínica y los registros médicos de los pacientes** dentro del sistema CuidarMed.  
Permite almacenar, consultar y actualizar información clínica, asegurando integridad y confidencialidad, y proporcionando datos esenciales a otros microservicios como `SchedulingMS`, `AuthMS` y `DirectoryMS`.

---

## 🧾 Funcionalidades

ClinicalMS centraliza datos médicos de pacientes, incluyendo:

- **Antecedentes médicos**  
- **Consultas médicas y encuentros clínicos**  
- **Recetas médicas**  
- **Resultados y archivos adjuntos**  
- **Notas y observaciones clínicas**    

Permite operaciones CRUD sobre estos registros y proporciona endpoints RESTful para otros microservicios y clientes.

---

## ⚙️ Tecnologías

- **.NET 9 / ASP.NET Core**  
- **Entity Framework Core** para acceso a base de datos SQL Server  
- **SQL Server** como gestor de base de datos  
- **FluentValidation** para validaciones de modelos  
- **Swagger / OpenAPI** para documentación de APIs  
- **Docker** para desarrollo y despliegue  
- **CORS** y localización (`es-US`)  

---
## 💾 Base de datos

- **SQL Server** como sistema gestor de base de datos.

| Tabla | Descripción |
|-------|-------------|
| `Antedecents` | Registra los antecedentes médicos de cada paciente (categoría, descripción, fechas y estado) |
| `Prescriptions` | Registra recetas médicas asociadas a pacientes, médicos y consultas; incluye medicación, dosis, frecuencia, duración e instrucciones adicionales |
| `Encounters` | Registra cada encuentro médico del paciente, vinculado a citas; contiene diagnóstico, examen subjetivo/objetivo, evaluación, plan y notas |
| `Attachments` | Archivos adjuntos relacionados a un encuentro o paciente (informes, estudios, imágenes), con nombre, tipo y URL de almacenamiento |

---
## 🔗 Integración con otros microservicios

- **SchedulingMS**: Vincula citas médicas con la historia clínica y los encuentros médicos.  
- **AuthMS**: Control de permisos y roles para acceso a datos sensibles.  
- **DirectoryMS**: Obtención de información de pacientes y médicos.  

---
## 🚀 Instalación

1. Clonar el repositorio:

```bash
https://github.com/CuidarMed/ClinicalMS.git
```
```bash
cd ClinicalMS
```
2. Levantar el servicio con Docker Compose:
```bash
dotnet docker compose up --build
```
3. Si no usas Docker -> Configurar la cadena de conexión en appsettings.json:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=ClinicalDB;User Id=sa;Password=TuPassword123!;"
}

```
4. Aplicar migraciones
```bash
dotnet ef database update
```
5. Ejecutar la aplicación
```bash
dotnet run
```
6. Acceder a Swagger para explorar la API:
- Si usas Docker
```bash
http://localhost:8084/swagger/index.html
```
- Si usas appsettings.json. El puerto (5001) va a variar según los que tengas disponibles
```bash
https://localhost:5001/swagger
```
---

