using QuickMart.Core.Entities;
using QuickMart.Core.Enums;
using QuickMart.Infrastructure.Services;

namespace QuickMart.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(QuickMartDbContext context)
    {
        // Check if data already exists
        if (context.Categories.Any())
        {
            return; // Database has been seeded
        }

        // Seed Categories
        var categories = new List<Category>
        {
            new Category
            {
                CategoryName = "Atta, Rice & Dal",
                Description = "Essential grains, pulses, and flour for your daily cooking needs",
                ImageUrl = "/images/categories/atta-rice-dal.jpg",
                IsActive = true
            },
            new Category
            {
                CategoryName = "Tea, Coffee & Milk Drinks",
                Description = "Refreshing beverages for every moment of your day",
                ImageUrl = "/images/categories/beverages.jpg",
                IsActive = true
            },
            new Category
            {
                CategoryName = "Bakery & Biscuits",
                Description = "Fresh baked goods, cookies, and snacks for teatime",
                ImageUrl = "/images/categories/bakery.jpg",
                IsActive = true
            },
            new Category
            {
                CategoryName = "Personal Care",
                Description = "Daily essentials for personal hygiene and grooming",
                ImageUrl = "/images/categories/personal-care.jpg",
                IsActive = true
            }
        };

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        // Seed Products
        var products = new List<Product>
        {
            // Atta, Rice & Dal Category
            new Product
            {
                ProductName = "Aashirvaad Whole Wheat Atta",
                Description = "100% MP Chakki Atta with natural fiber and nutrients for soft rotis",
                Price = 299,
                DiscountPrice = 279,
                CategoryId = categories[0].CategoryId,
                ImageUrl = "/images/products/aashirvaad-atta.jpg",
                StockQuantity = 150,
                IsFeatured = true,
                IsActive = true,
                Unit = "5 kg"
            },
            new Product
            {
                ProductName = "India Gate Basmati Rice",
                Description = "Premium aged basmati rice with extra long grains and exquisite aroma",
                Price = 599,
                DiscountPrice = 549,
                CategoryId = categories[0].CategoryId,
                ImageUrl = "/images/products/basmati-rice.jpg",
                StockQuantity = 120,
                IsFeatured = true,
                IsActive = true,
                Unit = "5 kg"
            },
            new Product
            {
                ProductName = "Toor Dal (Arhar)",
                Description = "Premium quality split pigeon peas, rich in protein",
                Price = 189,
                DiscountPrice = null,
                CategoryId = categories[0].CategoryId,
                ImageUrl = "/images/products/toor-dal.jpg",
                StockQuantity = 200,
                IsFeatured = false,
                IsActive = true,
                Unit = "1 kg"
            },
            new Product
            {
                ProductName = "Moong Dal",
                Description = "Split green gram, excellent source of protein and fiber",
                Price = 159,
                DiscountPrice = 149,
                CategoryId = categories[0].CategoryId,
                ImageUrl = "/images/products/moong-dal.jpg",
                StockQuantity = 180,
                IsFeatured = false,
                IsActive = true,
                Unit = "1 kg"
            },
            new Product
            {
                ProductName = "Chana Dal",
                Description = "Split Bengal gram, perfect for traditional Indian recipes",
                Price = 129,
                DiscountPrice = null,
                CategoryId = categories[0].CategoryId,
                ImageUrl = "/images/products/chana-dal.jpg",
                StockQuantity = 220,
                IsFeatured = false,
                IsActive = true,
                Unit = "1 kg"
            },

            // Tea, Coffee & Milk Drinks Category
            new Product
            {
                ProductName = "Tata Tea Premium",
                Description = "Rich and refreshing blend of fine tea leaves from the best gardens",
                Price = 249,
                DiscountPrice = 229,
                CategoryId = categories[1].CategoryId,
                ImageUrl = "/images/products/tata-tea.jpg",
                StockQuantity = 300,
                IsFeatured = true,
                IsActive = true,
                Unit = "1 kg"
            },
            new Product
            {
                ProductName = "Nescafe Classic Coffee",
                Description = "Rich and aromatic instant coffee for a perfect start to your day",
                Price = 399,
                DiscountPrice = 359,
                CategoryId = categories[1].CategoryId,
                ImageUrl = "/images/products/nescafe.jpg",
                StockQuantity = 250,
                IsFeatured = true,
                IsActive = true,
                Unit = "200 g"
            },
            new Product
            {
                ProductName = "Bru Instant Coffee",
                Description = "Premium blend of 70% coffee and 30% chicory for authentic South Indian taste",
                Price = 329,
                DiscountPrice = null,
                CategoryId = categories[1].CategoryId,
                ImageUrl = "/images/products/bru-coffee.jpg",
                StockQuantity = 200,
                IsFeatured = false,
                IsActive = true,
                Unit = "200 g"
            },
            new Product
            {
                ProductName = "Amul Milk Powder",
                Description = "Full cream milk powder for tea, coffee, and cooking",
                Price = 459,
                DiscountPrice = 439,
                CategoryId = categories[1].CategoryId,
                ImageUrl = "/images/products/amul-milk-powder.jpg",
                StockQuantity = 150,
                IsFeatured = false,
                IsActive = true,
                Unit = "500 g"
            },
            new Product
            {
                ProductName = "Horlicks Health Drink",
                Description = "Nutritional drink with 23 vital nutrients for growing kids",
                Price = 499,
                DiscountPrice = 449,
                CategoryId = categories[1].CategoryId,
                ImageUrl = "/images/products/horlicks.jpg",
                StockQuantity = 180,
                IsFeatured = false,
                IsActive = true,
                Unit = "1 kg"
            },

            // Bakery & Biscuits Category
            new Product
            {
                ProductName = "Britannia Good Day Cookies",
                Description = "Delicious butter cookies with cashew and almonds",
                Price = 80,
                DiscountPrice = 75,
                CategoryId = categories[2].CategoryId,
                ImageUrl = "/images/products/goodday-cookies.jpg",
                StockQuantity = 400,
                IsFeatured = true,
                IsActive = true,
                Unit = "200 g"
            },
            new Product
            {
                ProductName = "Parle-G Gold Biscuits",
                Description = "India's favorite glucose biscuits, perfect with tea",
                Price = 50,
                DiscountPrice = null,
                CategoryId = categories[2].CategoryId,
                ImageUrl = "/images/products/parle-g.jpg",
                StockQuantity = 500,
                IsFeatured = true,
                IsActive = true,
                Unit = "250 g"
            },
            new Product
            {
                ProductName = "Sunfeast Dark Fantasy Choco Fills",
                Description = "Premium chocolate filled cookies with smooth chocolate cream",
                Price = 90,
                DiscountPrice = 85,
                CategoryId = categories[2].CategoryId,
                ImageUrl = "/images/products/dark-fantasy.jpg",
                StockQuantity = 350,
                IsFeatured = false,
                IsActive = true,
                Unit = "150 g"
            },
            new Product
            {
                ProductName = "Britannia Marie Gold Biscuits",
                Description = "Light and crispy marie biscuits, rich in wheat flavor",
                Price = 60,
                DiscountPrice = null,
                CategoryId = categories[2].CategoryId,
                ImageUrl = "/images/products/marie-gold.jpg",
                StockQuantity = 450,
                IsFeatured = false,
                IsActive = true,
                Unit = "250 g"
            },
            new Product
            {
                ProductName = "Modern White Bread",
                Description = "Soft and fresh white bread, perfect for sandwiches",
                Price = 45,
                DiscountPrice = 40,
                CategoryId = categories[2].CategoryId,
                ImageUrl = "/images/products/white-bread.jpg",
                StockQuantity = 100,
                IsFeatured = false,
                IsActive = true,
                Unit = "400 g"
            },

            // Personal Care Category
            new Product
            {
                ProductName = "Colgate Total Advanced Health",
                Description = "Protects against germs for 12 hours with advanced protection",
                Price = 149,
                DiscountPrice = 139,
                CategoryId = categories[3].CategoryId,
                ImageUrl = "/images/products/colgate.jpg",
                StockQuantity = 300,
                IsFeatured = true,
                IsActive = true,
                Unit = "200 g"
            },
            new Product
            {
                ProductName = "Dettol Original Soap",
                Description = "Trusted germ protection with classic Dettol fragrance",
                Price = 95,
                DiscountPrice = 89,
                CategoryId = categories[3].CategoryId,
                ImageUrl = "/images/products/dettol-soap.jpg",
                StockQuantity = 400,
                IsFeatured = true,
                IsActive = true,
                Unit = "125 g × 3"
            },
            new Product
            {
                ProductName = "Pantene Hair Fall Control Shampoo",
                Description = "Reduces hair fall and makes hair stronger from root to tip",
                Price = 349,
                DiscountPrice = 319,
                CategoryId = categories[3].CategoryId,
                ImageUrl = "/images/products/pantene-shampoo.jpg",
                StockQuantity = 250,
                IsFeatured = false,
                IsActive = true,
                Unit = "650 ml"
            },
            new Product
            {
                ProductName = "Dove Intense Repair Conditioner",
                Description = "Deep conditioning for visibly healthier and stronger hair",
                Price = 399,
                DiscountPrice = null,
                CategoryId = categories[3].CategoryId,
                ImageUrl = "/images/products/dove-conditioner.jpg",
                StockQuantity = 200,
                IsFeatured = false,
                IsActive = true,
                Unit = "650 ml"
            },
            new Product
            {
                ProductName = "Lifebuoy Total 10 Hand Wash",
                Description = "Germ protection hand wash with activ silver formula",
                Price = 129,
                DiscountPrice = 119,
                CategoryId = categories[3].CategoryId,
                ImageUrl = "/images/products/lifebuoy-handwash.jpg",
                StockQuantity = 350,
                IsFeatured = false,
                IsActive = true,
                Unit = "750 ml"
            }
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();

        // Seed a test user
        var passwordHasher = new PasswordHasher();
        var testUser = new User
        {
            Email = "test@quickmart.com",
            PasswordHash = passwordHasher.HashPassword("Test@123"),
            FirstName = "Test",
            LastName = "User",
            PhoneNumber = "9876543210",
            Role = UserRole.Customer,
            IsActive = true
        };

        await context.Users.AddAsync(testUser);
        await context.SaveChangesAsync();

        // Create a cart for the test user
        var testCart = new Cart
        {
            UserId = testUser.UserId
        };

        await context.Carts.AddAsync(testCart);
        await context.SaveChangesAsync();
    }
}
