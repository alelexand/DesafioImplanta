# DesafioImplanta

**.NET 6** REST API for registering professionals, with Brazilian CPF (taxpayer ID) validation, **SQL Server** persistence through **Entity Framework Core**, and **Swagger** documentation.

## Endpoints

| Method | Route | Description |
|---|---|---|
| `GET` | `/api/Profissional?nome=` | Lists professionals, with an optional name filter |
| `GET` | `/api/Profissional/{id}` | Gets a professional by id |
| `POST` | `/api/Profissional` | Registers a professional (validates the CPF) |
| `PUT` | `/api/Profissional/{id}` | Updates a professional |
| `DELETE` | `/api/Profissional/{id}` | Deletes a professional |

Fields: full name, date of birth, sex, CPF, postal code (CEP), city, income (optional) and active status.

## Getting started

1. Set the `DevConnection` connection string in `DesafioImplanta/appsettings.json` to your SQL Server.
2. Create the database from the migrations:
   ```bash
   dotnet ef database update --project DesafioImplanta
   ```
3. Run the API:
   ```bash
   dotnet run --project DesafioImplanta
   ```
4. Open Swagger at `https://localhost:<port>/swagger`.

## Tech stack

- C# / .NET 6 / ASP.NET Core Web API
- Entity Framework Core 6 + SQL Server
- Swashbuckle (Swagger)
