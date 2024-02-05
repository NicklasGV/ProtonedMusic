import { Component, OnInit, importProvidersFrom } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartItem } from 'src/app/Models/CartModel';
import { OrdersuccessService } from 'src/app/Services/ordersuccess.service';
import { CartService } from 'src/app/Services/cart.service';
import { AuthService } from 'src/app/Services/auth.service';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { MatDialog } from '@angular/material/dialog';
import { PaymentService } from 'src/app/Services/payment.service';
import { Stripe } from '@stripe/stripe-js';




@Component({
  selector: 'app-ordersuccess',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ordersuccess.component.html',
  styleUrl: './ordersuccess.component.css'
})
export class OrdersuccessComponent implements OnInit {
  cartItems: CartItem[] = [];
  testSuccess: boolean = false;



  constructor(
    private cartService: CartService,
    private orderSuccessService: OrdersuccessService,
    private authService: AuthService,
    private snackBar: SnackBarService,
    
    private dialog: MatDialog,
    private paymentService: PaymentService
  ) { }

  ngOnInit(): void {

    this.cartService.currentCart.subscribe((x) => (this.cartItems = x))

  }
 

}
