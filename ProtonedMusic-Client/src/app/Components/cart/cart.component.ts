import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Cart, CartItem } from '../../Models/CartModel';
import { CartService } from '../../Services/cart.service';

import { MatCardModule } from '@angular/material/card';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../Services/auth.service';
import { ProductModel } from 'src/app/Models/ProductModel';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

import { loadStripe } from '@stripe/stripe-js';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, MatCardModule],
  templateUrl: './cart.component.html',
  styles: []
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = [];
  products: ProductModel[] = [];
  amount: number = 1;
  constructor(public cartService: CartService, private authService:AuthService,
     private snackBar: SnackBarService,private dialog: MatDialog,
     private http: HttpClient) { }


  ngOnInit(): void {
    this.cartService.currentCart.subscribe(x => this.cartItems = x);
    console.log(this.cartItems);
  }

  clearCart(): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: { title: "Clear cart", message: "Are you sure you want to clear your cart?" }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.cartService.clearCart();
        this.snackBar.openSnackBar('Clearing successful.', '','success');
        console.log('Product deleted!');
      } else {
        // User canceled the operation
        this.snackBar.openSnackBar('Clearing canceled.', '','warning');
        console.log('Clearing canceled.');
      }
    });
  }

  updateCart(item: CartItem): void {
     const index = this.cartItems.findIndex(cartItem => cartItem.id === item.id);

     if (index !== -1 && this.cartItems[index].quantity > 0) {
        this.cartItems[index].quantity = item.quantity;
        this.cartService.saveCart(this.cartItems);
     }
     else if (index !== -1 && this.cartItems[index].quantity === 0) {
      const dialogRef = this.dialog.open(DialogComponent, {
        data: { title: "Remove Item?", message: "Are you sure you want to delete this item?" }
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.cartItems.splice(index, 1);
        this.cartService.saveCart(this.cartItems);
          this.snackBar.openSnackBar('Clearing successful.', '','success');
          console.log('Product deleted!');
        } else {
          // User canceled the operation
          this.snackBar.openSnackBar('Clearing canceled.', '','warning');
          console.log('Clearing canceled.');
        }
      });
     }
  }

  buyCartItems(): void {
    if (this.authService.currentUserValue.email == "")
    {
      this.snackBar.openSnackBar('You must be logged in to purchase items.', '','warning');
    } else {
      console.log(this.cartItems)
      this.snackBar.openSnackBar('Buying successful.', '','success');
    }
    this.http
      .post('http://localhost:4242/checkout', {
        items: this.cartItems,
      })
      .subscribe(async (res: any) => {
        let stripe = await loadStripe('pk_test_51MawfMFFxCTt81aXOvpKeSzT34kMWgpEgfkaCwX3EJqE3nEtp0z9qUDQbgd3yTIKppstc2xGKsV3pXIlb33p92eJ00N01PxT3Q');
        stripe?.redirectToCheckout({
          sessionId: res.id,
        });
      });

  }

  removeItem(item: CartItem): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: { title: "Remove Item(s)?", message: "Are you sure you want to delete the item(s)?" }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.cartService.removeItemFromCart(item.id);
        this.snackBar.openSnackBar('Clearing successful.', '','success');
        console.log('Product deleted!');
      } else {
        // User canceled the operation
        this.snackBar.openSnackBar('Clearing canceled.', '','warning');
        console.log('Clearing canceled.');
      }
    });
   }

}
