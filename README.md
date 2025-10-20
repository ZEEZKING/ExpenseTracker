# 💰 Expense Tracker API

A **Personal Expense Tracker Backend** built with **ASP.NET Core 8**, featuring secure JWT authentication, clean architecture, and modular service design.  
Developed as part of a technical assessment for **CrediSure Financial Technologies Ltd**, the API enables users to register, log in, and manage their daily expenses efficiently — with filtering, categorization, and analytics support.

---

## 🚀 Live API & Documentation

- **Base URL:** [https://expensetrackerapi-bdbkged4hzgvbqgk.westeurope-01.azurewebsites.net](https://expensetrackerapi-bdbkged4hzgvbqgk.westeurope-01.azurewebsites.net)  
- **Swagger UI:** [API Documentation](https://expensetrackerapi-bdbkged4hzgvbqgk.westeurope-01.azurewebsites.net/swagger/index.html)

---

## 🧩 Features

✅ User Registration & Login (JWT Authentication)  
✅ Add, Edit, Delete, and View Expenses  
✅ Filter Expenses by **Date** or **Category**  
✅ View Spending Summary by Category  
✅ Data prepared for chart visualizations  
✅ Clean modular architecture — Controllers, Services, Repositories  
✅ Password hashing (BCrypt)  
✅ Global error handling and validation middleware  
✅ Entity Framework Core with PostgreSQL  

---

## 🏗️ Project Architecture

All source code is inside the `src/` folder.  
The structure follows **Clean Architecture** principles — similar to **NestJS module-based design**, allowing easy scalability or migration to Node-based frameworks in the future.




**Layer responsibilities:**

| Layer | Purpose |
|--------|----------|
| **Domain** | Core entities and domain models |
| **Application** | DTOs, business logic interfaces, and services |
| **Infrastructure** | Database context, repositories, and migrations |
| **WebAPI** | Controllers, routes, and middlewares |

---

## ⚙️ Tech Stack

| Category | Technology |
|-----------|-------------|
| Framework | ASP.NET Core 8 Web API |
| Language | C# |
| Database | PostgreSQL (via EF Core) |
| Authentication | JWT Bearer Tokens |
| ORM | Entity Framework Core |
| Deployment | Azure App Service |
| Logging | Console, Middleware Logging |
| Pattern | Clean Architecture, Repository Pattern |

---

## 🧠 API Endpoints Overview

### 🔐 **Authentication**
| Method | Endpoint | Description |
|--------|-----------|-------------|
| **POST** | `/api/Auth/register` | Register a new user |
| **POST** | `/api/Auth/login` | Login and receive JWT token |

### 💸 **Expense Management**
| Method | Endpoint | Description |
|--------|-----------|-------------|
| **GET** | `/api/Expense/all` | Retrieve all expenses |
| **POST** | `/api/Expense` | Add a new expense |
| **PUT** | `/api/Expense/{id}` | Update an existing expense |
| **DELETE** | `/api/Expense/{id}` | Delete an expense |
| **GET** | `/api/Expense/summary/category` | Get total expenses grouped by category |
| **POST** | `/api/Expense/filter/` | Filter expenses by date or category |
| **GET** | `/api/Expense/trend` | Get expense trend data for charts |

---

## 🧰 Setup Instructions

### 1️⃣ Clone the Repository
```bash
git clone https://github.com/ZEEZKING/ExpenseTracker.git
cd ExpenseTracker/src


