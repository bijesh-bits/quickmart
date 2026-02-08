export interface Cart {
  cartId: number;
  userId: number;
  items: CartItem[];
  totalPrice: number;
}

export interface CartItem {
  cartItemId: number;
  cartId: number;
  productId: number;
  quantity: number;
  unitPrice: number;
  subtotal: number;
  productName?: string;
  productImage?: string;
  stockQuantity?: number;
}

export interface AddToCartRequest {
  productId: number;
  quantity: number;
}

export interface UpdateCartItemRequest {
  quantity: number;
}
