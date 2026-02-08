namespace QuickMart.Application.DTOs.Cart;

public class CartItemDto
{
    public int CartItemId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public decimal FinalPrice => DiscountPrice ?? Price;
    public int Quantity { get; set; }
    public decimal Subtotal => FinalPrice * Quantity;
    public string Unit { get; set; } = string.Empty;
}
