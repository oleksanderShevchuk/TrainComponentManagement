# 🚆 Train Component Management

A clean architecture project for managing train components using **ASP.NET Core Web API + EF Core + SQL Server**.

---

## 📦 Features

1. **List all components** with optional search (`GET /api/component?search=Wheel`)
2. **Get component details** by ID (`GET /api/component/{id}`)
3. **Create a new component** (`POST /api/component`)
4. **Assign quantity** to components that allow it (`PATCH /api/component/{id}/quantity`)
5. **Validation rules**:

   * `Quantity` must be a **positive integer**
   * Quantity can only be assigned if `CanAssignQuantity = true`

---

## 🛠 Technologies

* **.NET 8** (Web API)
* **Entity Framework Core (EF Core)**
* **SQL Server**
* **Clean Architecture** (Domain / Application / Infrastructure / API)
* **Manual DTO Mapping** (no AutoMapper)
* **Swagger** (for API testing)
* **Global Error Handling + Structured Responses**
* **Docker & Docker Compose** (API + SQL Server)

---

## 🚀 Run Locally (Without Docker)

1. Clone the repository:

   ```bash
   git clone https://github.com/oleksanderShevchuk/TrainComponentManagement.git
   cd TrainComponentManagement
   ```

2. Restore dependencies:

   ```bash
   dotnet restore
   ```

3. Configure SQL Server connection string in `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=TrainComponentsDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

4. Apply EF Core migrations:

   ```bash
   dotnet ef database update --project TrainComponentManagement.Infrastructure
   ```

5. Run the project:

   ```bash
   dotnet run --project TrainComponentManagement.Api
   ```

6. Open Swagger to test the API:

   ```
   https://localhost:5001/swagger
   ```

---

## 🐳 Run with Docker

The project supports **full containerized development**:

1. Ensure **Docker Desktop** is running

2. Build and start API + SQL Server:

   ```bash
   docker-compose up --build
   ```

3. Open Swagger:

   ```
   http://localhost:8080/swagger
   ```

4. SQL Server is accessible on port `1433`:

   ```
   Server: localhost,1433
   User: sa
   Password: YourStrong!Passw0rd
   ```

5. Data is persisted in `sql_data` volume.

---

## 📂 Project Structure

```
TrainComponentManagement/
├── TrainComponentManagement.Api/          # Web API (Controllers, Middleware)
│   └── Dockerfile                         # Dockerfile for API
├── TrainComponentManagement.Application/  # Application layer (DTOs, Services)
├── TrainComponentManagement.Domain/       # Domain layer (Entities, Interfaces)
├── TrainComponentManagement.Infrastructure/ # Data access (DbContext, Repositories)
├── TrainComponentManagement.Tests/        # xUnit tests
├── docker-compose.yml                     # API + SQL Server setup
├── .dockerignore
└── README.md
```

---

## 🌱 Database Seeding

* The database is automatically populated with **initial train components**
  **only in Development mode** using `DbInitializer`.
* In Production mode, the database will remain empty by default.

To test this feature:

1. Ensure `ASPNETCORE_ENVIRONMENT=Development`
2. Run the project for the first time
3. Check the `Components` table – it will be automatically filled

---

## ✅ Testing

* Unit tests for `ComponentService` are implemented with **xUnit + Moq**
* Run tests:

```bash
dotnet test
```
