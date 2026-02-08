import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService, ConfirmationService } from 'primeng/api';
import { CartService } from '../../../../core/services/cart.service';
import { ProductService } from '../../../../core/services/product.service';
import { Cart, CartItem } from '../../../../core/models';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  cart: Cart | null = null;
  loading = false;
  updatingItem: number | null = null;
  imageUrls: { [productId: number]: string } = {};

  constructor(
    private cartService: CartService,
    private productService: ProductService,
    private router: Router,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.loadCart();
  }

  getProductImageUrl(imageUrl: string | undefined | null): string {
    return this.productService.getImageUrl(imageUrl ?? undefined);
  }

  handleImageError(event: any): void {
    event.target.src = 'data:image/svg+xml,%3Csvg xmlns="http://www.w3.org/2000/svg" width="300" height="200"%3E%3Crect width="300" height="200" fill="%23f0f0f0"/%3E%3Ctext x="50%25" y="50%25" dominant-baseline="middle" text-anchor="middle" font-family="Arial" font-size="16" fill="%23999"%3ENo Image%3C/text%3E%3C/svg%3E';
    event.target.onerror = null; // Prevent infinite loop
  }

  loadCart(): void {
    this.loading = true;
    this.cartService.getCart().subscribe({
      next: (cart) => {
        this.cart = cart;
        // Pre-process image URLs to avoid repeated function calls in template
        if (cart && cart.items) {
          cart.items.forEach(item => {
            this.imageUrls[item.productId] = this.productService.getImageUrl(item.productImage);
          });
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading cart:', error);
        this.loading = false;
      }
    });
  }

  checkout(): void {
    this.router.navigate(['/checkout']);
  }

  updateQuantity(item: CartItem, newQuantity: number): void {
    if (newQuantity < 1) return;
    
    if (item.stockQuantity && newQuantity > item.stockQuantity) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Insufficient Stock',
        detail: `Only ${item.stockQuantity} items available`
      });
      return;
    }

    this.updatingItem = item.cartItemId;
    this.cartService.updateCartItem(item.cartItemId, { quantity: newQuantity }).subscribe({
      next: (cart) => {
        this.cart = cart;
        // Re-process image URLs after cart update
        if (cart && cart.items) {
          cart.items.forEach(item => {
            this.imageUrls[item.productId] = this.productService.getImageUrl(item.productImage);
          });
        }
        this.updatingItem = null;
        this.messageService.add({
          severity: 'success',
          summary: 'Updated',
          detail: 'Cart item updated'
        });
      },
      error: (error) => {
        this.updatingItem = null;
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: error.error?.message || 'Failed to update cart item'
        });
      }
    });
  }

  removeItem(item: CartItem): void {
    this.confirmationService.confirm({
      message: `Remove ${item.productName} from cart?`,
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.cartService.removeCartItem(item.cartItemId).subscribe({
          next: (cart) => {
            this.cart = cart;
            // Re-process image URLs after cart update
            if (cart && cart.items) {
              cart.items.forEach(item => {
                this.imageUrls[item.productId] = this.productService.getImageUrl(item.productImage);
              });
            }
            this.messageService.add({
              severity: 'success',
              summary: 'Removed',
              detail: 'Item removed from cart'
            });
          },
          error: (error) => {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: error.error?.message || 'Failed to remove item'
            });
          }
        });
      }
    });
  }

  clearCart(): void {
    this.confirmationService.confirm({
      message: 'Are you sure you want to clear your cart?',
      header: 'Clear Cart',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.cartService.clearCart().subscribe({
          next: () => {
            this.cart = null;
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Cart cleared'
            });
          },
          error: (error) => {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: error.error?.message || 'Failed to clear cart'
            });
          }
        });
      }
    });
  }

  proceedToCheckout(): void {
    if (!this.cart || this.cart.items.length === 0) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Empty Cart',
        detail: 'Please add items to your cart before checkout'
      });
      return;
    }
    this.router.navigate(['/checkout']);
  }

  continueShopping(): void {
    this.router.navigate(['/dashboard']);
  }
}
