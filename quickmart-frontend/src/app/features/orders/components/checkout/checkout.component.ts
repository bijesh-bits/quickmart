import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { CartService } from '../../../../core/services/cart.service';
import { OrderService } from '../../../../core/services/order.service';
import { Cart, PaymentMethod } from '../../../../core/models';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
  cart: Cart | null = null;
  checkoutForm!: FormGroup;
  loading = false;
  submitting = false;
  
  paymentMethods = [
    { label: 'Credit Card', value: PaymentMethod.CreditCard },
    { label: 'Debit Card', value: PaymentMethod.DebitCard },
    { label: 'UPI', value: PaymentMethod.UPI },
    { label: 'Net Banking', value: PaymentMethod.NetBanking },
    { label: 'Cash on Delivery', value: PaymentMethod.CashOnDelivery }
  ];

  constructor(
    private formBuilder: FormBuilder,
    private cartService: CartService,
    private orderService: OrderService,
    private router: Router,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadCart();
  }

  initForm(): void {
    this.checkoutForm = this.formBuilder.group({
      shippingAddress: ['', [Validators.required, Validators.minLength(10)]],
      city: ['', [Validators.required, Validators.minLength(2)]],
      zipCode: ['', [Validators.required, Validators.pattern(/^[0-9]{6}$/)]],
      paymentMethod: [PaymentMethod.CreditCard, Validators.required],
      cardHolderName: [''],
      cardNumber: [''],
      expiryDate: [''],
      cvv: ['']
    });

    // Add conditional validators based on payment method
    this.checkoutForm.get('paymentMethod')?.valueChanges.subscribe(method => {
      this.updatePaymentValidators(method);
    });
    this.updatePaymentValidators(this.checkoutForm.get('paymentMethod')?.value);
  }

  updatePaymentValidators(paymentMethod: PaymentMethod): void {
    const cardHolderName = this.checkoutForm.get('cardHolderName');
    const cardNumber = this.checkoutForm.get('cardNumber');
    const expiryDate = this.checkoutForm.get('expiryDate');
    const cvv = this.checkoutForm.get('cvv');

    if (paymentMethod === PaymentMethod.CreditCard || paymentMethod === PaymentMethod.DebitCard) {
      cardHolderName?.setValidators([Validators.required, Validators.minLength(3)]);
      cardNumber?.setValidators([Validators.required, Validators.pattern(/^[0-9]{4}-?[0-9]{4}-?[0-9]{4}-?[0-9]{4}$/)]);
      expiryDate?.setValidators([Validators.required, Validators.pattern(/^(0[1-9]|1[0-2])\/?([0-9]{2})$/)]);
      cvv?.setValidators([Validators.required, Validators.pattern(/^[0-9]{3}$/)]);
    } else {
      cardHolderName?.clearValidators();
      cardNumber?.clearValidators();
      expiryDate?.clearValidators();
      cvv?.clearValidators();
    }

    cardHolderName?.updateValueAndValidity();
    cardNumber?.updateValueAndValidity();
    expiryDate?.updateValueAndValidity();
    cvv?.updateValueAndValidity();
  }

  loadCart(): void {
    this.loading = true;
    this.cartService.getCart().subscribe({
      next: (cart) => {
        this.cart = cart;
        if (!cart || cart.items.length === 0) {
          this.messageService.add({
            severity: 'warn',
            summary: 'Empty Cart',
            detail: 'Your cart is empty. Redirecting to shop...'
          });
          setTimeout(() => this.router.navigate(['/dashboard']), 2000);
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading cart:', error);
        this.loading = false;
      }
    });
  }

  get f() {
    return this.checkoutForm.controls;
  }

  isCardPayment(): boolean {
    const method = this.checkoutForm.get('paymentMethod')?.value;
    return method === PaymentMethod.CreditCard || method === PaymentMethod.DebitCard;
  }

  onSubmit(): void {
    if (this.checkoutForm.invalid) {
      this.markFormGroupTouched(this.checkoutForm);
      this.messageService.add({
        severity: 'error',
        summary: 'Validation Error',
        detail: 'Please fill all required fields correctly'
      });
      return;
    }

    if (!this.cart || this.cart.items.length === 0) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Empty Cart',
        detail: 'Your cart is empty'
      });
      return;
    }

    this.submitting = true;
    const orderData = this.checkoutForm.value;

    this.orderService.createOrder(orderData).subscribe({
      next: (order) => {
        this.messageService.add({
          severity: 'success',
          summary: 'Order Placed',
          detail: `Your order #${order.orderId} has been placed successfully!`
        });
        
        // Clear the cart
        this.cartService.clearCart().subscribe();
        
        // Redirect to orders page
        setTimeout(() => {
          this.router.navigate(['/orders']);
        }, 2000);
      },
      error: (error) => {
        this.submitting = false;
        this.messageService.add({
          severity: 'error',
          summary: 'Order Failed',
          detail: error.error?.message || 'Failed to place order. Please try again.'
        });
      }
    });
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();
    });
  }
}
