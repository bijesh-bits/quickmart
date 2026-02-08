using QuickMart.Application.DTOs.Cart;
using QuickMart.Application.Interfaces;
using QuickMart.Core.Entities;
using QuickMart.Core.Interfaces;

namespace QuickMart.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task<CartDto> GetCartAsync(int userId)
    {
        var cart = await _cartRepository.GetCartWithItemsAsync(userId);
        
        if (cart == null)
        {
            // Create cart if doesn't exist
            cart = new Cart { UserId = userId };
            await _cartRepository.AddAsync(cart);
        }

        return MapToDto(cart);
    }

    public async Task<CartDto> AddToCartAsync(int userId, AddToCartDto request)
    {
        var cart = await _cartRepository.GetCartWithItemsAsync(userId);
        
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            await _cartRepository.AddAsync(cart);
            cart = await _cartRepository.GetCartWithItemsAsync(userId);
        }

        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        if (product.StockQuantity < request.Quantity)
        {
            throw new InvalidOperationException("Insufficient stock");
        }

        // Check if product already in cart
        var existingItem = cart!.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId);
        
        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
            existingItem.PriceAtAdd = product.DiscountPrice ?? product.Price;
            await _cartRepository.UpdateAsync(cart);
        }
        else
        {
            cart.CartItems.Add(new CartItem
            {
                CartId = cart.CartId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                PriceAtAdd = product.DiscountPrice ?? product.Price
            });
            await _cartRepository.UpdateAsync(cart);
        }

        cart = await _cartRepository.GetCartWithItemsAsync(userId);
        return MapToDto(cart!);
    }

    public async Task<CartDto> UpdateCartItemAsync(int userId, UpdateCartItemDto request)
    {
        var cart = await _cartRepository.GetCartWithItemsAsync(userId);
        
        if (cart == null)
        {
            throw new InvalidOperationException("Cart not found");
        }

        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == request.CartItemId);
        
        if (cartItem == null)
        {
            throw new InvalidOperationException("Cart item not found");
        }

        if (request.Quantity <= 0)
        {
            throw new InvalidOperationException("Quantity must be greater than 0");
        }

        var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
        if (product!.StockQuantity < request.Quantity)
        {
            throw new InvalidOperationException("Insufficient stock");
        }

        cartItem.Quantity = request.Quantity;
        await _cartRepository.UpdateAsync(cart);

        cart = await _cartRepository.GetCartWithItemsAsync(userId);
        return MapToDto(cart!);
    }

    public async Task<CartDto> RemoveFromCartAsync(int userId, int cartItemId)
    {
        var cart = await _cartRepository.GetCartWithItemsAsync(userId);
        
        if (cart == null)
        {
            throw new InvalidOperationException("Cart not found");
        }

        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);
        
        if (cartItem != null)
        {
            cart.CartItems.Remove(cartItem);
            await _cartRepository.UpdateAsync(cart);
        }

        cart = await _cartRepository.GetCartWithItemsAsync(userId);
        return MapToDto(cart!);
    }

    public async Task ClearCartAsync(int userId)
    {
        var cart = await _cartRepository.GetCartWithItemsAsync(userId);
        
        if (cart != null)
        {
            cart.CartItems.Clear();
            await _cartRepository.UpdateAsync(cart);
        }
    }

    private CartDto MapToDto(Cart cart)
    {
        return new CartDto
        {
            CartId = cart.CartId,
            Items = cart.CartItems.Select(ci => new CartItemDto
            {
                CartItemId = ci.CartItemId,
                ProductId = ci.ProductId,
                ProductName = ci.Product.ProductName,
                ImageUrl = ci.Product.ImageUrl,
                Price = ci.Product.Price,
                DiscountPrice = ci.Product.DiscountPrice,
                Quantity = ci.Quantity,
                Unit = ci.Product.Unit
            }).ToList()
        };
    }
}
