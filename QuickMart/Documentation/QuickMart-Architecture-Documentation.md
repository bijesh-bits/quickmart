# QuickMart E-Commerce Application
## Full Stack Application Development Project

**Course**: Full Stack Application Development (FSAD)  
**Institution**: BITS Pilani  
**Project Type**: Complete E-Commerce System  
**Date**: February 2026

---

## Table of Contents

1. [Executive Summary](#executive-summary)
2. [System Overview](#system-overview)
3. [Logical Architecture](#logical-architecture)
4. [Entity-Relationship (ER) Model](#entity-relationship-model)
5. [Technology Stack](#technology-stack)
6. [Component Design](#component-design)
7. [Data Requirements](#data-requirements)
8. [Security Architecture](#security-architecture)
9. [Setup and Installation](#setup-and-installation)
10. [Future Enhancements](#future-enhancements)

---

## Executive Summary

QuickMart is a modern, scalable e-commerce web application designed primarily for grocery shopping with extensibility to support multiple product categories including electronics, books, and general merchandise. The application follows a **layered architecture** approach ensuring separation of concerns, maintainability, and scalability.

### Key Features
- User Registration and Authentication
- Product Catalog with Search and Filtering
- Shopping Cart Management
- Order Processing with Payment Gateway (Mock)
- Order History and Tracking
- Category-based Product Organization

### Architecture Highlights
- **Frontend**: Angular 20 with Siemens Element (SiMPL) Design System
- **Backend**: ASP.NET Core 8 Web API
- **Database**: SQLite with Entity Framework Core
- **Architecture Pattern**: Clean Architecture with Layered Approach
- **Authentication**: JWT (JSON Web Tokens)

---

## System Overview

### Business Domain
QuickMart serves as an online grocery store similar to modern quick-commerce platforms (Blinkit, Zepto, Swiggy Instamart). The initial release focuses on grocery categories with architecture supporting future expansion.

### Primary Product Categories
1. **Atta, Rice & Dal** - Staple grains and pulses
2. **Tea, Coffee & Milk Drinks** - Beverages
3. **Bakery & Biscuits** - Baked goods and snacks
4. **Personal Care** - Hygiene and grooming products

### User Personas
- **Customer**: End users who browse, search, purchase products
- **Admin** (Future): System administrators who manage products, orders, and users

### Application Screens
1. **Registration Screen** - New user account creation
2. **Login Screen** - User authentication
3. **Dashboard (Main Menu)** - Product listing with search and filters
4. **Product Detail Page** - Detailed product information
5. **Shopping Cart** - Cart management and checkout initiation
6. **Payment Gateway** - Mock payment processing interface
7. **Orders Screen** - Order history and status tracking

---

## Logical Architecture

### Architecture Pattern: Layered Architecture (Clean Architecture)

The application follows a **4-tier layered architecture** ensuring clear separation of concerns, testability, and maintainability. Each layer has well-defined responsibilities and dependencies flow in one direction (top to bottom).

![Logical Architecture Diagram](Diagrams/logical-architecture.puml)

### Layer Description

#### 1. Presentation Layer (Frontend - Angular 20)

**Responsibilities:**
- User interface rendering using Siemens Element components
- Client-side routing and navigation
- Form validation and user input handling
- State management (cart, user session)
- HTTP API communication
- Authentication token management

**Key Components:**
- **Angular Modules**: Feature-based modules (Auth, Products, Cart, Orders, Payment)
- **Services**: HTTP client services for API communication
- **Guards**: Route protection (AuthGuard)
- **Interceptors**: JWT token injection, error handling
- **Components**: Smart and presentational components
- **Models**: TypeScript interfaces for data structures

**Technologies:**
- Angular 20 (Standalone components)
- Siemens Element (SiMPL) UI Components
- RxJS for reactive programming
- TypeScript
- SCSS for styling

---

#### 2. API Layer (ASP.NET Core 8 Web API)

**Responsibilities:**
- RESTful API endpoint exposure
- HTTP request/response handling
- JWT authentication and authorization
- Input validation and sanitization
- Exception handling and error responses
- API documentation (Swagger/OpenAPI)

**Key Components:**
- **Controllers**: AuthController, ProductsController, CartController, OrdersController, PaymentController
- **Middleware**: JwtMiddleware, ExceptionHandlerMiddleware, ValidationMiddleware
- **Filters**: AuthorizationFilter, ValidationFilter
- **DTOs**: Request/Response data transfer objects

**API Endpoints:**

| Endpoint | Method | Description | Auth Required |
|----------|--------|-------------|---------------|
| `/api/auth/register` | POST | User registration | No |
| `/api/auth/login` | POST | User login | No |
| `/api/products` | GET | Get all products | Yes |
| `/api/products/{id}` | GET | Get product by ID | Yes |
| `/api/products/search` | GET | Search products | Yes |
| `/api/categories` | GET | Get all categories | Yes |
| `/api/cart` | GET | Get user cart | Yes |
| `/api/cart/add` | POST | Add item to cart | Yes |
| `/api/cart/remove/{id}` | DELETE | Remove item from cart | Yes |
| `/api/orders` | GET | Get user orders | Yes |
| `/api/orders` | POST | Create new order | Yes |
| `/api/orders/{id}` | GET | Get order details | Yes |
| `/api/payment/process` | POST | Process payment | Yes |

---

#### 3. Application Layer (Business Logic)

**Responsibilities:**
- Business logic implementation
- Business rule validation
- DTO mapping (Entity ↔ DTO)
- Service orchestration
- Transaction coordination
- Business exception handling

**Key Components:**

**Services:**
- **IUserService / UserService**: User management, authentication, JWT generation
- **IProductService / ProductService**: Product CRUD, search, filtering
- **ICartService / CartService**: Cart operations, item management
- **IOrderService / OrderService**: Order creation, status management
- **IPaymentService / PaymentService**: Payment processing (mock)

**DTOs:**
- **Request DTOs**: LoginRequestDto, RegisterRequestDto, CreateOrderDto, PaymentRequestDto
- **Response DTOs**: AuthResponseDto, ProductResponseDto, OrderResponseDto, PaymentResponseDto

**Validators:**
- Input validation using FluentValidation
- Business rule enforcement
- Data integrity checks

**Business Rules:**
- Password must meet strength requirements (min 8 chars, uppercase, number, special char)
- Email must be unique
- Product stock must be available before adding to cart
- Order total must match cart total
- Payment amount must match order final amount

---

#### 4. Domain Layer (Core)

**Responsibilities:**
- Domain model definition
- Business entities with properties and relationships
- Repository contracts/interfaces
- Domain-driven design patterns
- Business invariants and rules
- No external dependencies (pure C# classes)

**Entities:**

1. **User**
   - Properties: UserId, Email, PasswordHash, FirstName, LastName, PhoneNumber, Role, CreatedAt, UpdatedAt, IsActive
   - Relationships: Has many Orders, Has one Cart

2. **Category**
   - Properties: CategoryId, CategoryName, Description, ImageUrl, IsActive, DisplayOrder, CreatedAt
   - Relationships: Has many Products

3. **Product**
   - Properties: ProductId, CategoryId, ProductName, Description, Price, DiscountPrice, StockQuantity, Unit, ImageUrl, Brand, IsActive, IsFeatured, CreatedAt, UpdatedAt
   - Relationships: Belongs to Category, Has many CartItems, Has many OrderItems

4. **Cart**
   - Properties: CartId, UserId, CreatedAt, UpdatedAt
   - Relationships: Belongs to User, Has many CartItems

5. **CartItem**
   - Properties: CartItemId, CartId, ProductId, Quantity, PriceAtAdd, AddedAt
   - Relationships: Belongs to Cart, References Product

6. **Order**
   - Properties: OrderId, UserId, OrderNumber, OrderDate, TotalAmount, DiscountAmount, TaxAmount, FinalAmount, Status, ShippingAddress, ShippingCity, ShippingZipCode, CreatedAt, UpdatedAt
   - Relationships: Belongs to User, Has many OrderItems, Has one Payment

7. **OrderItem**
   - Properties: OrderItemId, OrderId, ProductId, ProductName, Quantity, UnitPrice, DiscountPrice, TotalPrice
   - Relationships: Belongs to Order, References Product

8. **Payment**
   - Properties: PaymentId, OrderId, PaymentMethod, TransactionId, PaymentStatus, Amount, PaymentDate, CardLastFourDigits, CardType, CreatedAt
   - Relationships: Belongs to Order

**Repository Interfaces:**
- `IRepository<T>`: Generic repository with common CRUD operations
- `IUserRepository`: User-specific queries (GetByEmail, etc.)
- `IProductRepository`: Product-specific queries (Search, GetByCategory, etc.)
- `ICartRepository`: Cart-specific queries
- `IOrderRepository`: Order-specific queries (GetByUser, GetByOrderNumber, etc.)

---

#### 5. Infrastructure Layer (Data Access & External Services)

**Responsibilities:**
- Database access via Entity Framework Core
- Repository pattern implementation
- Database migrations and schema management
- External service integrations
- Caching mechanisms (future)
- File storage operations (future)

**Key Components:**

**Data Context:**
- **QuickMartDbContext**: EF Core DbContext with DbSets for all entities
- **Entity Configurations**: Fluent API configurations for each entity
- **Migrations**: Database schema versioning

**Repositories:**
- **GenericRepository<T>**: Base repository implementing IRepository<T>
- **UserRepository**: Implements IUserRepository
- **ProductRepository**: Implements IProductRepository
- **CartRepository**: Implements ICartRepository
- **OrderRepository**: Implements IOrderRepository

**External Services:**
- **JwtTokenGenerator**: JWT token creation and validation
- **PasswordHasher**: BCrypt password hashing and verification
- **EmailService** (Future): Email notifications

**Database:**
- **SQLite**: Lightweight, file-based database ideal for development and demo
- **Connection String**: `Data Source=quickmart.db`

---

### Package Diagram

The package diagram illustrates the structural organization of the system into cohesive packages/modules and their dependencies.

![Package Diagram](Diagrams/package-diagram.puml)

### Interaction Between Layers

**Dependency Flow**: Presentation → API → Application → Domain ← Infrastructure

- **Presentation Layer** depends on API Layer (via HTTP)
- **API Layer** depends on Application Layer (via service interfaces)
- **Application Layer** depends on Domain Layer (via entities and repository interfaces)
- **Infrastructure Layer** depends on Domain Layer (implements repository interfaces)
- **Infrastructure Layer** is injected into Application Layer (Dependency Injection)

**Principle of Separation of Concerns:**
- Each layer has a single, well-defined responsibility
- Changes in one layer have minimal impact on others
- UI changes don't affect business logic
- Business logic is independent of data access implementation
- Easy to swap implementations (e.g., SQLite → PostgreSQL)

---

## Entity-Relationship (ER) Model

The ER model captures the data requirements from the business domain, identifies constraints, and defines relationships between entities.

![ER Diagram](Diagrams/er-diagram.puml)

### Entity Descriptions

#### 1. User Entity
**Purpose**: Stores customer and admin user information

**Attributes:**
- `UserId` (PK): Unique identifier
- `Email` (UNIQUE): User's email address for login
- `PasswordHash`: Hashed password (BCrypt)
- `FirstName`, `LastName`: User's full name
- `PhoneNumber`: Contact number
- `Role`: User role (Customer/Admin)
- `CreatedAt`, `UpdatedAt`: Audit timestamps
- `IsActive`: Soft delete flag

**Constraints:**
- Email must be unique
- Password must be hashed (never store plain text)
- PhoneNumber format validation
- Role defaults to 'Customer'

---

#### 2. Category Entity
**Purpose**: Organizes products into hierarchical categories

**Attributes:**
- `CategoryId` (PK): Unique identifier
- `CategoryName` (UNIQUE): Category display name
- `Description`: Category description
- `ImageUrl`: Category banner/icon image
- `IsActive`: Visibility flag
- `DisplayOrder`: Sort order for UI
- `CreatedAt`: Creation timestamp

**Constraints:**
- CategoryName must be unique
- DisplayOrder used for custom sorting

**Initial Categories:**
1. Atta, Rice & Dal
2. Tea, Coffee & Milk Drinks
3. Bakery & Biscuits
4. Personal Care
5. Electronics (future)
6. Books (future)

---

#### 3. Product Entity
**Purpose**: Stores product information for the catalog

**Attributes:**
- `ProductId` (PK): Unique identifier
- `CategoryId` (FK): Reference to Category
- `ProductName`: Product display name
- `Description`: Product details
- `Price`: Original price
- `DiscountPrice`: Discounted price (nullable)
- `StockQuantity`: Available inventory
- `Unit`: Measurement unit (kg, ltr, pcs)
- `ImageUrl`: Product image
- `Brand`: Product brand/manufacturer
- `IsActive`: Availability flag
- `IsFeatured`: Homepage featured flag
- `CreatedAt`, `UpdatedAt`: Audit timestamps

**Constraints:**
- Price must be > 0
- DiscountPrice <= Price
- StockQuantity >= 0
- Category must exist (foreign key)
- ProductName unique per category

---

#### 4. Cart Entity
**Purpose**: Stores user's shopping cart

**Attributes:**
- `CartId` (PK): Unique identifier
- `UserId` (FK): Reference to User
- `CreatedAt`, `UpdatedAt`: Audit timestamps

**Constraints:**
- One cart per user (1:1 relationship)
- Auto-created on user registration

---

#### 5. CartItem Entity
**Purpose**: Individual items in a shopping cart

**Attributes:**
- `CartItemId` (PK): Unique identifier
- `CartId` (FK): Reference to Cart
- `ProductId` (FK): Reference to Product
- `Quantity`: Number of units
- `PriceAtAdd`: Price snapshot when added
- `AddedAt`: Timestamp

**Constraints:**
- Quantity must be > 0
- Product must be in stock
- Unique (CartId, ProductId) combination
- PriceAtAdd captures price at time of adding (handles price changes)

---

#### 6. Order Entity
**Purpose**: Customer purchase orders

**Attributes:**
- `OrderId` (PK): Unique identifier
- `UserId` (FK): Reference to User
- `OrderNumber` (UNIQUE): Human-readable order number
- `OrderDate`: Order placement timestamp
- `TotalAmount`: Sum of product prices
- `DiscountAmount`: Total discounts applied
- `TaxAmount`: Applicable taxes
- `FinalAmount`: Net payable amount
- `Status`: Order status (enum)
- `ShippingAddress`, `ShippingCity`, `ShippingZipCode`: Delivery address
- `CreatedAt`, `UpdatedAt`: Audit timestamps

**Constraints:**
- OrderNumber format: ORD-YYYYMMDD-XXXX (auto-generated)
- FinalAmount = TotalAmount - DiscountAmount + TaxAmount
- Status follows state machine: Pending → Processing → Shipped → Delivered / Cancelled

**Order Status Enum:**
- Pending: Order created, payment pending
- Processing: Payment completed, order being prepared
- Shipped: Order dispatched
- Delivered: Order completed
- Cancelled: Order cancelled

---

#### 7. OrderItem Entity
**Purpose**: Individual products in an order

**Attributes:**
- `OrderItemId` (PK): Unique identifier
- `OrderId` (FK): Reference to Order
- `ProductId` (FK): Reference to Product
- `ProductName`: Product name snapshot
- `Quantity`: Number of units ordered
- `UnitPrice`: Price per unit at order time
- `DiscountPrice`: Discounted price at order time
- `TotalPrice`: Quantity × UnitPrice

**Constraints:**
- Quantity must be > 0
- Captures product details at order time (immutable)
- Total calculation: Quantity × (DiscountPrice ?? UnitPrice)

---

#### 8. Payment Entity
**Purpose**: Payment transaction records

**Attributes:**
- `PaymentId` (PK): Unique identifier
- `OrderId` (FK): Reference to Order (1:1)
- `PaymentMethod`: Payment type (enum)
- `TransactionId` (UNIQUE): Payment gateway transaction ID
- `PaymentStatus`: Payment status (enum)
- `Amount`: Payment amount
- `PaymentDate`: Transaction timestamp
- `CardLastFourDigits`: Card number (last 4 digits)
- `CardType`: Card type (Visa/Mastercard/etc.)
- `CreatedAt`: Creation timestamp

**Constraints:**
- TransactionId must be unique (generated by mock gateway)
- Amount must match Order.FinalAmount
- One payment per order (in current version)

**Payment Method Enum:**
- CreditCard
- DebitCard
- UPI
- Cash (future)

**Payment Status Enum:**
- Pending: Payment initiated
- Completed: Payment successful
- Failed: Payment failed
- Refunded: Payment refunded

---

### Relationships

1. **User ↔ Cart**: One-to-One (User has one Cart)
2. **User ↔ Order**: One-to-Many (User has many Orders)
3. **Category ↔ Product**: One-to-Many (Category contains many Products)
4. **Cart ↔ CartItem**: One-to-Many (Cart contains many CartItems)
5. **Product ↔ CartItem**: One-to-Many (Product can be in many Carts)
6. **Order ↔ OrderItem**: One-to-Many (Order contains many OrderItems)
7. **Product ↔ OrderItem**: One-to-Many (Product can be in many Orders)
8. **Order ↔ Payment**: One-to-One (Order has one Payment)

### Data Integrity Rules

1. **Referential Integrity**: All foreign keys must reference valid records
2. **Cascading Deletes**: 
   - Deleting Cart deletes all CartItems
   - Deleting Order deletes all OrderItems and Payment
   - User deletion is soft delete (IsActive = false)
3. **Unique Constraints**: Email, OrderNumber, TransactionId must be unique
4. **Check Constraints**: Price > 0, Quantity > 0, Stock >= 0
5. **Default Values**: Role = 'Customer', IsActive = true, CreatedAt = NOW()

---

## Technology Stack

### Frontend Technologies

| Technology | Version | Purpose |
|------------|---------|---------|
| **Angular** | 20 | Frontend framework (standalone components) |
| **Siemens Element (SiMPL)** | Latest | UI component library |
| **TypeScript** | 5.x | Type-safe JavaScript |
| **RxJS** | 7.x | Reactive programming |
| **Angular Router** | 20 | Client-side routing |
| **Angular Forms** | 20 | Reactive forms |
| **SCSS** | - | Styling |
| **HttpClient** | 20 | HTTP communication |

### Backend Technologies

| Technology | Version | Purpose |
|------------|---------|---------|
| **ASP.NET Core** | 8.0 | Web API framework |
| **C#** | 12 | Programming language |
| **Entity Framework Core** | 8.0 | ORM for database access |
| **SQLite** | 3.x | Database engine |
| **JWT** | - | Authentication tokens |
| **BCrypt.Net** | - | Password hashing |
| **FluentValidation** | - | Input validation |
| **AutoMapper** | - | DTO mapping |
| **Swashbuckle (Swagger)** | - | API documentation |

### Development Tools

- **Visual Studio Code**: Primary IDE
- **Node.js & npm**: Frontend build tools
- **.NET CLI**: Backend build tools
- **Git**: Version control
- **Postman**: API testing
- **PlantUML / Draw.io**: Diagram creation

---

## Component Design

### Frontend Components

#### 1. Auth Module
**Components:**
- `LoginComponent`: User login form with email/password
- `RegisterComponent`: User registration form with validation

**Features:**
- Form validation (required fields, email format, password strength)
- Error handling and display
- Redirect to dashboard on success
- JWT token storage in localStorage

---

#### 2. Products Module
**Components:**
- `ProductListComponent`: Grid/list view of products with pagination
- `ProductDetailComponent`: Detailed product view with add-to-cart
- `ProductSearchComponent`: Search bar with filters (category, price range)
- `ProductCardComponent`: Reusable product card (presentational)

**Features:**
- Lazy loading of product images
- Category filtering
- Price sorting (low to high, high to low)
- Search by product name
- Stock availability indicator
- Featured products section

---

#### 3. Cart Module
**Components:**
- `CartComponent`: Shopping cart with item list
- `CartItemComponent`: Individual cart item with quantity controls

**Features:**
- Real-time cart total calculation
- Quantity increment/decrement
- Remove item functionality
- Empty cart state
- Proceed to checkout button

---

#### 4. Orders Module
**Components:**
- `OrdersListComponent`: Order history list
- `OrderDetailComponent`: Detailed order view with items and status

**Features:**
- Order status tracking
- Order search/filter by date, status
- Reorder functionality (future)
- Download invoice (future)

---

#### 5. Payment Module
**Components:**
- `PaymentComponent`: Payment page with shipping and payment forms
- `PaymentFormComponent`: Mock payment form (card details)

**Features:**
- Shipping address input
- Payment method selection
- Card validation (Luhn algorithm)
- Mock payment processing
- Order confirmation

---

### Backend Components

#### 1. Controllers

**AuthController**
- `POST /api/auth/register`: User registration
- `POST /api/auth/login`: User login
- Returns JWT token on success

**ProductsController**
- `GET /api/products`: Get all products with pagination
- `GET /api/products/{id}`: Get product by ID
- `GET /api/products/search?q={query}`: Search products
- `GET /api/products/category/{categoryId}`: Get products by category

**CartController**
- `GET /api/cart`: Get user's cart
- `POST /api/cart/add`: Add item to cart
- `PUT /api/cart/update/{id}`: Update item quantity
- `DELETE /api/cart/remove/{id}`: Remove item from cart
- `DELETE /api/cart/clear`: Clear entire cart

**OrdersController**
- `GET /api/orders`: Get user's orders
- `GET /api/orders/{id}`: Get order details
- `POST /api/orders`: Create new order from cart

**PaymentController**
- `POST /api/payment/process`: Process payment (mock)

---

#### 2. Services

**UserService**
- RegisterUserAsync(): Create new user account
- LoginAsync(): Authenticate user and generate JWT
- GetUserByIdAsync(): Retrieve user details
- UpdateUserAsync(): Update user profile

**ProductService**
- GetAllProductsAsync(): Retrieve all products
- GetProductByIdAsync(): Get single product
- SearchProductsAsync(): Search by name/description
- GetProductsByCategoryAsync(): Filter by category
- GetFeaturedProductsAsync(): Get featured products

**CartService**
- GetCartAsync(): Get user's cart with items
- AddToCartAsync(): Add product to cart
- UpdateCartItemAsync(): Update item quantity
- RemoveFromCartAsync(): Remove item from cart
- ClearCartAsync(): Empty cart

**OrderService**
- CreateOrderAsync(): Create order from cart items
- GetUserOrdersAsync(): Get user's order history
- GetOrderByIdAsync(): Get order details
- UpdateOrderStatusAsync(): Update order status (admin)

**PaymentService**
- ProcessPaymentAsync(): Mock payment processing
- GenerateTransactionId(): Create unique transaction ID
- ValidatePaymentAsync(): Validate payment details

---

## Data Requirements

### Database Schema (SQLite)

#### Users Table
```sql
CREATE TABLE Users (
    UserId INTEGER PRIMARY KEY AUTOINCREMENT,
    Email VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash VARCHAR(500) NOT NULL,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    PhoneNumber VARCHAR(20),
    Role VARCHAR(50) DEFAULT 'Customer',
    IsActive BOOLEAN DEFAULT 1,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_users_email ON Users(Email);
```

#### Categories Table
```sql
CREATE TABLE Categories (
    CategoryId INTEGER PRIMARY KEY AUTOINCREMENT,
    CategoryName VARCHAR(100) NOT NULL UNIQUE,
    Description TEXT,
    ImageUrl VARCHAR(500),
    IsActive BOOLEAN DEFAULT 1,
    DisplayOrder INTEGER DEFAULT 0,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_categories_name ON Categories(CategoryName);
```

#### Products Table
```sql
CREATE TABLE Products (
    ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
    CategoryId INTEGER NOT NULL,
    ProductName VARCHAR(200) NOT NULL,
    Description TEXT,
    Price DECIMAL(10,2) NOT NULL CHECK(Price > 0),
    DiscountPrice DECIMAL(10,2) CHECK(DiscountPrice <= Price),
    StockQuantity INTEGER NOT NULL DEFAULT 0 CHECK(StockQuantity >= 0),
    Unit VARCHAR(50) NOT NULL,
    ImageUrl VARCHAR(500),
    Brand VARCHAR(100),
    IsActive BOOLEAN DEFAULT 1,
    IsFeatured BOOLEAN DEFAULT 0,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);

CREATE INDEX idx_products_category ON Products(CategoryId);
CREATE INDEX idx_products_name ON Products(ProductName);
CREATE INDEX idx_products_featured ON Products(IsFeatured);
```

#### Carts Table
```sql
CREATE TABLE Carts (
    CartId INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL UNIQUE,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE INDEX idx_carts_user ON Carts(UserId);
```

#### CartItems Table
```sql
CREATE TABLE CartItems (
    CartItemId INTEGER PRIMARY KEY AUTOINCREMENT,
    CartId INTEGER NOT NULL,
    ProductId INTEGER NOT NULL,
    Quantity INTEGER NOT NULL CHECK(Quantity > 0),
    PriceAtAdd DECIMAL(10,2) NOT NULL,
    AddedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CartId) REFERENCES Carts(CartId) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    UNIQUE(CartId, ProductId)
);

CREATE INDEX idx_cartitems_cart ON CartItems(CartId);
```

#### Orders Table
```sql
CREATE TABLE Orders (
    OrderId INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    OrderNumber VARCHAR(50) NOT NULL UNIQUE,
    OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    TotalAmount DECIMAL(10,2) NOT NULL,
    DiscountAmount DECIMAL(10,2) DEFAULT 0,
    TaxAmount DECIMAL(10,2) DEFAULT 0,
    FinalAmount DECIMAL(10,2) NOT NULL,
    Status VARCHAR(50) NOT NULL DEFAULT 'Pending',
    ShippingAddress TEXT NOT NULL,
    ShippingCity VARCHAR(100) NOT NULL,
    ShippingZipCode VARCHAR(20) NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE INDEX idx_orders_user ON Orders(UserId);
CREATE INDEX idx_orders_number ON Orders(OrderNumber);
CREATE INDEX idx_orders_status ON Orders(Status);
```

#### OrderItems Table
```sql
CREATE TABLE OrderItems (
    OrderItemId INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderId INTEGER NOT NULL,
    ProductId INTEGER NOT NULL,
    ProductName VARCHAR(200) NOT NULL,
    Quantity INTEGER NOT NULL CHECK(Quantity > 0),
    UnitPrice DECIMAL(10,2) NOT NULL,
    DiscountPrice DECIMAL(10,2),
    TotalPrice DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

CREATE INDEX idx_orderitems_order ON OrderItems(OrderId);
```

#### Payments Table
```sql
CREATE TABLE Payments (
    PaymentId INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderId INTEGER NOT NULL UNIQUE,
    PaymentMethod VARCHAR(50) NOT NULL,
    TransactionId VARCHAR(100) NOT NULL UNIQUE,
    PaymentStatus VARCHAR(50) NOT NULL DEFAULT 'Pending',
    Amount DECIMAL(10,2) NOT NULL,
    PaymentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    CardLastFourDigits VARCHAR(4),
    CardType VARCHAR(50),
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) ON DELETE CASCADE
);

CREATE INDEX idx_payments_order ON Payments(OrderId);
CREATE INDEX idx_payments_transaction ON Payments(TransactionId);
```

---

### Sample Data

#### Categories
```
1. Atta, Rice & Dal
2. Tea, Coffee & Milk Drinks
3. Bakery & Biscuits
4. Personal Care
```

#### Sample Products (Atta, Rice & Dal)
```
- Aashirvaad Atta (5kg) - ₹250
- India Gate Basmati Rice (1kg) - ₹180
- Toor Dal (1kg) - ₹140
- Chana Dal (500g) - ₹70
```

---

## Security Architecture

### Authentication & Authorization

**JWT (JSON Web Tokens)**
- Token-based authentication
- Stateless authentication (no server-side sessions)
- Token contains: UserId, Email, Role, Expiry
- Token expiry: 24 hours
- Token stored in localStorage (frontend)
- Token sent in Authorization header: `Bearer <token>`

**Password Security**
- BCrypt hashing with salt (cost factor: 12)
- Password strength validation:
  - Minimum 8 characters
  - At least one uppercase letter
  - At least one lowercase letter
  - At least one number
  - At least one special character

**Authorization**
- Role-based access control (RBAC)
- Customer role: Access to shopping features
- Admin role: Access to admin panel (future)
- Route guards on frontend (AuthGuard)
- Authorize attribute on backend controllers

### API Security

**HTTPS**
- All API communication over HTTPS (production)
- SSL/TLS encryption

**CORS (Cross-Origin Resource Sharing)**
- Configured to allow frontend origin
- Credentials allowed for cookie-based auth (if used)

**Input Validation**
- Model validation using Data Annotations
- FluentValidation for complex rules
- SQL injection prevention via parameterized queries (EF Core)
- XSS prevention via output encoding

**Rate Limiting** (Future)
- Limit API requests per user/IP
- Prevent brute-force attacks

---

## Setup and Installation

### Prerequisites
- .NET 8 SDK
- Node.js (v20+) and npm
- Angular CLI (v20)
- Git
- Visual Studio Code

### Backend Setup

1. **Navigate to backend directory**
   ```bash
   cd QuickMart/Backend
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the API**
   ```bash
   dotnet run --project QuickMart.API
   ```

   API will be available at: `https://localhost:5001`

### Frontend Setup

1. **Navigate to frontend directory**
   ```bash
   cd QuickMart/Frontend
   ```

2. **Install npm packages**
   ```bash
   npm install
   ```

3. **Run the Angular application**
   ```bash
   ng serve
   ```

   Application will be available at: `http://localhost:4200`

### Database Seeding

Database will be automatically seeded with:
- 4 product categories
- 20+ sample products
- 1 test user (email: test@quickmart.com, password: Test@123)

---

## Future Enhancements

### Phase 2 Features
1. **Admin Panel**
   - Product management (CRUD)
   - Order management
   - User management
   - Analytics dashboard

2. **Advanced Search**
   - Full-text search
   - Filters (brand, price range, rating)
   - Sort options

3. **User Features**
   - Wishlist
   - Product reviews and ratings
   - Order tracking with real-time status
   - Multiple delivery addresses

4. **Payment Integration**
   - Real payment gateway (Razorpay, Stripe)
   - Multiple payment methods
   - Wallet integration

5. **Notifications**
   - Email notifications (order confirmation, shipping updates)
   - Push notifications
   - SMS alerts

6. **Performance Optimization**
   - Redis caching
   - CDN for images
   - Lazy loading and code splitting

7. **Mobile Application**
   - React Native / Flutter mobile app
   - Push notifications

---

## Conclusion

QuickMart is designed with a **clean, layered architecture** ensuring:
- **Separation of Concerns**: Each layer has distinct responsibilities
- **Scalability**: Easy to scale horizontally (add more servers)
- **Maintainability**: Easy to modify and extend
- **Testability**: Each layer can be tested independently
- **Flexibility**: Easy to swap implementations (e.g., database, UI framework)

The architecture supports future growth from a grocery-only platform to a multi-category e-commerce marketplace with admin capabilities, advanced features, and real payment integration.

---

**Document Version**: 1.0  
**Last Updated**: February 7, 2026  
**Author**: BITS Pilani FSAD Student  
**Project**: QuickMart E-Commerce Application
