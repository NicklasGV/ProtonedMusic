import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Cart, CartItem } from '../../Models/CartModel';
import { CartService } from '../../Services/cart.service';

/* import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon'; */
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../Services/auth.service';
import { ProductModel } from 'src/app/Models/ProductModel';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './cart.component.html',
  styles: []
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = [];
  products: ProductModel[] = [];
  amount: number = 1;
  constructor(public cartService: CartService, private authService:AuthService, private snackBar: SnackBarService,private dialog: MatDialog) { }


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
        this.snackBar.openSnackBar('Deletion successful.', '','success');
        console.log('Product deleted!');
      } else {
        // User canceled the operation
        this.snackBar.openSnackBar('Deletion canceled.', '','warning');
        console.log('Deletion canceled.');
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
      if (confirm(`Er du sikker på du vil fjerne ${item.name}?`))
      {
        this.cartItems.splice(index, 1);
        this.cartService.saveCart(this.cartItems);
      }
     }
  }

  buyCartItems(): void {
    /* if (this.authService.CurrentUserValue.mail == "")
    {
      alert("Du skal være logget ind for at kunne købe dine varer")
    } else {
      console.log(this.cartItems)
    } */
  }

  removeItem(item: CartItem): void {
    if (confirm(`Er du sikker på du vil fjerne ${item.name}?`)) {
      this.cartService.removeItemFromCart(item.id);
    }
  }
}
