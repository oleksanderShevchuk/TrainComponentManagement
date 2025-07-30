# 🚆 Train Component Management API

This is a project for managing train components.
It is implemented using **ASP.NET Core Web API + EF Core + SQL Server**.

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
* **AutoMapper** (DTO mapping)
* **Swagger** (for API testing)
* **Global Error Handling + Structured Responses**

---

## 🚀 How to Run Locally

1. Clone the repository:

   ```bash
   git clone https://github.com/oleksanderShevchuk/TrainComponentManagement.git
   cd TrainComponentManagement/TrainComponentApi
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
   dotnet ef database update
   ```

5. Run the project:

   ```bash
   dotnet run
   ```

6. Open Swagger to test the API:

   ```
   https://localhost:5001/swagger
   ```

---

## 📂 Project Structure

```
TrainComponentManagement/
│
├── TrainComponentApi/           # ASP.NET Core Web API project
│   ├── Controllers/
│   ├── Data/
│   ├── DTOs/
│   ├── Middleware/
│   ├── Models/
│   ├── Profiles/
│   ├── Responses/
│   └── ...
│
├── README.md                    # Project documentation
└── .gitignore
```


## 🌱 Database Seeding

- The database is automatically populated with **30 initial train components**  
  **only in Development mode** using `DbInitializer`.  
- This ensures that the project can be tested immediately without any manual setup.  
- In Production mode, the database will remain empty by default.  

To test this feature:
1. Ensure `ASPNETCORE_ENVIRONMENT=Development`
2. Run the project for the first time
3. Check the `Components` table – it will be automatically filled
