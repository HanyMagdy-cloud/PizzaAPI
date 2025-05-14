
# 🍕 PizzaAPI – RESTful Web API for Pizza Ordering

This is a simplified .NET 8 Web API project for managing users, dishes, categories, ingredients, and orders in a pizza restaurant system. It fulfills the **basic requirements (G-level)** of the assignment.

---

## ✅ Features

- **User Registration & Login** with JWT authentication
- Logged-in users can:
  - View and update their own profile
  - Place orders and view their own order history
  - Delete their own orders
- **Dish Management**:
  - Dishes have name, price, description, category, and ingredients
  - Users can view all dishes (GET), filter by category, or get by ID
- **Category & Ingredient Handling**:
  - Ingredients are assigned to dishes (many-to-many)
  - Each dish belongs to one category (one-to-many)

---

## 🧰 Technologies Used

- **.NET 8 Web API**
- **Entity Framework Core**
- **SQL Server / Azure SQL Database**
- **JWT Bearer Authentication**
- **Swagger (OpenAPI) for testing**
- **Azure App Service** deployment

---

## 📂 Folder Structure

```plaintext
PizzaAPI/
├── Controllers/         → API endpoints
├── DTOs/                → Request and response models
├── Entities/            → Database models
├── Interfaces/          → Repositories interfaces
├── Repos/               → Repository implementations
├── Data/                → DbContext & Migrations
├── JwtToken/            → JWT generation logic
└── appsettings.json     → Configuration (DB connection, JWT keys, etc.)
```

---

## 🔐 Authentication

- **JWT-based auth**
- Swagger UI requires clicking "Authorize" and pasting:  
  `Bearer <your_token_here>`
- Claims extracted from token to identify and protect access

---

## 📦 Sample Endpoints

| Method | Route                     | Description                     |
|--------|---------------------------|---------------------------------|
| POST   | `/api/user/register`      | Register a new user             |
| POST   | `/api/user/login`         | Log in and get a JWT            |
| GET    | `/api/user/me`            | Get your own user info          |
| PUT    | `/api/user/me`            | Update your own user info       |
| GET    | `/api/dish`               | Get all dishes                  |
| GET    | `/api/order/my`           | Get your own orders             |
| POST   | `/api/order`              | Place a new order               |
| DELETE | `/api/order/{id}`         | Delete your own order           |

---

## 🌐 Deployment

- App deployed to Azure App Service
- Connected to Azure SQL Database
- Swagger UI available at:  
  `https://<your-app-name>.azurewebsites.net/swagger`

---

## 🚀 How to Run Locally

1. Clone the repo
2. Update `appsettings.json` with your SQL connection
3. Run migrations:
   ```
   dotnet ef database update
   ```
4. Launch the app:
   ```
   dotnet run
   ```
5. Visit Swagger at `https://localhost:<port>/swagger`

---

## ✏️ Note

This project intentionally **excludes VG-level features** like:
- Role-based access (Admin/Customer)
- Admin control over all users/orders
- Advanced filtering/sorting or extra validations
