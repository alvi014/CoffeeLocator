# 📑 Sprint 1: Full-Stack Foundation & Clean Architecture

**Status:** Completed ✅ | **Date:** February 02, 2026

## 🎯 Sprint Objective
Establish the core technical infrastructure, ensuring seamless communication between the Backend (.NET 8) and Frontend (Angular 17) using Clean Architecture standards.

## 🏗️ Technical Achievements

### Backend (Core & API)
- **Clean Architecture Implementation:** Strict separation of concerns into layers: Domain, Application, Infrastructure, and API.
- **Security:** Implemented **JWT (JSON Web Tokens)** authentication and password hashing using **BCrypt**.
- **Data Layer:** Configured **Entity Framework Core** with the **Unit of Work** pattern to ensure transactional integrity.

### Frontend (SPA)
- **Architecture:** Singleton pattern for global services and feature-based folder organization.
- **Interactive Map:** Integrated **Leaflet.js** with lazy loading of markers based on camera movement (`moveend`).
- **Resilience:** Implemented HTTP Interceptors and error handling with **Mock Data** for offline development continuity.

## 🧪 Tests Performed
- [x] Successful SQL Server connection from the API.
- [x] Automatic JWT injection in Angular request headers.
- [x] Dynamic popup rendering on the map with real Backend data.

## 💡 Key Learnings
- Resolved Leaflet icon rendering conflicts within the Angular component lifecycle.
- Maintained DTO (C#) and Interface (TypeScript) symmetry to ensure a strong contract between FE and BE.