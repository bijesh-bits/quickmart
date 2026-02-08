# QuickMart - Quick Start Guide

## 🚀 Start the Application (2 Commands)

### Terminal 1 - Backend API
```powershell
cd "d:\__Projects\BITS Pilani\FSAD_Project\QuickMart\QuickMart.API"
dotnet run
```
✅ Backend will start at: **http://localhost:5095**  
✅ Swagger UI: **http://localhost:5095/swagger**

### Terminal 2 - Frontend
```powershell
cd "d:\__Projects\BITS Pilani\FSAD_Project\quickmart-frontend"
ng serve --port 4201
```
✅ Frontend will start at: **http://localhost:4201**

---

## 🧪 Test the Application

### Option 1: Use Test Account
- Navigate to http://localhost:4201
- Click **Login**
- Email: `test@quickmart.com`
- Password: `Test@123`

### Option 2: Register New Account
- Navigate to http://localhost:4201
- Click **Register**
- Fill in your details
- Create account and start shopping!

---

## 🛒 Complete User Journey

1. **Login/Register** → Access the platform
2. **Browse Products** → View 20 seeded products in 4 categories
3. **Search/Filter** → Search "rice" or filter by "Atta Rice Dal"
4. **View Details** → Click any product for full details
5. **Add to Cart** → Add items with quantity selector
6. **Shopping Cart** → View cart, update quantities, see total
7. **Checkout** → Enter shipping address and payment info
8. **Place Order** → Complete the purchase
9. **My Orders** → View order history and details

---

## 📊 What's Already Done

### Backend ✅
- 4-layer clean architecture
- 8 entities, 4 enums
- 5 RESTful API controllers
- JWT authentication
- SQLite database with seed data
- Swagger documentation

### Frontend ✅
- 36 files created
- 8 components implemented
- 5 HTTP services
- Auth guard and interceptor
- PrimeNG UI components
- Responsive design

---

## 🎯 Key Features

- ✅ User Authentication (Register, Login, JWT)
- ✅ Product Catalog (Browse, Search, Filter)
- ✅ Product Details (View full product info)
- ✅ Shopping Cart (Add, Update, Remove items)
- ✅ Checkout (Shipping address, Mock payment)
- ✅ Order Management (Order history, Details)
- ✅ Responsive UI (PrimeNG Lara Light Blue)

---

## 📁 Project Structure

```
FSAD_Project/
├── QuickMart/              # .NET Core 8 Backend
│   └── QuickMart.API/      # ← Start here (Terminal 1)
│       └── quickmart.db    # SQLite database
│
└── quickmart-frontend/     # Angular 17 Frontend
    └── src/                # ← Start here (Terminal 2)
```

---

## 🔑 Test Credentials

- **Email**: test@quickmart.com
- **Password**: Test@123

---

## 📦 Seed Data (Already Loaded)

### 4 Categories
1. Atta Rice Dal
2. Tea Coffee Beverages
3. Bakery & Dairy
4. Personal Care

### 20 Products
- Tata Gold Tea, Nescafe Classic, Fortune Sunflower Oil
- India Gate Basmati Rice, Aashirvaad Atta
- Amul Butter, Britannia Bread, Mother Dairy Milk
- Colgate Toothpaste, Lux Soap, and more!

---

## ⚡ Quick Commands Reference

### Backend
```bash
# Run API
dotnet run

# Check migrations
dotnet ef migrations list

# Access Swagger
# http://localhost:5095/swagger
```

### Frontend
```bash
# Serve app
ng serve --port 4201

# Build for production
npm run build

# Install packages (if needed)
npm install
```

---

## 🐛 Troubleshooting

**Port 4200 in use?**
```bash
ng serve --port 4201
```

**Backend not starting?**
```bash
dotnet restore
dotnet build
dotnet run
```

**Frontend build errors?**
```bash
npm install
ng serve
```

**CORS errors?**
- Backend CORS allows: http://localhost:4200 and http://localhost:4201
- Check `Program.cs` line 79-86

---

## 📸 Expected Screens

1. **Login Page** - Professional card with PrimeNG inputs
2. **Dashboard** - Product grid with search and filter
3. **Product Detail** - Full product info with "Add to Cart"
4. **Cart** - Table view with quantities and total
5. **Checkout** - Shipping form and payment options
6. **Orders** - Order history table with details

---

## 🎓 Technologies Used

- .NET Core 8.0
- Angular 17.1.0
- PrimeNG 17.18.0
- Entity Framework Core 8.0.11
- SQLite Database
- JWT Authentication
- BCrypt Password Hashing

---

## ✅ Assignment Compliance

All rubric items covered:
- ✅ Logical Architecture (4-layer)
- ✅ ER Model (8 entities)
- ✅ Backend Framework (.NET Core 8)
- ✅ Frontend Framework (Angular 17)
- ✅ Database (SQLite with EF Core)
- ✅ Search Functionality
- ✅ Features (Auth, Products, Cart, Orders)
- ✅ Documentation (PlantUML + README)
- ✅ UI/UX (PrimeNG responsive design)

---

## 📞 Need Help?

1. Check `PROJECT_SUMMARY.md` for detailed documentation
2. Review Swagger UI at http://localhost:5095/swagger
3. Check browser DevTools console for frontend errors
4. Check terminal output for backend errors

---

**Ready to Go!** 🎉

Both backend and frontend are complete. Just run the two commands above and start testing!
