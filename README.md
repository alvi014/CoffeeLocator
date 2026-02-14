# ☕ CoffeeLocator — Full-Stack Coffee Experience

<p align="center">
  <img src="https://img.shields.io/badge/.NET%208-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/Angular%2017-DD0031?style=for-the-badge&logo=angular&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" />
  <img src="https://img.shields.io/badge/Clean%20Architecture-black?style=for-the-badge" />
</p>

🚧 **Status:** Work in Progress (~65% Complete) 

**CoffeeLocator** is a full-stack application for coffee lovers to discover local coffee shops, manage favorites, and leave authentic reviews. The project is built following **professional architectural standards**, with a strong focus on **Clean Architecture**, **security**, and **scalability**, keeping Backend and Frontend structurally aligned.

---

## 📋 Table of Contents
- [📦 Overview](#-overview)
- [🏗️ System Architecture](#️-system-architecture)
- [📁 Project Structure](#-project-structure)
- [🚀 Development Progress](#-development-progress)
- [🧪 Core Endpoints](#-core-endpoints)
- [⚙️ Installation](#️-installation)
- [📫 Contact](#-contact)

---

## 📦 Overview

CoffeeLocator is a centralized system where:
- Users can discover nearby coffee shops
- Rate and review experiences
- Interact with a secure, API-first backend

### ⭐ Key Features
- 📍 **Geolocation Search:** Distance-based filtering using the **Haversine formula**
- 🗺️ **Interactive Map:** Built with **Leaflet + OpenStreetMap**
- ⭐ **Smart Ratings:** Automatic average calculation based on user reviews
- 🔐 **JWT Authentication:** Secure access using claims-based identity
- 🧱 **Clean Architecture:** Business rules isolated from frameworks

---

## 🏗️ System Architecture

The project follows **Clean Architecture principles on both Backend and Frontend**, ensuring low coupling and long-term maintainability.

### 🔹 Backend (.NET 8)

**Domain Layer** — Core business entities and contracts
- Core entities (`CoffeeShop`, `Review`, `User`, `Visit`, `Achievement`)
- Repository interfaces
- Business rules and enums

**Application Layer** — Business logic and use cases
- Use cases and services
- DTOs and validation
- Service interfaces (Auth, JWT, CoffeeShop)
- Geolocation calculations (Haversine)

**Infrastructure Layer** — External concerns
- EF Core persistence with SQL Server
- Repository implementations
- External services (Google Places API)
- Security services (JWT, BCrypt)
- Database migrations

**API Layer** — HTTP interface
- Controllers (Auth, CoffeeShop, Reviews)
- Exception middleware
- Dependency injection configuration

---

### 🔹 Frontend (Angular 17)

The frontend mirrors backend layers to keep responsibilities aligned:

**Core** — Application-wide services and infrastructure
- Guards (AuthGuard)
- Interceptors (JWT)
- Authentication services

**Data** — Data access layer
- Services for external communication (CoffeeShopService)

**Features** — Feature modules
- Auth (login, register)
- Coffee map (interactive Leaflet map)
- Home

**Shared** — Reusable components and contracts
- Models (aligned with backend DTOs)
- Header component
- Common UI elements

---

## 📁 Project Structure

### Backend Structure
```
CoffeeLocator/
├── CoffeeLocator.Api/
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── CoffeeShopController.cs
│   │   └── ReviewsController.cs
│   ├── Middleware/
│   │   └── ExceptionMiddleware.cs
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── Program.cs
│   └── appsettings.json
│
├── CoffeeLocator.Application/
│   ├── DTOs/
│   │   ├── Auth/
│   │   │   ├── AuthDtos.cs
│   │   │   ├── LoginDto.cs
│   │   │   └── RegisterDto.cs
│   │   ├── CoffeeShops/
│   │   │   ├── CoffeeShopDetailDto.cs
│   │   │   ├── CoffeeShopNearbyDto.cs
│   │   │   └── CreateCoffeeShopDto.cs
│   │   └── Reviews/
│   │       ├── CreateReviewDto.cs
│   │       └── ReviewResponseDto.cs
│   ├── Interfaces/
│   │   ├── IAuthService.cs
│   │   ├── ICoffeeShopService.cs
│   │   ├── IJwtService.cs
│   │   └── IPasswordHasher.cs
│   ├── Services/
│   │   ├── AuthService.cs
│   │   └── CoffeeShopService.cs
│   └── Validators/
│       ├── CreateCoffeeShopValidator.cs
│       ├── CreateReviewValidator.cs
│       └── RegisterRequestValidator.cs
│
├── CoffeeLocator.Domain/
│   ├── Common/
│   │   └── BaseEntity.cs
│   ├── Entities/
│   │   ├── Achievement.cs
│   │   ├── CoffeeShop.cs
│   │   ├── Review.cs
│   │   ├── User.cs
│   │   └── Visit.cs
│   ├── Enums/
│   │   └── UserRole.cs
│   └── Interfaces/
│       ├── IAchievementRepository.cs
│       ├── ICoffeeShopRepository.cs
│       ├── IReviewRepository.cs
│       ├── IUnitOfWork.cs
│       ├── IUserRepository.cs
│       └── IVisitRepository.cs
│
└── CoffeeLocator.Infrastructure/
    ├── ExternalServices/
    │   └── GooglePlacesService.cs
    ├── Migrations/
    │   └── [EF Core Migrations]
    ├── Persistence/
    │   ├── Configurations/
    │   │   └── VisitConfiguration.cs
    │   ├── AppDbContext.cs
    │   └── UnitOfWork.cs
    ├── Repositories/
    │   ├── AchievementRepository.cs
    │   ├── CoffeeShopRepository.cs
    │   ├── ReviewRepository.cs
    │   ├── UserRepository.cs
    │   └── VisitRepository.cs
    ├── Security
    │   ├── BCryptPasswordHasher.cs
    │   ├── CurrentUserService.cs
    │   └── JwtService.cs
    ├── CoffeeLocator.Infrastructure.csproj
    └── DependencyInjection.cs
```

### Frontend Structure
```
CoffeeLocator-Frontend/
├── public/
│   └── favicon.ico
├── src/
│   ├── app/
│   │   ├── core/
│   │   │   ├── guards/
│   │   │   │   ├── auth-guard.spec.ts
│   │   │   │   └── auth-guard.ts
│   │   │   ├── interceptors/
│   │   │   │   └── jwt.interceptor.ts
│   │   │   └── services/
│   │   │       └── auth/
│   │   │           ├── auth.service.ts
│   │   │           └── auth.services.spec.ts
│   │   ├── data/
│   │   │   └── services/
│   │   │       └── coffee-shop.service.ts
│   │   ├── features/
│   │   │   ├── auth/
│   │   │   │   └── login/
│   │   │   │       ├── login.component.css
│   │   │   │       ├── login.component.html
│   │   │   │       └── login.component.ts
│   │   │   ├── coffee-map/
│   │   │   │   ├── coffee-map.component.css
│   │   │   │   ├── coffee-map.component.html
│   │   │   │   └── coffee-map.component.ts
│   │   │   ├── home/
│   │   │   │   ├── home.component.css
│   │   │   │   ├── home.component.html
│   │   │   │   └── home.component.ts
│   │   │   └── register/
│   │   │       ├── register.component.css
│   │   │       ├── register.component.html
│   │   │       └── register.component.ts
│   │   ├── shared/
│   │   │   ├── components/
│   │   │   │   └── header/
│   │   │   │       ├── header.component.css
│   │   │   │       ├── header.component.html
│   │   │   │       └── header.component.ts
│   │   │   └── models/
│   │   │       ├── achievement.model.ts
│   │   │       ├── coffee-shop.model.ts
│   │   │       ├── review.model.ts
│   │   │       └── visit.model.ts
│   │   ├── app.component.html
│   │   ├── app.component.ts
│   │   ├── app.config.ts
│   │   ├── app.css
│   │   └── app.routes.ts
│   ├── index.html
│   ├── main.ts
│   └── styles.css
├── angular.json
├── package.json
├── tailwind.config.js
└── tsconfig.json
```

---

## 🚀 Development Progress

### ✅ Completed (65%)

#### Backend
- ✅ Clean Architecture structure (Domain, Application, Infrastructure, API)
- ✅ Entity models with audit fields and soft delete
- ✅ Repository pattern with Unit of Work
- ✅ JWT authentication and authorization
- ✅ Password hashing with BCrypt
- ✅ Geolocation service with Haversine formula
- ✅ Google Places API integration
- ✅ FluentValidation for DTOs
- ✅ Exception middleware
- ✅ EF Core migrations and database setup
- ✅ Core CRUD operations (CoffeeShop, Review)

#### Frontend
- ✅ Angular 17 project with Clean Architecture
- ✅ TypeScript models aligned with backend
- ✅ AuthService with dev login support
- ✅ JWT interceptor for automatic token injection
- ✅ CoffeeShopService with resilient error handling
- ✅ Interactive Leaflet map with custom popups
- ✅ Dynamic map reload on movement
- ✅ Login and register components
- ✅ Header navigation component
- ✅ Tailwind CSS integration

### 🚧 In Progress (35%)

#### Backend
- ⏳ Visit tracking system
- ⏳ Achievement system
- ⏳ User profile management
- ⏳ Advanced filtering and search
- ⏳ Image upload service
- ⏳ Caching layer

#### Frontend
- ⏳ Coffee shop detail page
- ⏳ Review submission form
- ⏳ User profile page
- ⏳ Favorites management
- ⏳ Achievement display
- ⏳ Advanced map filters
- ⏳ Responsive mobile design
- ⏳ Loading states and error boundaries

---

## 🧪 Core Endpoints

| Category | Endpoint | Description |
|----------|----------|-------------|
| Auth | `POST /api/Auth/register` | User registration |
| Auth | `POST /api/Auth/login` | JWT token generation |
| Shops | `GET /api/CoffeeShops/nearby` | List shops by proximity |
| Shops | `GET /api/CoffeeShops/{id}` | Get shop details |
| Shops | `POST /api/CoffeeShops` | Create coffee shop (Auth) |
| Reviews | `POST /api/Reviews` | Create review (Auth required) |
| Reviews | `GET /api/Reviews/shop/{id}` | Get reviews for shop |

---

## ⚙️ Installation

### Backend (.NET 8)
```bash
git clone https://github.com/alvi014/CoffeeLocator.git
cd CoffeeLocator

# Configure SQL Server connection in appsettings.json
# Update connection string and Google Places API key

dotnet ef database update --project CoffeeLocator.Infrastructure --startup-project CoffeeLocator.Api
dotnet run --project CoffeeLocator.Api
```

Backend will run on `http://localhost:5224`

### Frontend (Angular 17)
```bash
cd CoffeeLocator-Frontend
npm install
ng serve
```

Frontend will run on `http://localhost:4200`

---

## 📫 Let's Connect!

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://linkedin.com/in/álvaro-víctor-zamora-385965210)
[![Instagram](https://img.shields.io/badge/Instagram-E4405F?style=for-the-badge&logo=instagram&logoColor=white)](https://instagram.com/alvi014)
[![Email](https://img.shields.io/badge/Email-D14836?style=for-the-badge&logo=gmail&logoColor=white)](mailto:alvarovictor06@gmail.com)
[![WhatsApp](https://img.shields.io/badge/WhatsApp-25D366?style=for-the-badge&logo=whatsapp&logoColor=white)](https://wa.me/50687221109)

---

*Built with ❤️ and ☕ by Álvaro Víctor Zamora*
