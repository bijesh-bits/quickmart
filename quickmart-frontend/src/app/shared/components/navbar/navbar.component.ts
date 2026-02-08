import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { MenuItem } from 'primeng/api';
import { AuthService } from '../../../core/services/auth.service';
import { CartService } from '../../../core/services/cart.service';
import { User } from '../../../core/models';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, OnDestroy {
  menuItems: MenuItem[] = [];
  currentUser: User | null = null;
  cartItemCount = 0;
  private destroy$ = new Subject<void>();

  constructor(
    private authService: AuthService,
    private cartService: CartService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Subscribe to current user
    this.authService.currentUser$
      .pipe(takeUntil(this.destroy$))
      .subscribe(user => {
        this.currentUser = user;
        this.updateMenuItems();
      });

    // Subscribe to cart updates
    this.cartService.cart$
      .pipe(takeUntil(this.destroy$))
      .subscribe(cart => {
        this.cartItemCount = cart ? cart.items.reduce((count, item) => count + item.quantity, 0) : 0;
        this.updateMenuItems(); // Update menu items when cart changes
      });

    // Always load cart (works for both authenticated and guest users)
    this.cartService.getCart().subscribe();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  updateMenuItems(): void {
    const cartMenuItem: MenuItem = {
      label: 'Cart',
      icon: 'pi pi-shopping-cart',
      badge: this.cartItemCount > 0 ? String(this.cartItemCount) : undefined,
      badgeStyleClass: 'cart-badge',
      styleClass: 'cart-menu-item', // Add a specific class for the menu item
      command: () => this.router.navigate(['/cart'])
    };

    if (this.currentUser) {
      this.menuItems = [
        {
          label: 'Home',
          icon: 'pi pi-home',
          command: () => this.router.navigate(['/dashboard'])
        },
        cartMenuItem,
        {
          label: 'My Orders',
          icon: 'pi pi-list',
          command: () => this.router.navigate(['/orders'])
        },
        {
          label: this.currentUser.firstName,
          icon: 'pi pi-user',
          items: [
            {
              label: 'Profile',
              icon: 'pi pi-user',
              disabled: false,
              styleClass: 'profile-menu-item',
            },
            {
              separator: true
            },
            {
              label: 'Logout',
              icon: 'pi pi-sign-out',
              command: () => this.logout(),
              styleClass: 'profile-menu-item',
            }
          ]
        }
      ];
    } else {
      this.menuItems = [
        {
          label: 'Home',
          icon: 'pi pi-home',
          command: () => this.router.navigate(['/dashboard'])
        },
        cartMenuItem,
        {
          label: 'Login',
          icon: 'pi pi-sign-in',
          command: () => this.router.navigate(['/login'])
        }
      ];
    }
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
