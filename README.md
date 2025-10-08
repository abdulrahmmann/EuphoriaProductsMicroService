# 🛍️ Euphoria Products Microservice

[![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet?logo=dotnet)](https://dotnet.microsoft.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20%26%20DDD-blue)](#)
[![Pattern](https://img.shields.io/badge/Pattern-CQRS%20%26%20MediatR-brightgreen)](#)
[![Database](https://img.shields.io/badge/Database-EF%20Core%20%7C%20SQL%20Server-lightgrey)](#)
[![Docker](https://img.shields.io/badge/Docker-ready-blue?logo=docker)](https://www.docker.com/)

---

> **EuphoriaProductsMicroService** — the core catalog and product management microservice for the **Euphoria eCommerce Platform**.  
>  
> It manages all product-related data including brands, categories, subcategories, colors, sizes, variants, wishlists, and feedback.  
>  
> Built using **.NET 9**, ** Minimal APIs **, **Entity Framework Core**, **CQRS**, and **MediatR**, with **Domain-Driven Design (DDD)** and **Clean Architecture** for maximum scalability and maintainability.
> The database Used is MySql.
---

## ⚙️ Key Features

- 🧱 **Product Management:** CRUD operations for all product data (name, price, description, images, brand, etc.)
- 🎨 **Product Variants:** Supports multiple combinations of colors, sizes, and price overrides.
- 🧩 **Category Hierarchy:**  
  - `MainCategory` → Men, Women, Kids  
  - `Category` → Clothing, Shoes, Accessories  
  - `SubCategory` → T-Shirts, Sneakers, Jackets
- 💬 **Feedback & Wishlist:** Manage product reviews and user wishlists.
- 🧠 **CQRS with MediatR:** Separate read/write flows for performance and clean code.
- 🧱 **Clean 4-Layer Architecture:**  
  - **API Layer** — Controllers and endpoints  
  - **Application Layer** — Commands, Queries, DTOs  
  - **Domain Layer** — Core entities and aggregates  
  - **Infrastructure Layer** — EF Core persistence and repositories
- 🧰 **DDD Principles:** Clear aggregate roots (`Product`, `ProductVariant`) and rich domain models.
- 🐳 **Dockerized:** Includes `Dockerfile` and `docker-compose.yml` for isolated microservice deployment.

---

## 🧠 Tech Stack

| Layer | Technologies |
|-------|---------------|
| **Backend** | .NET 9, ASP.NET Core Web API |
| **Architecture** | Clean Architecture + DDD + CQRS |
| **Mediator** | MediatR |
| **ORM** | Entity Framework Core 9 |
| **Validation** | FluentValidation |
| **Mapping** | Mapster |
| **Database** | SQL Server |
| **Deployment** | Docker & Docker Compose |

---

## 🧩 Core Domain Entities

| Entity | Description |
|---------|-------------|
| **Product** | Main entity holding product info, price, images, and category links. |
| **ProductVariant** | Specific product variant (color + size + stock + optional price override). |
| **Brand** | Product manufacturer or brand. |
| **MainCategory** | Top-level classification (Men / Women / Kids). |
| **Category** | Subsection of main category (Clothing, Shoes, etc.). |
| **SubCategory** | Detailed section (e.g., T-Shirts, Sneakers). |
| **Color** | Defines color with name and hex value. |
| **Size** | Defines available sizes (S, M, L, XL). |
| **Feedback** | Product review and rating. |
| **Wishlist** | User’s saved products. |
