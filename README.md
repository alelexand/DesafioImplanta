# DesafioImplanta

API REST em **.NET 6** para cadastro de profissionais, com validação de CPF, persistência em **SQL Server** via **Entity Framework Core** e documentação com **Swagger**.

## Endpoints

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/Profissional?nome=` | Lista profissionais, com filtro opcional por nome |
| `GET` | `/api/Profissional/{id}` | Busca um profissional pelo id |
| `POST` | `/api/Profissional` | Cadastra um profissional (valida o CPF) |
| `PUT` | `/api/Profissional/{id}` | Atualiza um profissional |
| `DELETE` | `/api/Profissional/{id}` | Remove um profissional |

Campos do cadastro: nome completo, data de nascimento, sexo, CPF, CEP, cidade, renda (opcional) e status ativo.

## Como rodar

1. Ajuste a connection string `DevConnection` em `DesafioImplanta/appsettings.json` para o seu SQL Server.
2. Crie o banco com as migrations:
   ```bash
   dotnet ef database update --project DesafioImplanta
   ```
3. Rode a API:
   ```bash
   dotnet run --project DesafioImplanta
   ```
4. Abra o Swagger em `https://localhost:<porta>/swagger`.

## Tecnologias

- C# / .NET 6 / ASP.NET Core Web API
- Entity Framework Core 6 + SQL Server
- Swashbuckle (Swagger)
