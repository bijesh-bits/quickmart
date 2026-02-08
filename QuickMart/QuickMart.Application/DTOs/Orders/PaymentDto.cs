namespace QuickMart.Application.DTOs.Orders;

public class PaymentDto
{
    public int PaymentId { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? CardLastFourDigits { get; set; }
    public string? CardType { get; set; }
}
