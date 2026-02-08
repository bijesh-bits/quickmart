using QuickMart.Application.DTOs.Cart;

namespace QuickMart.Application.Interfaces;

public interface ICartService
{
    Task<CartDto> GetCartAsync(int userId);
    Task<CartDto> AddToCartAsync(int userId, AddToCartDto request);
    Task<CartDto> UpdateCartItemAsync(int userId, UpdateCartItemDto request);
    Task<CartDto> RemoveFromCartAsync(int userId, int cartItemId);
    Task ClearCartAsync(int userId);
}
