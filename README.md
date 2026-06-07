# KitsuQuestions🦊
Kitsu will question your knowledge.

Kitsu Questions is a gamified learning application designed to help test your knowledge. Built with a kitsune-themed interface, this project serves as a personal learning aid to track goals, give evaluating quizes and manage learning resources effectively. 

## Architecture & Tech Stack

This project is built using a **Clean Architecture** approach to ensure separation of concerns, scalability, and maintainability.

**Front end:**
* Blazor WebAssembly (WASM)
* Hosted online via GitHub Pages

**Back end (Web API):**
* .NET 10
* Entity Framework Core (SQL Server/Express)
* Dependency Injection & Repository Pattern (Generic Repositories)

**Testing:**
* xUnit (Unit Testing Framework)
* NSubstitute (For mocking repository interfaces)
* GitHub Actions (CI/CD for automated testing and deployment)

## Project Structure

The backend follows a strict 4-layer Clean Architecture:

* **Domain:** Contains the core business logic, Entities (e.g., Category, Goal, QuizAttempt, Resource), and Enums.
* **Application:** Contains business rules, DTOs, and Interfaces (Services and Repositories).
* **Infrastructure:** Handles data access, the EF Core `ApplicationDbContext`, database migrations, and concrete repository implementations.
* **API:** The presentation layer containing API Controllers, `Program.cs` for DI registration, and application settings.

## Getting Started

### Prerequisites

- **.NET 10 SDK** — [download from microsoft.com/net](https://dotnet.microsoft.com/download)
- **SQL Server LocalDB** — included with Visual Studio. If you're on a clean machine without VS, install via the [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads) installer (choose "LocalDB").
- **dotnet-ef CLI tool** — if you don't already have it:
```
  dotnet tool install --global dotnet-ef
```
- **Visual Studio 2022 (17.12+) or 2026** recommended. VS Code works too, but the steps below assume Visual Studio.

### First-time setup

> ⚠️ **The solution file is unusual for now.** `KitsuQuestions.slnx` lives inside the `KitsuQuestions.Api/` folder, not at the repo root. Commands below account for this.

1. **Clone the repo**
```
   git clone https://github.com/ann-johansson/KitsuQuestions.git
   cd KitsuQuestions
```

2. **Restore dependencies**
```
   dotnet restore KitsuQuestions.API\KitsuQuestions.slnx
```

3. **Trust the HTTPS development certificate** (once per machine)
```
   dotnet dev-certs https --trust
```
   Click "Yes" when Windows asks. This stops the browser from blocking `https://localhost` later.

4. **Create the database** (EF Core applies all migrations to LocalDB)
```
   dotnet ef database update --project KitsuQuestions.Infrastructure --startup-project KitsuQuestions.API
```

5. **Verify the build**
```
   dotnet build KitsuQuestions.API\KitsuQuestions.slnx
```

### Running the app in Visual Studio

The app has two projects that need to run together: the API (`KitsuQuestions.API`) and the Blazor client (`KitsuQuestions.BlazorClient`).

1. Open `KitsuQuestions.API\KitsuQuestions.slnx` in Visual Studio.
2. Right-click the solution in Solution Explorer → **Configure Startup Projects…**
3. Choose **Multiple startup projects**.
4. Set both `KitsuQuestions.API` and `KitsuQuestions.BlazorClient` to **Start**. Make sure the API starts before Blazor.
5. Press **F5**.

The API opens Swagger at `https://localhost:7190/swagger`; the Blazor client opens at `https://localhost:7118`.

### Troubleshooting

- **"Cannot connect to the API"** in the browser → the API project isn't running. Check that *both* projects launched (you should see two console windows).
- **Browser warns about an untrusted certificate** → step 3 wasn't applied. Re-run `dotnet dev-certs https --trust`.
