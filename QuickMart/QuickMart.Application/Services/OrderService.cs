using QuickMart.Application.DTOs.Orders;
using QuickMart.Application.Interfaces;
using QuickMart.Core.Entities;
using QuickMart.Core.Enums;
using QuickMart.Core.Interfaces;

namespace QuickMart.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(
        IOrderRepository orderRepository,
        ICartRepository cartRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task<OrderDto> CreateOrderAsync(int userId, CreateOrderDto request)
    {
        // Get cart with items
        var cart = await _cartRepository.GetCartWithItemsAsync(userId);
        
        if (cart == null || !cart.CartItems.Any())
        {
            throw new InvalidOperationException("Cart is empty");
        }

        // Validate stock availability
        foreach (var cartItem in cart.CartItems)
        {
            var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
            if (product!.StockQuantity < cartItem.Quantity)
            {
                throw new InvalidOperationException($"Insufficient stock for {product.ProductName}");
            }
        }

        // Calculate order amounts
        decimal totalAmount = cart.CartItems.Sum(ci => 
            (ci.Product.DiscountPrice ?? ci.Product.Price) * ci.Quantity);
        decimal discountAmount = cart.CartItems.Sum(ci => 
            ci.Product.DiscountPrice.HasValue ? (ci.Product.Price - ci.Product.DiscountPrice.Value) * ci.Quantity : 0);
        decimal taxAmount = totalAmount * 0.05m; // 5% tax
        decimal finalAmount = totalAmount + taxAmount;

        // Generate order number
        var orderNumber = await _orderRepository.GenerateOrderNumberAsync();

        // Create order
        var order = new Order
        {
            UserId = userId,
            OrderNumber = orderNumber,
            TotalAmount = totalAmount,
            DiscountAmount = discountAmount,
            TaxAmount = taxAmount,
            FinalAmount = finalAmount,
            ShippingAddress = request.ShippingAddress,
            ShippingCity = request.ShippingCity,
            ShippingZipCode = request.ShippingZipCode,
            Status = OrderStatus.Pending
        };

        // Add order items
        foreach (var cartItem in cart.CartItems)
        {
            order.OrderItems.Add(new OrderItem
            {
                ProductId = cartItem.ProductId,
                ProductName = cartItem.Product.ProductName,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.Product.Price,
                DiscountPrice = cartItem.Product.DiscountPrice,
                TotalPrice = (cartItem.Product.DiscountPrice ?? cartItem.Product.Price) * cartItem.Quantity
            });
        }

        // Process mock payment
        var transactionId = $"TXN-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid():N}".Substring(0, 50);
        
        var payment = new Payment
        {
            PaymentMethod = Enum.Parse<PaymentMethod>(request.PaymentMethod, true),
            TransactionId = transactionId,
            PaymentStatus = PaymentStatus.Completed, // Mock payment always succeeds
            Amount = finalAmount,
            CardLastFourDigits = request.CardNumber.Length >= 4 ? request.CardNumber.Substring(request.CardNumber.Length - 4) : "****",
            CardType = DetermineCardType(request.CardNumber)
        };

        order.Payment = payment;
        order.Status = OrderStatus.Processing; // Update status after successful payment

        // Update product stock
        foreach (var cartItem in cart.CartItems)
        {
            var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
            product!.StockQuantity -= cartItem.Quantity;
            await _productRepository.UpdateAsync(product);
        }

        // Save order
        await _orderRepository.AddAsync(order);

        // Clear cart
        cart.CartItems.Clear();
        await _cartRepository.UpdateAsync(cart);

        // Return order DTO
        var createdOrder = await _orderRepository.GetOrderWithDetailsAsync(order.OrderId);
        return MapToDto(createdOrder!);
    }

    public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(int userId)
    {
        var orders = await _orderRepository.GetByUserIdAsync(userId);
        return orders.Select(MapToDto);
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int orderId, int userId)
    {
        var order = await _orderRepository.GetOrderWithDetailsAsync(orderId);
        
        if (order == null || order.UserId != userId)
        {
            return null;
        }

        return MapToDto(order);
    }

    private OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            OrderId = order.OrderId,
            OrderNumber = order.OrderNumber,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            DiscountAmount = order.DiscountAmount,
            TaxAmount = order.TaxAmount,
            FinalAmount = order.FinalAmount,
            Status = order.Status.ToString(),
            ShippingAddress = order.ShippingAddress,
            ShippingCity = order.ShippingCity,
            ShippingZipCode = order.ShippingZipCode,
            Items = order.OrderItems.Select(oi => new OrderItemDto
            {
                OrderItemId = oi.OrderItemId,
                ProductId = oi.ProductId,
                ProductName = oi.ProductName,
                ProductImageUrl = oi.Product.ImageUrl,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                DiscountPrice = oi.DiscountPrice,
                TotalPrice = oi.TotalPrice
            }).ToList(),
            Payment = order.Payment != null ? new PaymentDto
            {
                PaymentId = order.Payment.PaymentId,
                PaymentMethod = order.Payment.PaymentMethod.ToString(),
                TransactionId = order.Payment.TransactionId,
                PaymentStatus = order.Payment.PaymentStatus.ToString(),
                Amount = order.Payment.Amount,
                PaymentDate = order.Payment.PaymentDate,
                CardLastFourDigits = order.Payment.CardLastFourDigits,
                CardType = order.Payment.CardType
            } : null
        };
    }

    private string DetermineCardType(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 2)
            return "Unknown";

        var firstTwo = cardNumber.Substring(0, 2);
        
        return firstTwo switch
        {
            "4" => "Visa",
            "51" or "52" or "53" or "54" or "55" => "Mastercard",
            "34" or "37" => "American Express",
            "60" or "65" => "Discover",
            _ => "Unknown"
        };
    }
}
