import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartItem } from 'src/app/Models/CartModel';


@Component({
  selector: 'app-ordersuccess',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ordersuccess.component.html',
  styleUrl: './ordersuccess.component.css'
})
export class OrdersuccessComponent {
  cartItems: CartItem[] = [];

  constructor() {}

    ngOnInit(): void {

    }

}
