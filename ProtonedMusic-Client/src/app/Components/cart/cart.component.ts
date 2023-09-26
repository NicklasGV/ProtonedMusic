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

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = [];
  amount: number = 1;
  constructor(public cartService: CartService, private authService:AuthService) { }

  ngOnInit(): void {
    /* this.cartService.cart.subscribe(x => this.cartItems = x); */
  }

  // OBS! this method belongs on productpage and other places where items can be placed in basket
  addToCart(item?: CartItem): void {

  }

  clearCart(): void {
    if(confirm('Er du sikker på du vil tømme din kurv?'))
    {
      this.cartService.clearCart();
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
    if (confirm(`Er du sikker på du vil fjerne ${item.id} ${item.name}?`)) {
      this.cartItems = this.cartItems.filter(x => x.id != item.id);
    }
  }


}
