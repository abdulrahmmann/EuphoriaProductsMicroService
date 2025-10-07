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

