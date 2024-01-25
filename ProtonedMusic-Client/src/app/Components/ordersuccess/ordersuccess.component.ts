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

  constructor(private cartService: CartService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    // Hent data fra localStorage, inklusive købte varer
    this.cartItems = this.cartService.getFullCart();


  }

}
