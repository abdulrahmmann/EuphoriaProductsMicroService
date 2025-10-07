# EuphoriaProductsMicroService
EuphoriaProductsMicroService : the core catalog and product management microservice for the Euphoria eCommerce Platform.

It handles products, brands, categories, colors, sizes, variants, feedback, and wishlists — following Domain-Driven Design (DDD) and Clean Architecture principles with CQRS and MediatR 
for clear separation of commands and queries.

The Database Used in ProductsService is MySql. 

Key Features:
Products Management: Create, update, delete, and query products with full details (images, category, subcategory, brand, ... etc).
Product Variants: Handle combinations of colors and sizes with stock and optional price overrides.
Category Hierarchy: Organize products under MainCategory (Men/Women/Kids) → Category → SubCategory.
Feedback & Wishlist: Manage product feedback and user wishlists.

CQRS with MediatR: Split commands (write operations) and queries (read operations) for maintainability and scalability.

Based Structure:

API → Controllers & Endpoints.

Application → Commands, Queries, DTOs, and Handlers.

Domain → Entities, Value Objects, and Aggregates.

Infrastructure → EF Core repositories, database context, and configurations.

Dockerized: Includes Dockerfile and Compose setup for isolated container deployment.

Tech Stack:
Backend:	.NET 9, ASP.NET Core Web API
Architecture:	Clean Architecture + CQRS + DDD
Mediator:	MediatR
Persistence:	Entity Framework Core 9 + SQL Server
Validation:	FluentValidation
Mapping:	Mapster
Containerization:	Docker & Docker Compose

Core Domain Entities:
Product: Represents the main product with base info, images, brand, category, and price.
ProductVariant:	Specific variant of a product (Color + Size + Stock + PriceOverride).
Brand:	Product manufacturer or brand.
MainCategory:	Top-level category (Men / Women / Kids).
Category:	Middle-level classification (Clothing / Shoes / Accessories).
SubCategory: Detailed classification (T-Shirts / Jackets / Sneakers).
Color: Represents product color (Name, HexCode).
Size:	Represents product size (S, M, L, XL...).
Feedback:	Product rating and review by user.
Wishlist:	User’s saved products.
