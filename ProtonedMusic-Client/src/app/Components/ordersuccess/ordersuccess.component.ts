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
      // Hent sessionId fra ruteparametre
      this.route.params.subscribe(params => {
        const sessionId = params['sessionId'];

        // Hent ordreoplysninger baseret på sessionId fra din backend
        this.getOrderDetails(sessionId);
      });
    }
    getOrderDetails(orderId: string) {
      // Implementer logik for at hente ordreoplysninger fra din backend
      // Brug orderId til at hente data fra din backend
      // Du kan kalde en ny backend-tjeneste eller opdatere din nuværende service til at håndtere dette
      // Opdater cartItems med de hentede oplysninger
    }
}
