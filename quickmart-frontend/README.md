# QuickMart Frontend

Angular 17 frontend for QuickMart - Online Grocery Shopping Platform

## Features

- 🛒 Product catalog with search and filtering
- 🛍️ Shopping cart management
- 💳 Mock payment gateway integration
- 📦 Order history and tracking
- 🔐 JWT-based authentication
- 📱 Responsive PrimeNG UI components

## Prerequisites

- Node.js 18+ 
- npm 9+
- Angular CLI 17

## Installation

```bash
npm install
```

## Configuration

Update API URL in `src/environments/environment.ts`:

```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5095/api'
};
```

## Development Server

```bash
ng serve
```

Navigate to `http://localhost:4200`

## Build

```bash
ng build
```

Build artifacts will be in the `dist/` directory.

## Project Structure

```
src/
├── app/
│   ├── core/               # Core services, models, guards
│   │   ├── guards/        # Auth guard
│   │   ├── interceptors/  # HTTP interceptors
│   │   ├── models/        # TypeScript interfaces
│   │   └── services/      # HTTP services
│   ├── features/          # Feature modules
│   │   ├── auth/         # Login, Register
│   │   ├── products/     # Dashboard, Product Detail
│   │   ├── cart/         # Shopping Cart
│   │   └── orders/       # Checkout, Orders
│   └── shared/           # Shared components
│       └── components/   # Navbar
├── assets/               # Static files
├── environments/         # Environment configs
└── styles.scss          # Global styles
```

## Test Credentials

- **Email**: test@quickmart.com
- **Password**: Test@123

## Technologies

- Angular 17
- PrimeNG 17.18.0
- RxJS 7
- TypeScript 5

## API Integration

Backend API endpoints:
- `/auth/register` - User registration
- `/auth/login` - User login
- `/products` - Product catalog
- `/cart` - Cart management
- `/orders` - Order management

## License

MIT
