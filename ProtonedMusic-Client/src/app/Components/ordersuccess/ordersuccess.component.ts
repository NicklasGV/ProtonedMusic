import { CartService } from 'src/app/Services/cart.service';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartItem } from 'src/app/Models/CartModel';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-ordersuccess',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ordersuccess.component.html',
  styleUrl: './ordersuccess.component.css',
})
export class OrdersuccessComponent {
  cartItems: CartItem[] = [];

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    // Hent data fra localStorage, inklusive købte varer
    const temporaryCartItems = this.cartService.getFullCart();
    console.log('CartItems from localStorage:', temporaryCartItems);

    // Opdater currentCartSubject med de midlertidige varer
    this.cartService.currentCartSubject.next(temporaryCartItems);

    // Tøm indkøbskurven efter at have hentet data
    this.cartService.clearCart();

    // Nu har du adgang til købte varer i temporaryCartItems
    this.cartItems = temporaryCartItems;
  }
}
