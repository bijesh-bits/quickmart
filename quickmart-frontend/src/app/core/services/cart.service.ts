import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap, of, switchMap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Cart, AddToCartRequest, UpdateCartItemRequest, CartItem } from '../models';
import { AuthService } from './auth.service';
import { ProductService } from './product.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = `${environment.apiUrl}/cart`;
  private cartSubject = new BehaviorSubject<Cart | null>(null);
  public cart$ = this.cartSubject.asObservable();
  private readonly CART_STORAGE_KEY = 'quickmart_guest_cart';

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private productService: ProductService
  ) {
    this.loadGuestCart();
  }

  private loadGuestCart(): void {
    if (!this.authService.isAuthenticated()) {
      const savedCart = localStorage.getItem(this.CART_STORAGE_KEY);
      if (savedCart) {
        this.cartSubject.next(JSON.parse(savedCart));
      }
    }
  }

  private saveGuestCart(cart: Cart): void {
    localStorage.setItem(this.CART_STORAGE_KEY, JSON.stringify(cart));
  }

  private clearGuestCart(): void {
    localStorage.removeItem(this.CART_STORAGE_KEY);
  }

  private createGuestCart(): Cart {
    return {
      cartId: 0,
      userId: 0,
      items: [],
      totalPrice: 0
    };
  }

  private calculateTotal(items: CartItem[]): number {
    return items.reduce((total, item) => total + (item.unitPrice * item.quantity), 0);
  }

  getCart(): Observable<Cart> {
    if (!this.authService.isAuthenticated()) {
      const cart = this.cartSubject.value || this.createGuestCart();
      return of(cart);
    }
    return this.http.get<Cart>(this.apiUrl)
      .pipe(
        tap(cart => this.cartSubject.next(cart))
      );
  }

  addToCart(request: AddToCartRequest): Observable<Cart> {
    if (!this.authService.isAuthenticated()) {
      // For guest users, fetch product details first
      return this.productService.getById(request.productId).pipe(
        switchMap(product => {
          const cart = this.cartSubject.value || this.createGuestCart();
          const existingItem = cart.items.find(item => item.productId === request.productId);
          
          if (existingItem) {
            existingItem.quantity += request.quantity;
            existingItem.subtotal = existingItem.unitPrice * existingItem.quantity;
          } else {
            const newItem: CartItem = {
              cartItemId: Date.now(),
              cartId: 0,
              productId: request.productId,
              quantity: request.quantity,
              unitPrice: product.discountPrice || product.price,
              subtotal: (product.discountPrice || product.price) * request.quantity,
              productName: product.productName,
              productImage: this.productService.getImageUrl(product.imageUrl)
            };
            cart.items.push(newItem);
          }
          
          cart.totalPrice = this.calculateTotal(cart.items);
          this.cartSubject.next(cart);
          this.saveGuestCart(cart);
          return of(cart);
        })
      );
    }
    return this.http.post<Cart>(`${this.apiUrl}/add`, request)
      .pipe(
        tap(cart => this.cartSubject.next(cart))
      );
  }

  updateCartItem(itemId: number, request: UpdateCartItemRequest): Observable<Cart> {
    if (!this.authService.isAuthenticated()) {
      const cart = this.cartSubject.value || this.createGuestCart();
      const item = cart.items.find(i => i.cartItemId === itemId);
      if (item) {
        item.quantity = request.quantity;
        item.subtotal = item.unitPrice * item.quantity;
      }
      cart.totalPrice = this.calculateTotal(cart.items);
      this.cartSubject.next(cart);
      this.saveGuestCart(cart);
      return of(cart);
    }
    return this.http.put<Cart>(`${this.apiUrl}/items/${itemId}`, request)
      .pipe(
        tap(cart => this.cartSubject.next(cart))
      );
  }

  removeCartItem(itemId: number): Observable<Cart> {
    if (!this.authService.isAuthenticated()) {
      const cart = this.cartSubject.value || this.createGuestCart();
      cart.items = cart.items.filter(item => item.cartItemId !== itemId);
      cart.totalPrice = this.calculateTotal(cart.items);
      this.cartSubject.next(cart);
      this.saveGuestCart(cart);
      return of(cart);
    }
    return this.http.delete<Cart>(`${this.apiUrl}/items/${itemId}`)
      .pipe(
        tap(cart => this.cartSubject.next(cart))
      );
  }

  clearCart(): Observable<void> {
    if (!this.authService.isAuthenticated()) {
      this.cartSubject.next(this.createGuestCart());
      this.clearGuestCart();
      return of(void 0);
    }
    return this.http.delete<void>(`${this.apiUrl}/clear`)
      .pipe(
        tap(() => this.cartSubject.next(null))
      );
  }

  getCartItemCount(): number {
    const cart = this.cartSubject.value;
    return cart ? cart.items.reduce((count, item) => count + item.quantity, 0) : 0;
  }
}
