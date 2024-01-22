import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CartItem } from '../../Models/CartModel';
import { CartService } from '../../Services/cart.service';

import { MatCardModule } from '@angular/material/card';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../Services/auth.service';
import { ProductModel } from 'src/app/Models/ProductModel';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

import { loadStripe } from '@stripe/stripe-js';
import { CheckoutModel } from 'src/app/Models/CheckoutModel';
import { PaymentService } from 'src/app/Services/payment.service';

import { StripeChekoutModel } from 'src/app/Models/StripeChekoutItems';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, MatCardModule],
  templateUrl: './cart.component.html',
  styles: [],
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = [];
  products: ProductModel[] = [];
  amount: number = 1;
  constructor(
    public cartService: CartService,
    private authService: AuthService,
    private snackBar: SnackBarService,
    private dialog: MatDialog,
    private paymentService: PaymentService
  ) {}

  ngOnInit(): void {
    this.cartService.currentCart.subscribe((x) => (this.cartItems = x));
  }

  clearCart(): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        title: 'Clear cart',
        message: 'Are you sure you want to clear your cart?',
        confirmYes: 'Confirm',
        confirmNo: 'Cancel'
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.cartService.clearCart();
        this.snackBar.openSnackBar('Clearing successful.', '', 'success');
      } else {
        // User canceled the operation
        this.snackBar.openSnackBar('Clearing canceled.', '', 'warning');
      }
    });
  }

  formatCurrency(amount: number): string {
    return amount.toLocaleString('da-DK') + ' DKK';
  } 

  updateCart(item: CartItem): void {
    const index = this.cartItems.findIndex(
      (cartItem) => cartItem.id === item.id
    );

    if (index !== -1 && this.cartItems[index].quantity > 0) {
      this.cartItems[index].quantity = item.quantity;
      this.cartService.saveCart(this.cartItems);
    } else if (index !== -1 && this.cartItems[index].quantity === 0) {
      const dialogRef = this.dialog.open(DialogComponent, {
        data: {
          title: 'Remove Item?',
          message: 'Are you sure you want to delete this item?',
          confirmYes: 'Confirm',
          confirmNo: 'Cancel'
        },
      });
      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          this.cartItems.splice(index, 1);
          this.cartService.saveCart(this.cartItems);
          this.snackBar.openSnackBar('Clearing successful.', '', 'success');
        } else {
          // User canceled the operation
          this.snackBar.openSnackBar('Clearing canceled.', '', 'warning');
        }
      });
    }
  }

  buyCartItems(): void {
    if (this.authService.currentUserValue.email === '') {
      this.snackBar.openSnackBar('You must be logged in to purchase items.', '', 'warning');
    } else {
      this.snackBar.openSnackBar('Buying successful.', '', 'success');
    }

    const stripeCheckoutItems: StripeChekoutModel[] = this.cartItems.map((item) => {
      return {
        name: item.name,
        unitAmount: item.price,
        quantity: item.quantity,
        price: item.price,
      };
    });

    this.paymentService.CreateCheckoutSession(stripeCheckoutItems, this.authService.currentUserValue.email).subscribe(
      (response) => {
        console.log('Session created:', response);
        this.initiateStripeCheckout(response);
      },
      (error) => {
        console.error('Error creating session:', error);
      }
    );
  }

  private initiateStripeCheckout(checkoutData: CheckoutModel): void {
    loadStripe('pk_test_51MawfMFFxCTt81aXOvpKeSzT34kMWgpEgfkaCwX3EJqE3nEtp0z9qUDQbgd3yTIKppstc2xGKsV3pXIlb33p92eJ00N01PxT3Q').then((stripe) => {
      stripe?.redirectToCheckout({
        sessionId: checkoutData.sessionId,
      });
    });
  }

  removeItem(item: CartItem): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        title: 'Remove Item(s)?',
        message: 'Are you sure you want to delete the item(s)?',
        confirmYes: 'Confirm',
        confirmNo: 'Cancel'
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.cartService.removeItemFromCart(item.id);
        this.snackBar.openSnackBar('Clearing successful.', '', 'success');
      } else {
        // User canceled the operation
        this.snackBar.openSnackBar('Clearing canceled.', '', 'warning');
      }
    });
  }
}
