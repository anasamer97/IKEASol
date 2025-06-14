# Department & Employee Management System (ASP.NET Core MVC)

A full-featured web application built with **ASP.NET Core MVC**, designed to manage departments and employees using a clean 3-tier architecture. The system supports user authentication, form validation, file uploads, and complete CRUD functionality, making it suitable for internal company management tools.

---

## 🔧 Tech Stack

- ASP.NET Core MVC
- Entity Framework Core
- C#
- SQL Server
- Bootstrap (for responsive UI)
- Repository Pattern & Unit of Work
- ASP.NET Identity (for Authentication)

---

## ✨ Features

- ✅ User Signup, Sign-in, and Logout
- ✅ Server-side validation for forms
- ✅ CRUD operations for Employees and Departments
- ✅ File upload support (e.g., employee profile pictures)
- ✅ Role-based or basic authentication
- ✅ Clean 3-tier architecture:
  - Presentation Layer (Controllers, Views)
  - Business Logic Layer (Services)
  - Data Access Layer (Repositories)
- ✅ Repository Pattern & Unit of Work for clean and testable backend
- ✅ EF Core used for efficient ORM and data management

---

## 🧱 Project Structure

```
/Controllers       => Handles HTTP requests (MVC Controllers)
/Services          => Contains business logic
/Repositories      => Data access logic (EF Core)
/Models            => Domain models and view models
/Views             => Razor Views (UI)
/wwwroot           => Static files (CSS, JS, images)
/Data              => EF DbContext and Migrations
```

---

## 🗂️ Database

- Built using **Entity Framework Core**
- Follows **Code-First** approach

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/anasamer97/IKEASol.git
cd IKEASol
```

### 2. Configure the Database

Edit your `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=YourDbName;Trusted_Connection=True;"
}
```

Or in `appsettings.Development.json` if you prefer environment-specific settings.

### 3. Apply Migrations & Run the App

```bash
dotnet ef database update
dotnet run
```

Once the app is running, navigate to:

```
https://localhost:5001
```

Or whatever URL your project is configured to launch with.

---


## 📷 Screenshots

![image](https://github.com/user-attachments/assets/4d092ede-9a0e-437c-bfa4-b60638d84420)

- Login Page  
- Dashboard / Home  
- Department CRUD  
- Employee CRUD  

---



