# CoffeeLocator ☕

**CoffeeLocator** is a robust RESTful API designed for coffee lovers to discover coffee shops, manage their favorites, and leave genuine reviews. Built with a Clean Architecture approach, it emphasizes maintainability and scalability.

🚀 **Project Status:** Backend Base Completed (Security + CRUD)

---

## 📋 Table of Contents
* [📦 Overview](#-overview)
* [🛠 Technologies and Tools](#-technologies-and-tools)
* [🧭 System Architecture](#-system-architecture)
* [🧪 Key Endpoints](#-key-endpoints)
* [🔐 Security and Authentication](#-security-and-authentication)
* [⚙️ Installation](#-installation)
* [📬 Contact](#-contact)

---

## 📦 Overview
This system centralizes the information of local coffee shops. Users can register to rate their experiences, ensuring data integrity and authenticity via a structured approach.

**Key Components:**
* **Authentication:** JWT-based Login/Registration system.
* **Reviews:** Manage reviews with identity protection.
* **Validation:** FluentValidation ensures proper input data.

---

## 🛠 Technologies and Tools
* **Language:** C#
* **Framework:** .NET 8.0
* **Database:** SQL Server / Entity Framework Core (Code First)
* **Security:** JWT (JSON Web Tokens) & BCrypt.Net for password hashing.
* **Documentation:** Swagger / OpenAPI
* **Validation:** FluentValidation

---

## 🏗️ Project Architecture

The project follows the principles of **Clean Architecture**, dividing responsibilities into 4 main layers:

* **Domain:** Contains business entities (`CoffeeShop`, `Review`), domain logic, and repository contracts.
* **Application:** Manages use cases, orchestration services, DTOs, and calculation logic (Haversine formula for geolocation).
* **Infrastructure:** Implements data persistence using Entity Framework Core and specific repositories.
* **API:** Decoupled REST controllers exposing endpoints documented with Swagger.

### 🚀 Key Features
- **Proximity Search:** Real-time distance calculation based on geographic coordinates.
- **Dynamic Rating:** Automated calculation of review averages from domain entities.

---

## 🧪 Key Endpoints

### 🔐 Authentication (Auth)
* `POST /api/Auth/register` - Register new users.
* `POST /api/Auth/login` - Obtain JWT token.

### ☕ Coffee Shops
* `GET /api/CoffeeShops` - Full list of venues.
* `POST /api/CoffeeShops` - Add a new coffee shop (Protected).

### ⭐ Reviews
* `POST /api/Reviews` - Submit a review (Requires Auth).
* `GET /api/Reviews/CoffeeShop/{id}` - View reviews for a venue.

---

## 🔐 Security and Authentication
The system uses a modern security workflow:
1. **Hashing:** Passwords are never stored in plain text; they are processed using **BCrypt**.
2. **JWT:** Upon login, the server generates a signed token that expires in 8 hours.
3. **Middleware:** A security guard verifies the token on every protected request.

---

## ⚙️ Installation

1. **Clone the repository:**
    ```bash
    git clone https://github.com/alvi014/CoffeeLocator.git
    cd CoffeeLocator
    ```

2. **Configure the Database:**
    Update the connection string in `appsettings.json`:
    ```json
    "DefaultConnection": "Server=YOUR_SERVER;Database=CoffeeLocatorDb;..."
    ```

3. **Run migrations:**
    ```bash
    dotnet ef database update
    ```

4. **Start the API:**
    ```bash
    dotnet run --project CoffeeLocator.Api
    ```

---

## ✨ Notable Features
* **Global Exception Handling:** Standardized JSON response for any server errors.
* **Auto-Validation:** Requests are automatically validated before reaching the controller.
* **Identity Extraction:** The system automatically recognizes the user based on the token claims.

---

## 📬 Contact
👨‍💻 **Developer:** Alvaro Victor Zamora  
📧 **Email:** alvarovictor06@gmail.com

---

Project created for educational purposes and the coffee lover community.