# ☕ CoffeeLocator — Full-Stack Coffee Experience

<p align="center">
  <img src="https://img.shields.io/badge/.NET%208-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/Angular%2017-DD0031?style=for-the-badge&logo=angular&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" />
  <img src="https://img.shields.io/badge/Clean%20Architecture-black?style=for-the-badge" />
</p>

🚧 **Status:** Work in Progress (Active Development)

**CoffeeLocator** is a full-stack application for coffee lovers to discover local coffee shops, manage favorites, and leave authentic reviews.  
The project is built following **professional architectural standards**, with a strong focus on **Clean Architecture**, **security**, and **scalability**, keeping Backend and Frontend structurally aligned.

---

## 📋 Table of Contents
- [📦 Overview](#-overview)
- [🏗️ System Architecture](#️-system-architecture)
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

- **Domain**
  - Core entities (`CoffeeShop`, `Review`)
  - Business rules and contracts
- **Application**
  - Use cases
  - DTOs and mapping logic
  - Geolocation calculations (Haversine)
- **Infrastructure**
  - EF Core persistence
  - SQL Server
  - External services

---

### 🔹 Frontend (Angular 17)

The frontend mirrors backend layers to keep responsibilities aligned:

- **Core**
  - Models (contracts)
  - Services
  - Interceptors
- **Features**
  - Coffee shops
  - Reviews
  - Map visualization
- **UI**
  - Components
  - Pages
  - Shared layouts

---

## 🚀 Development Progress

### 🏗️ Architecture & Structure
- Angular project restructured following **Clean Architecture**
- Clear symmetry between Backend and Frontend layers

---

### 1️⃣ Data Models (Contract Layer)
Implemented TypeScript interfaces aligned with backend entities:
- `coffee-shop.model.ts`
- `review.model.ts`
- `visit.model.ts`
- `achievement.model.ts`

These models define a strict and explicit FE–BE contract.

---

### 2️⃣ Security & Authentication
- **AuthService**
  - Implemented `devLogin()` for development-only JWT injection
  - Speeds up development without repeated manual login
- **JwtInterceptor**
  - Automatically appends:
    ```
    Authorization: Bearer <token>
    ```
  - Applied globally to HTTP requests

---

### 3️⃣ Coffee Shop Data Layer
- **CoffeeShopService**
  - Connected to `/api/coffeeshops/nearby`
- **Resilient Frontend Logic**
  - Uses `map` and `catchError`
  - Injects mock data if backend or database is unavailable
  - Allows uninterrupted frontend development

---

### 4️⃣ Interactive Map
- Integrated **Leaflet + OpenStreetMap**
- Dynamic data reload on map movement (`moveend`)
- Custom HTML/CSS popups:
  - Coffee shop image
  - Name
  - “View details” action
- Fixed Angular + Leaflet icon loading issues

---

### 🔧 Technical Cleanup
- Backend port aligned (`http://localhost:5224`)
- Removed duplicated services and unused files
- Cleaned redundant components and artifacts

---

## 🧪 Core Endpoints

| Category | Endpoint | Description |
|--------|----------|-------------|
| Auth | `POST /api/Auth/register` | User registration |
| Auth | `POST /api/Auth/login` | JWT token generation |
| Shops | `GET /api/CoffeeShops/nearby` | List shops by proximity |
| Reviews | `POST /api/Reviews` | Create review (Auth required) |

---

## ⚙️ Installation

### Backend (.NET 8)

```bash
git clone https://github.com/alvi014/CoffeeLocator.git
cd CoffeeLocator

# Configure SQL Server connection in appsettings.json
dotnet ef database update
dotnet run --project CoffeeLocator.Api
```
### 📫 Let's Connect! 
[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://linkedin.com/in/álvaro-víctor-zamora-385965210)
[![Instagram](https://img.shields.io/badge/Instagram-E4405F?style=for-the-badge&logo=instagram&logoColor=white)](https://instagram.com/alvi014) 
[![Email](https://img.shields.io/badge/Email-D14836?style=for-the-badge&logo=gmail&logoColor=white)](mailto:alvarovictor06@gmail.com)
[![WhatsApp](https://img.shields.io/badge/WhatsApp-25D366?style=for-the-badge&logo=whatsapp&logoColor=white)](https://wa.me/50687221109)






