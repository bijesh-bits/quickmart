import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { ProductService } from '../../../../core/services/product.service';
import { CategoryService } from '../../../../core/services/category.service';
import { CartService } from '../../../../core/services/cart.service';
import { Product, Category } from '../../../../core/models';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  products: Product[] = [];
  categories: Category[] = [];
  selectedCategory: Category | null = null;
  searchQuery = '';
  layout: 'grid' | 'list' = 'grid';
  loading = false;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private cartService: CartService,
    private router: Router,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.loadCategories();
    this.loadProducts();
  }

  loadCategories(): void {
    this.categoryService.getAll().subscribe({
      next: (categories) => {
        this.categories = categories;
      },
      error: (error) => {
        console.error('Error loading categories:', error);
      }
    });
  }

  loadProducts(): void {
    this.loading = true;
    
    if (this.searchQuery.trim()) {
      this.productService.search(this.searchQuery).subscribe({
        next: (products) => {
          this.products = products;
          this.loading = false;
        },
        error: (error) => {
          console.error('Error searching products:', error);
          this.loading = false;
        }
      });
    } else if (this.selectedCategory) {
      this.productService.getByCategory(this.selectedCategory.categoryId).subscribe({
        next: (products) => {
          this.products = products;
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading products by category:', error);
          this.loading = false;
        }
      });
    } else {
      this.productService.getAll().subscribe({
        next: (products) => {
          this.products = products;
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading products:', error);
          this.loading = false;
        }
      });
    }
  }

  onCategoryChange(): void {
    this.searchQuery = '';
    this.loadProducts();
  }

  onSearch(): void {
    this.selectedCategory = null;
    this.loadProducts();
  }

  viewProduct(product: Product): void {
    this.router.navigate(['/products', product.productId]);
  }

  addToCart(product: Product, event: Event): void {
    event.stopPropagation();
    
    if (product.stockQuantity <= 0) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Out of Stock',
        detail: `${product.productName} is currently out of stock`
      });
      return;
    }

    this.cartService.addToCart({ productId: product.productId, quantity: 1 }).subscribe({
      next: () => {
        this.messageService.add({
          severity: 'success',
          summary: 'Added to Cart',
          detail: `${product.productName} added to your cart`
        });
      },
      error: (error) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: error.error?.message || 'Failed to add item to cart'
        });
      }
    });
  }

  getDiscountPercentage(product: Product): number {
    if (product.discountPrice) {
      return Math.round(((product.price - product.discountPrice) / product.price) * 100);
    }
    return 0;
  }

  getProductImageUrl(imageUrl: string | undefined): string {
    return this.productService.getImageUrl(imageUrl);
  }

  handleImageError(event: any): void {
    event.target.src = 'data:image/svg+xml,%3Csvg xmlns="http://www.w3.org/2000/svg" width="300" height="200"%3E%3Crect width="300" height="200" fill="%23f0f0f0"/%3E%3Ctext x="50%25" y="50%25" dominant-baseline="middle" text-anchor="middle" font-family="Arial" font-size="16" fill="%23999"%3ENo Image%3C/text%3E%3C/svg%3E';
    event.target.onerror = null; // Prevent infinite loop
  }
}
