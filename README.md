# CoffeeLocator ☕

**CoffeeLocator** es una API RESTful robusta diseñada para que los amantes del café puedan descubrir cafeterías, gestionar sus favoritas y dejar reseñas auténticas. Construida con una arquitectura profesional en .NET, garantiza seguridad, escalabilidad y un manejo de datos eficiente.

🚀 **Estado del Proyecto:** Backend Base Completado (Seguridad + CRUD)

---

## 📋 Tabla de Contenidos
* [📦 Descripción General](#-descripción-general)
* [🛠 Tecnologías y Herramientas](#-tecnologías-y-herramientas)
* [🧭 Arquitectura del Sistema](#-arquitectura-del-sistema)
* [🧪 Endpoints Principales](#-endpoints-principales)
* [🔐 Seguridad y Autenticación](#-seguridad-y-autenticación)
* [⚙️ Instalación](#-instalación)
* [📬 Contacto](#-contacto)

---

## 📦 Descripción General
Este sistema permite centralizar la información de cafeterías locales. Los usuarios pueden registrarse para calificar sus experiencias, mientras que el sistema asegura la integridad de los datos mediante validaciones estrictas y un manejo global de excepciones.

**Componentes clave:**
* **Autenticación:** Sistema de Login/Registro basado en JWT.
* **Reseñas:** Gestión de calificaciones con protección de identidad.
* **Validación:** Uso de FluentValidation para asegurar datos de entrada correctos.

---

## 🛠 Tecnologías y Herramientas
* **Lenguaje:** C#
* **Framework:** .NET 8.0
* **Base de Datos:** SQL Server / Entity Framework Core (Code First)
* **Seguridad:** JWT (JSON Web Tokens) & BCrypt.Net para hashing de contraseñas.
* **Documentación:** Swagger / OpenAPI
* **Validación:** FluentValidation

---

## 🧭 Arquitectura del Sistema
El proyecto sigue una estructura de **Arquitectura en Capas** para separar responsabilidades:

* **API:** Controladores, Middlewares (Global Exception Handler) y Configuración.
* **Application:** DTOs, Validadores y Lógica de negocio.
* **Domain:** Entidades principales y Enums.
* **Infrastructure:** Persistencia de datos (DbContext), Migraciones y Repositorios.



---

## 🧪 Endpoints Principales

### 🔐 Autenticación (Auth)
* `POST /api/Auth/register` - Registro de nuevos usuarios.
* `POST /api/Auth/login` - Obtención de Token JWT.

### ☕ Cafeterías (CoffeeShops)
* `GET /api/CoffeeShops` - Listado completo de locales.
* `POST /api/CoffeeShops` - Agregar nueva cafetería (Protegido).

### ⭐ Reseñas (Reviews)
* `POST /api/Reviews` - Publicar una calificación (Requiere Auth).
* `GET /api/Reviews/CoffeeShop/{id}` - Ver opiniones de un local.

---

## 🔐 Seguridad y Autenticación
El sistema utiliza un flujo de seguridad moderno:
1.  **Hashing:** Las contraseñas nunca se guardan en texto plano, se procesan con **BCrypt**.
2.  **JWT:** Al iniciar sesión, el servidor genera un token firmado que expira en 8 horas.
3.  **Middleware:** Un guardia de seguridad verifica el token en cada petición protegida.

---

## ⚙️ Instalación

1.  **Clona el repositorio:**
    ```bash
    git clone [https://github.com/alvi014/CoffeeLocator.git](https://github.com/alvi014/CoffeeLocator.git)
    cd CoffeeLocator
    ```

2.  **Configura la Base de Datos:**
    Actualiza la cadena de conexión en `appsettings.json`:
    ```json
    "DefaultConnection": "Server=TU_SERVIDOR;Database=CoffeeLocatorDb;..."
    ```

3.  **Ejecuta las migraciones:**
    ```bash
    dotnet ef database update
    ```

4.  **Inicia la API:**
    ```bash
    dotnet run --project CoffeeLocator.Api
    ```

---

## ✨ Características Destacadas
* **Global Exception Handling:** Respuesta JSON estandarizada para cualquier error del servidor.
* **Auto-Validation:** Las peticiones se validan automáticamente antes de llegar al controlador.
* **Identity Extraction:** El sistema reconoce automáticamente al usuario mediante los *Claims* del token.

---

## 📬 Contacto
👨‍💻 **Desarrollador:** Alvaro Victor Zamora
📧 **Correo:** alvarovictor06@gmail.com

---
Proyecto creado con fines educativos y para la comunidad cafetalera.
