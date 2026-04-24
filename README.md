# 🎮 OrdSpel

<div align="center">

# 🎮 OrdSpel

**A turn-based multiplayer word game built with Blazor Server, ASP.NET Core Web API, Entity Framework Core, and SignalR**

[![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](#tech-stack)
[![Blazor](https://img.shields.io/badge/Blazor-Server-5C2D91?style=for-the-badge&logo=blazor&logoColor=white)](#tech-stack)
[![ASP.NET_Core](https://img.shields.io/badge/API-ASP.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](#tech-stack)
[![SQL_Server](https://img.shields.io/badge/Database-SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](#database-and-seed-data)
[![SignalR](https://img.shields.io/badge/Realtime-SignalR-0C80C0?style=for-the-badge&logo=signalr&logoColor=white)](#architecture)

OrdSpel is a two-player category-based word game where users create or join a session, take turns entering valid words, and compete for the highest score under shared game rules.

`Multiplayer` • `Turn-based` • `Realtime updates` • `Database-backed game state`

</div>

---

## 🚀 Project Overview
OrdSpel is a full-stack .NET project designed around a layered architecture with clear separation between UI, API, business logic, data access, and shared contracts.

> [!NOTE]
> This project combines authentication, API design, layered architecture, realtime updates, game state persistence, and automated testing in a single full-stack .NET solution.

The application allows players to:
- register and sign in
- create or join a game with a unique game code
- wait in a lobby until both players are connected
- play a turn-based word game within a selected category
- receive score updates based on letter values and game rules
- see final results and saved game history

The project stores game state in the database, which makes it possible to resume active games and keep both players synchronized from a shared source of truth.

## 🌟 Highlights
| Feature | Description |
|---|---|
| 🎯 Gameplay | Two-player turn-based word battles with category-based rules |
| 🔐 Authentication | Cookie-based authentication with ASP.NET Identity |
| 🖥️ Frontend | Interactive Blazor Server UI |
| 🌐 Backend | ASP.NET Core Web API with clean layered structure |
| 🧠 Game Logic | Validation, scoring, bonus rules, and turn handling |
| ⚡ Realtime | SignalR updates with polling fallback |
| 🗄️ Persistence | SQL Server with Entity Framework Core |
| ✅ Testing | Multiple test projects for BLL, API, DAL, and UI |

## 🖼️ Screenshots
> Replace the placeholders below with real screenshots from the project.

### Home / Game Entry
<img width="2000" height="1405" alt="Home" src="https://github.com/user-attachments/assets/b86823d0-fe0d-4d3d-b4eb-ab57965546ac" />
<img width="2000" height="1460" alt="GameEntry" src="https://github.com/user-attachments/assets/81b2d00d-e3e9-4ee3-9d13-a57eafd74666" />


### Lobby View
<img width="2000" height="1465" alt="Lobby" src="https://github.com/user-attachments/assets/a3c56596-dd52-4c5c-8ba0-f97c86df416a" />

### Active Game View
<img width="2000" height="1443" alt="ActiveGame" src="https://github.com/user-attachments/assets/5018a493-5a3f-400a-8ef9-f75b26152673" />

### Result Screen
<img width="2029" height="1442" alt="GameResult" src="https://github.com/user-attachments/assets/eaf63bec-4562-4a8c-8404-12db24ee74cc" />


## 🏗️ Architecture
OrdSpel follows a layered architecture to keep responsibilities isolated and maintainable.

### 🔄 High-level Flow
```text
Blazor Server UI
    ↓
ASP.NET Core Web API
    ↓
Business Logic Layer
    ↓
Data Access Layer / Repositories
    ↓
SQL Server Database
```

### 🧭 Architecture Diagram
> You can replace this placeholder with an exported diagram from draw.io, Excalidraw, Figma, or Visio.

![Architecture Diagram Placeholder](docs/images/architecture-diagram-placeholder.png)

### ⚡ Realtime Strategy
The application uses **SignalR** for live updates when players join or play a turn.
If SignalR is unavailable, the UI falls back to **polling every few seconds**, ensuring the game remains functional.

## 🧰 Tech Stack
### Frontend
- .NET 10
- Blazor Server
- Razor Components
- SignalR Client

### Backend
- ASP.NET Core Web API
- ASP.NET Identity
- Entity Framework Core
- SignalR
- SQL Server

### Testing
- xUnit
- bUnit
- Integration testing
- BDD/UI-oriented test projects

## 📁 Solution Structure
```text
OrdSpel.sln
|- OrdSpel/                Blazor Server UI
|- OrdSpel.API/            ASP.NET Core Web API
|- OrdSpel.BLL/            Business logic and game rules
|- OrdSpel.DAL/            EF Core, entities, repositories, seed data
|- OrdSpel.Shared/         DTOs, enums, shared constants
|- OrdSpel.API.Test/       API integration tests
|- OrdSpel.BLL.Test/       Business logic unit tests
|- OrdSpel.DAL.Test.xUnit/ Data layer tests
|- OrdSpel.DAL.Test.BDD/   BDD-related tests
|- OrdSpel.UI.Test/        Blazor UI tests
```

## 📜 Gameplay Rules
The current implementation is based on these core rules:
- The game is played by two players
- Each match uses a selected category, such as **Animals**, **Countries**, or **Fruits and Vegetables**
- A starting word is selected for the session
- Players take turns entering a new word in the same category
- The new word must begin with the last letter of the previous word
- A word cannot be used more than once in the same match
- Letter scores are calculated from shared constants in `OrdSpel.Shared`
- Passing a turn gives a score penalty
- Long words grant bonus points
- The game ends after the configured round limit

> [!TIP]
> The rule values are centralized in `OrdSpel.Shared`, making them easy to reuse across the UI, API, and business logic layers.

### 🎲 Shared Game Constants
Current values in the code:
- Total rounds: `20`
- Pass penalty: `-5`
- Long word bonus: `+3`
- Long word threshold: `12`
- Hard word bonus: `+3` *(prepared in code)*

## 🛠️ Getting Started
### Prerequisites
Make sure you have the following installed:
- .NET 10 SDK
- SQL Server or SQL Server LocalDB
- Visual Studio 2026 or later with .NET tooling
- A trusted development HTTPS certificate

### 1️⃣ Clone the repository
```powershell
git clone https://github.com/MammaGula/OrdSpel
cd OrdSpel
```

### 2️⃣ Verify connection strings
The API project uses the following connection strings in `OrdSpel.API/appsettings.json`:
- `AppDbConnection`
- `AuthDbConnection`

By default, both point to the `ordspel` database on `localhost`.

### 3️⃣ Apply database migrations
```powershell
dotnet ef database update --project OrdSpel.DAL --startup-project OrdSpel.API --context AppDbContext
dotnet ef database update --project OrdSpel.DAL --startup-project OrdSpel.API --context AuthDbContext
```

### 4️⃣ Run the API
```powershell
dotnet run --project OrdSpel.API
```
Default launch URLs:
- `https://localhost:7099`
- `http://localhost:5260`

### 5️⃣ Run the UI
Open a second terminal:
```powershell
dotnet run --project OrdSpel
```
Default launch URLs:
- `https://localhost:7265`
- `http://localhost:5235`

## ⚙️ Configuration
### UI Configuration
File: `OrdSpel/appsettings.json`
- `ConnectionStrings:ApiBaseUrl`

Default value:
- `https://localhost:7099`

### API Configuration
File: `OrdSpel.API/appsettings.json`
- `ConnectionStrings:AppDbConnection`
- `ConnectionStrings:AuthDbConnection`

### 🌐 LAN Profiles
Both UI and API include launch profiles for LAN scenarios.
The solution also reads environment variables such as:
- `API_BASE_URL`
- `LAN_IP`
- `UI_PORT`

## 🗃️ Database and Seed Data
The application seeds initial data at startup to simplify development and testing.

### Categories
- Countries
- Animals
- Fruits and Vegetables

### Seeded Users
- `spelare1` / `123`
- `spelare2` / `123`
- `playwright_user` / `Test123!`

All active game state is stored in the database, making session recovery and synchronization possible.

## 🔌 API Overview
### Auth
- `POST /api/auth/register`
- `POST /api/auth/login`
- `POST /api/auth/logout`
- `GET /api/auth/me`
- `DELETE /api/auth/delete`

### Game
- `POST /api/game/create`
- `POST /api/game/join`
- `PUT /api/game/end/{gameCode}`
- `GET /api/game/{code}/lobby`
- `GET /api/game/{code}/status`
- `GET /api/game/{code}/result`
- `GET /api/game/active`
- `GET /api/game/history`

### Turns
- `POST /api/games/{code}/turns`

### SignalR Hub
- `/hubs/game`

## 🧪 Testing
The solution includes several test projects for different layers of the system:
- **OrdSpel.BLL.Test** – unit tests for business logic
- **OrdSpel.API.Test** – integration tests for API endpoints
- **OrdSpel.DAL.Test.xUnit** – data access and repository tests
- **OrdSpel.DAL.Test.BDD** – BDD-oriented tests
- **OrdSpel.UI.Test** – UI tests for Blazor components

Run all tests with:
```powershell
dotnet test
```


## 💼 Portfolio Notes
This project demonstrates:
- full-stack .NET application design
- layered architecture
- authentication and authorization with cookies
- business-rule implementation in a separate logic layer
- database-driven multiplayer game state
- realtime client updates with SignalR
- automated testing across multiple layers

---
