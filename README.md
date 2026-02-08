# QuickMart - Full Stack E-Commerce Application

## 🚀 Quick Start

### Using VS Code Launch Configuration (Recommended)

1. **Press F5** or open **Run and Debug** panel (Ctrl+Shift+D)
2. Select **"🎯 QuickMart Full Stack (Backend + Frontend)"**
3. Click the green play button

This will automatically:
- ✅ Start .NET Backend API on http://localhost:5095
- ✅ Open Swagger UI at http://localhost:5095/swagger
- ✅ Start Angular Frontend on http://localhost:4200
- ✅ Launch the app in Chrome with debugging enabled

### Manual Start

#### Backend (.NET Core 8)
```bash
cd QuickMart/QuickMart.API
dotnet run --launch-profile http
```

#### Frontend (Angular 17)
```bash
cd quickmart-frontend
npm start
```

## 📱 Application URLs

- **Frontend:** http://localhost:4200
- **Backend API:** http://localhost:5095
- **Swagger UI:** http://localhost:5095/swagger

## 🔐 Test Credentials

- **Email:** test@quickmart.com
- **Password:** Test@123

## 🛠️ Technology Stack

### Backend
- .NET Core 8.0
- Entity Framework Core 8
- SQLite Database
- JWT Authentication
- Swagger/OpenAPI

### Frontend
- Angular 17
- PrimeNG 17 (UI Components)
- TypeScript
- SCSS
- RxJS

## 📦 Features

- ✅ User Authentication (Register/Login)
- ✅ Product Catalog with Categories
- ✅ Search & Filter Products
- ✅ Shopping Cart (works without login)
- ✅ Guest Cart (localStorage)
- ✅ Checkout (requires login)
- ✅ Order Management
- ✅ Responsive Design
- ✅ Modern UI with PrimeNG

## 🗂️ Project Structure

```
FSAD_Project/
├── QuickMart/                    # Backend Solution
│   ├── QuickMart.API/            # Web API Layer
│   ├── QuickMart.Application/    # Business Logic
│   ├── QuickMart.Core/           # Domain Models
│   └── QuickMart.Infrastructure/ # Data Access
├── quickmart-frontend/           # Angular Frontend
│   ├── src/
│   │   ├── app/
│   │   │   ├── core/            # Services, Guards, Interceptors
│   │   │   ├── features/        # Feature Modules
│   │   │   └── shared/          # Shared Components
│   │   └── assets/              # Static Assets
└── .vscode/                      # VS Code Configuration
    ├── launch.json              # Debug Configurations
    └── tasks.json               # Build Tasks
```

## 🐛 Debugging

### Backend Debugging
- Set breakpoints in C# files
- Use "Launch .NET Backend" configuration
- Debug in VS Code

### Frontend Debugging
- Set breakpoints in TypeScript files
- Use "Launch Angular Frontend" configuration
- Debug in Chrome DevTools or VS Code

### Full Stack Debugging
- Use compound configuration to debug both simultaneously
- Breakpoints work in both backend and frontend

## 📝 API Documentation

Visit http://localhost:5095/swagger when the backend is running to explore all available API endpoints.

## 🔄 Development Workflow

1. Make changes to backend/frontend code
2. Hot reload will automatically update the application
3. Backend: Changes require rebuild (Ctrl+C and restart)
4. Frontend: Changes are automatically reflected

## ⚠️ Troubleshooting

### Port Already in Use
- Backend: Stop any process using port 5095
- Frontend: Stop any process using port 4200

### Build Errors
- Backend: Run `dotnet clean` then `dotnet build`
- Frontend: Delete `node_modules` and run `npm install`

### Database Issues
- Delete `quickmart.db` file in QuickMart.API folder
- Restart the backend (database will be recreated with seed data)

## 📚 Additional Commands

```bash
# Backend
dotnet build          # Build the solution
dotnet test           # Run tests
dotnet ef migrations add <name>  # Add migration

# Frontend
ng serve              # Start dev server
ng build              # Build for production
ng test               # Run unit tests
ng lint               # Lint code
```
