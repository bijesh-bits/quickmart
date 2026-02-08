# QuickMart - E-Commerce Application

## Project Overview
QuickMart is a full-stack e-commerce web application designed for grocery shopping, built as part of the Full Stack Application Development (FSAD) course assignment at BITS Pilani. The application implements a clean, layered architecture with a .NET Core 8 backend and Angular 20 frontend using Siemens Element Design System.

## Technology Stack

### Backend
- **.NET Core 8.0** - Web API framework
- **Entity Framework Core 8.0.11** - ORM for database operations
- **SQLite** - Lightweight file-based database
- **JWT Authentication** - Stateless token-based authentication
- **BCrypt.Net** - Secure password hashing
- **AutoMapper 13.0.1** - Object-to-object mapping
- **FluentValidation 12.1.1** - Input validation
- **Swagger/OpenAPI** - API documentation

### Frontend
- **Angular 20** - SPA framework
- **Siemens Element (SiMPL)** - UI component library
- **TypeScript** - Type-safe JavaScript
- **RxJS** - Reactive programming

### Architecture
- **Clean/Layered Architecture** with 4 layers:
  - **Core** - Domain entities and repository interfaces
  - **Infrastructure** - Data access, repositories, external services
  - **Application** - Business logic, DTOs, service implementations
  - **API** - RESTful endpoints, controllers

## Features

### User Management
- User registration with email validation
- Secure login with JWT token generation
- Password hashing with BCrypt
- Role-based access (Customer role)

### Product Catalog
- Browse products by category
- Search products by name
- View featured products
- Product details with images, pricing, and stock info
- 4 main categories:
  - Atta, Rice & Dal
  - Tea, Coffee & Milk Drinks
  - Bakery & Biscuits
  - Personal Care

### Shopping Cart
- Add products to cart
- Update item quantities
- Remove items from cart
- Clear entire cart
- Real-time cart total calculation

### Order Management
- Create orders from cart
- Automatic order number generation (ORD-YYYYMMDD-XXXX)
- Order calculation with tax (5%)
- Mock payment processing
- View order history
- Order details with items and payment info

### Security
- JWT Bearer token authentication
- Password hashing with BCrypt (work factor 12)
- Secure API endpoints with [Authorize] attribute
- CORS configuration for Angular frontend

## Project Structure

```
QuickMart/
├── QuickMart.Core/                 # Domain Layer
│   ├── Entities/
│   │   ├── User.cs
│   │   ├── Category.cs
│   │   ├── Product.cs
│   │   ├── Cart.cs
│   │   ├── CartItem.cs
│   │   ├── Order.cs
│   │   ├── OrderItem.cs
│   │   └── Payment.cs
│   ├── Enums/
│   │   ├── UserRole.cs
│   │   ├── OrderStatus.cs
│   │   ├── PaymentStatus.cs
│   │   └── PaymentMethod.cs
│   └── Interfaces/
│       ├── IRepository.cs
│       ├── IUserRepository.cs
│       ├── IProductRepository.cs
│       ├── ICategoryRepository.cs
│       ├── ICartRepository.cs
│       └── IOrderRepository.cs
│
├── QuickMart.Infrastructure/       # Data Access Layer
│   ├── Data/
│   │   ├── QuickMartDbContext.cs
│   │   └── DbInitializer.cs
│   ├── Repositories/
│   │   ├── Repository.cs
│   │   ├── UserRepository.cs
│   │   ├── ProductRepository.cs
│   │   ├── CategoryRepository.cs
│   │   ├── CartRepository.cs
│   │   └── OrderRepository.cs
│   ├── Services/
│   │   ├── PasswordHasher.cs
│   │   └── JwtTokenGenerator.cs
│   └── Migrations/
│
├── QuickMart.Application/          # Business Logic Layer
│   ├── DTOs/
│   │   ├── Auth/
│   │   ├── Products/
│   │   ├── Categories/
│   │   ├── Cart/
│   │   └── Orders/
│   ├── Interfaces/
│   │   ├── IAuthService.cs
│   │   ├── IProductService.cs
│   │   ├── ICategoryService.cs
│   │   ├── ICartService.cs
│   │   └── IOrderService.cs
│   └── Services/
│       ├── AuthService.cs
│       ├── ProductService.cs
│       ├── CategoryService.cs
│       ├── CartService.cs
│       └── OrderService.cs
│
├── QuickMart.API/                  # Presentation Layer
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── ProductsController.cs
│   │   ├── CategoriesController.cs
│   │   ├── CartController.cs
│   │   └── OrdersController.cs
│   ├── Program.cs
│   └── appsettings.json
│
└── Documentation/
    ├── Diagrams/
    │   ├── logical-architecture.puml
    │   ├── er-diagram.puml
    │   ├── package-diagram.puml
    │   ├── sequence-registration.puml
    │   └── sequence-place-order.puml
    └── QuickMart-Architecture-Documentation.md
```

## Database Schema

### Tables
- **Users** - User accounts with authentication details
- **Categories** - Product categories
- **Products** - Product catalog with pricing and stock
- **Carts** - User shopping carts
- **CartItems** - Items in shopping carts
- **Orders** - Order headers
- **OrderItems** - Items in orders
- **Payments** - Payment information

### Key Relationships
- User → Cart (One-to-One)
- User → Orders (One-to-Many)
- Category → Products (One-to-Many)
- Cart → CartItems (One-to-Many)
- Product → CartItems (One-to-Many)
- Order → OrderItems (One-to-Many)
- Order → Payment (One-to-One)

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - User login

### Products
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `GET /api/products/featured` - Get featured products
- `GET /api/products/search?query={query}` - Search products
- `GET /api/products?categoryId={id}` - Get products by category

### Categories
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get category by ID

### Cart (Requires Authentication)
- `GET /api/cart` - Get user's cart
- `POST /api/cart/add` - Add item to cart
- `PUT /api/cart/items/{id}` - Update cart item quantity
- `DELETE /api/cart/items/{id}` - Remove item from cart
- `DELETE /api/cart/clear` - Clear entire cart

### Orders (Requires Authentication)
- `GET /api/orders` - Get user's orders
- `GET /api/orders/{id}` - Get order details
- `POST /api/orders` - Create new order

## Setup Instructions

### Prerequisites
- .NET 8.0 SDK
- Node.js 18+ and npm
- Angular CLI 20
- Visual Studio Code or Visual Studio 2022
- SQLite browser (optional, for database inspection)

### Backend Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd QuickMart
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

4. **Run database migrations** (Optional - auto-runs on startup)
   ```bash
   cd QuickMart.API
   dotnet ef database update --project ../QuickMart.Infrastructure
   ```

5. **Run the API**
   ```bash
   cd QuickMart.API
   dotnet run
   ```

   The API will start at `http://localhost:5095`

6. **Access Swagger documentation**
   Open browser: `http://localhost:5095/swagger`

### Frontend Setup (Coming Soon)

The Angular 20 frontend with Siemens Element UI will be added in the next phase.

## Test Credentials

A test user is automatically seeded during database initialization:

- **Email**: test@quickmart.com
- **Password**: Test@123

## Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=quickmart.db"
  },
  "Jwt": {
    "Secret": "QuickMart_SuperSecretKey_ForJWT_Authentication_2024_MinimumLength32Characters",
    "Issuer": "QuickMart",
    "Audience": "QuickMart.Client",
    "ExpirationHours": 24
  }
}
```

### CORS Policy

The API is configured to allow requests from `http://localhost:4200` (Angular default port).

## Seeded Data

### Categories (4)
1. Atta, Rice & Dal
2. Tea, Coffee & Milk Drinks
3. Bakery & Biscuits
4. Personal Care

### Products (20)
- 5 products in each category
- Realistic Indian grocery items
- Stock quantities between 100-500 units
- Price range: ₹40 - ₹599

## Development Workflow

### Adding New Features
1. Create entity in Core layer
2. Add repository interface in Core
3. Implement repository in Infrastructure
4. Create DTOs in Application layer
5. Add service interface in Application
6. Implement service in Application
7. Add controller in API layer
8. Register dependencies in Program.cs

### Running Tests
```bash
dotnet test
```

### Creating Database Migrations
```bash
dotnet ef migrations add <MigrationName> --project QuickMart.Infrastructure --startup-project QuickMart.API
dotnet ef database update --project QuickMart.Infrastructure --startup-project QuickMart.API
```

## Architecture Highlights

### Dependency Injection
All repositories and services are registered in `Program.cs` with scoped lifetime.

### Repository Pattern
Generic repository provides CRUD operations. Specialized repositories add custom queries.

### DTO Pattern
All API communication uses DTOs to decouple domain entities from API contracts.

### JWT Authentication
Stateless authentication with 24-hour token expiration. Tokens include user ID, email, and role claims.

### Password Security
BCrypt hashing with work factor 12 ensures passwords are securely stored.

### Mock Payment
Payment processing is mocked with generated transaction IDs (format: TXN-YYYYMMDDHHMMSS-GUID).

## Future Enhancements
- [ ] Angular 20 frontend with Siemens Element UI
- [ ] Real payment gateway integration
- [ ] Email notifications for orders
- [ ] Product reviews and ratings
- [ ] Admin dashboard
- [ ] Order tracking
- [ ] Wishlist functionality
- [ ] Product recommendations
- [ ] Advanced search filters
- [ ] Multi-language support

## Assignment Compliance

This project fulfills all requirements of the FSAD course assignment:

✅ Full-stack implementation (Backend complete, Frontend planned)  
✅ Clean layered architecture  
✅ Entity Framework Core with Code-First approach  
✅ RESTful API design  
✅ Authentication and authorization  
✅ Comprehensive documentation  
✅ PlantUML diagrams (5 diagrams)  
✅ SQLite database  
✅ Seed data for testing  
✅ Swagger API documentation  

## Documentation

Detailed architecture documentation and PlantUML diagrams are available in the `Documentation/` folder:

- Logical Architecture Diagram
- Entity-Relationship Diagram
- Package Structure Diagram
- Sequence Diagrams (Registration, Order Placement)
- Architecture Documentation (400+ lines)

## License

This project is created for academic purposes as part of BITS Pilani FSAD course assignment.

## Author

BITS Pilani Student - FSAD Project

## Acknowledgments

- BITS Pilani for the course structure
- Siemens Element Design System
- .NET and Angular communities
