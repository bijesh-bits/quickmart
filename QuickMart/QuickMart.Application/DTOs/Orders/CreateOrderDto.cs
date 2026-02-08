namespace QuickMart.Application.DTOs.Orders;

public class CreateOrderDto
{
    public string ShippingAddress { get; set; } = string.Empty;
    public string ShippingCity { get; set; } = string.Empty;
    public string ShippingZipCode { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public string CardExpiry { get; set; } = string.Empty;
    public string CardCvv { get; set; } = string.Empty;
    public string CardHolderName { get; set; } = string.Empty;
}
