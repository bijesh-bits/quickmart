export interface Order {
  orderId: number;
  userId: number;
  orderDate: Date;
  status: OrderStatus;
  totalAmount: number;
  shippingAddress: string;
  city: string;
  zipCode: string;
  items: OrderItem[];
  payment?: Payment;
}

export interface OrderItem {
  orderItemId: number;
  orderId: number;
  productId: number;
  quantity: number;
  unitPrice: number;
  subtotal: number;
  productName?: string;
  productImageUrl?: string;
}

export interface Payment {
  paymentId: number;
  orderId: number;
  paymentMethod: PaymentMethod;
  paymentStatus: PaymentStatus;
  amount: number;
  transactionId?: string;
  paymentDate?: Date;
}

export enum OrderStatus {
  Pending = 'Pending',
  Processing = 'Processing',
  Shipped = 'Shipped',
  Delivered = 'Delivered',
  Cancelled = 'Cancelled'
}

export enum PaymentMethod {
  CreditCard = 'CreditCard',
  DebitCard = 'DebitCard',
  UPI = 'UPI',
  NetBanking = 'NetBanking',
  CashOnDelivery = 'CashOnDelivery'
}

export enum PaymentStatus {
  Pending = 'Pending',
  Success = 'Success',
  Failed = 'Failed',
  Refunded = 'Refunded'
}

export interface CreateOrderRequest {
  shippingAddress: string;
  city: string;
  zipCode: string;
  paymentMethod: PaymentMethod;
  cardNumber?: string;
  cardHolderName?: string;
  expiryMonth?: number;
  expiryYear?: number;
  cvv?: string;
}
