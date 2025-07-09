# ProjectTracker

An ASP.NET Core MVC application for managing projects and tasks, with JWT-based authentication, layered (clean) architecture, Entity Framework Core, and unit tests.

---

## Table of Contents

1. [Features](#features)  
2. [Tech Stack](#tech-stack)  
3. [Prerequisites](#prerequisites)  
4. [Getting Started](#getting-started)  
   1. [Clone the Repo](#clone-the-repo)  
   2. [Configure Secrets](#configure-secrets)  
   3. [Database Setup](#database-setup)  
   4. [Build & Run](#build--run)  
5. [Project Structure](#project-structure)  
6. [Testing](#testing)  
7. [Deployment](#deployment)  
8. [Contributing](#contributing)  
9. [License](#license)  

---

## Features

- **User Authentication**: Register & login with JWT tokens stored in secure cookies.  
- **Project CRUD**: Create, read, update, delete projects (per-user).  
- **Task CRUD**: Manage tasks under each project, with due dates and completion status.  
- **Layered Architecture**:  
  - **Domain** (entities)  
  - **Application** (DTOs, interfaces, business logic, validators)  
  - **Infrastructure** (EF Core, repositories, auth service, settings)  
  - **API** (ASP.NET Core MVC, controllers, views)  
- **Validation**: Server- and client-side (FluentValidation + unobtrusive jQuery).  
- **Unit Tests**: xUnit + Moq coverage for `ProjectService` and `TaskService`.  

---

## Tech Stack

- **.NET 8** (ASP.NET Core MVC)  
- **Entity Framework Core 8** (SQL Server)  
- **FluentValidation** (input validation)  
- **JWT** (Microsoft.AspNetCore.Authentication.JwtBearer)  
- **xUnit & Moq** (unit testing)  
- **Bootstrap 5** (UI styling)  

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- [SQL Server](https://www.microsoft.com/en-us/sql-server/) (Express or full)  
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code  

---

## Getting Started

### Clone the Repo

```bash
git clone https://github.com/JinasMuhammed/ProjectTracker.git
cd ProjectTracker
```

### Configure Secrets

1. **appsettings.json** in `ProjectTracker.Api` contains placeholders:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=ProjectTrackerDb;User Id=sa;Password=YourP@ssw0rd;..."
   },
   "Jwt": {
     "Key": "YOUR_SECRET_KEY",
     "Issuer": "ProjectTracker",
     "Audience": "ProjectTrackerUsers",
     "ExpiresInMinutes": 60
   }
   ```

2. For development, consider using [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) instead of committing sensitive values.

### Database Setup

Open **Package Manager Console** in Visual Studio, set **Default project** to **ProjectTracker.Infrastructure**, then:

```powershell
Add-Migration InitialCreate
Update-Database
```

This creates the `Users`, `Projects`, and `Tasks` tables.

### Build & Run

In Visual Studio, set **ProjectTracker.Api** as startup, then press **F5**.  
Or from CLI:

```bash
cd ProjectTracker.Api
dotnet run
```

Navigate to `https://localhost:5001` (or the URL shown) to start.

---

## Project Structure

```
ProjectTracker/
├── ProjectTracker.Api/            # MVC + controllers + views + static files
├── ProjectTracker.Application/    # DTOs, interfaces, services, validators
├── ProjectTracker.Domain/         # Core entities
├── ProjectTracker.Infrastructure/ # EF Core, repositories, AuthService, settings
├── ProjectTracker.Tests/          # xUnit + Moq unit tests
└── README.md
```

---

## Testing

From the solution root, run:

```bash
cd ProjectTracker.Tests
dotnet test
```

All tests in `ProjectTracker.Tests/Services` will execute, ensuring your service logic works.

---

## Deployment

- **Docker**: Add a `Dockerfile` in `ProjectTracker.Api` referencing the SDK and runtime images.  
- **Azure App Service**: Publish via Visual Studio or GitHub Actions.  
- **Environment Variables**: Use `ConnectionStrings__DefaultConnection` and `Jwt__Key` to override secrets in production.

---

## Contributing

1. Fork the repo  
2. Create a feature branch (`git checkout -b feature/YourFeature`)  
3. Commit your changes (`git commit -m "Add feature"`)  
4. Push to your branch (`git push origin feature/YourFeature`)  
5. Open a Pull Request  

---

## License

This project is open-source under the [MIT License](LICENSE).
