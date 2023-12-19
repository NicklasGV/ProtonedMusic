import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StripeChekoutModel } from '../../Models/StripeChekoutItems';
import { CartItem } from 'src/app/Models/CartModel';
import { ActivatedRoute } from '@angular/router';
import { PaymentService } from 'src/app/Services/payment.service';

@Component({
  selector: 'app-ordersuccess',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ordersuccess.component.html',
  styleUrl: './ordersuccess.component.css'
})
export class OrdersuccessComponent {
  cartItems: CartItem[] = [];

  constructor(
    private route: ActivatedRoute,
    private paymentService: PaymentService) {}

    ngOnInit(): void {

    }

}
