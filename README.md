# CoffeeLocator ☕ 🇨🇷 *(Fullstack Development in Progress)*

**CoffeeLocator** is a robust Fullstack application designed for coffee lovers to discover local spots, manage favorites, and leave genuine reviews. Built with a **Clean Architecture** backend and a **Modern Standalone Angular** frontend.

🚀 **Project Status:** - **Backend:** 100% Completed (Security + CRUD + Geolocation).
- **Frontend:** 40% Completed (Map Integration & API Consumption).

---

## 📦 Overview
This system centralizes local coffee shop information. It allows users to discover nearby venues, rate their experiences, and contribute to the community, ensuring data integrity via a structured and secure approach.

---

## 🏗️ System Architecture

The project follows a decoupled architecture to ensure scalability and professional standards:

* **Backend (.NET 8):** Clean Architecture pattern (Domain, Application, Infrastructure, API).
* **Frontend (Angular 18):** Modern Standalone Component architecture, organized by features.



---

## 🛠 Technologies and Tools

### **Backend (The Engine)**
* **Framework:** .NET 8.0
* **Database:** SQL Server / EF Core (Code First)
* **Security:** JWT (JSON Web Tokens) & BCrypt hashing.
* **Validation:** FluentValidation.

### **Frontend (The Interface)**
* **Framework:** Angular 18 (Standalone Components).
* **Mapping:** Leaflet.js (OpenStreetMap integration).
* **Styling:** SCSS & Responsive Design.
* **Communication:** HttpClient with pre-configured CORS policies.

---

## ✨ Notable Features
* **Interactive Mapping:** Real-time visualization of coffee shops using Leaflet.
* **Dynamic Markers:** Markers display shop details (Name, Address, Photo) directly on the map.
* **Proximity Search:** Backend calculates distances using the Haversine formula, providing accurate "nearby" results.
* **Automated Migrations:** Database schema updates automatically on startup.

---

## 🧪 Key Endpoints & UI Views

### **API Endpoints**
* `POST /api/Auth/login` - Obtain JWT token.
* `GET /api/CoffeeShops/nearby` - Fetch shops based on coordinates and radius.
* `POST /api/CoffeeShops` - Protected endpoint to register new venues.

### **Frontend Components**
* **Map View:** Interactive map currently being tested with **Aguas Zarcas, San Carlos** coordinates to validate marker precision.
* **Coffee Cards:** (In progress) List view of nearby shops with ratings.

---

## ⚙️ Installation & Running

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/alvi014/CoffeeLocator.git](https://github.com/alvi014/CoffeeLocator.git)
    ```

2.  **Run the Backend:**
    ```bash
    cd CoffeeLocator.Api
    dotnet watch run
    ```

3.  **Run the Frontend:**
    ```bash
    cd CoffeeLocator-UI
    npm install
    npm start
    ```

---

## 📬 Contact
👨‍💻 **Developer:** Alvaro Victor Zamora 🇨🇷  
📧 **Email:** [alvarovictor06@gmail.com](mailto:alvarovictor06@gmail.com)  
📱 **WhatsApp:** [+506 8722-1109](https://wa.me/50687221109)

---
*Project created for educational purposes and the coffee lover community.*
