import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CheckoutModel } from '../Models/CheckoutModel';

@Injectable({
  providedIn: 'root'
})

export class PaymentService {
  private readonly url = environment.apiUrl + 'Checkout';

  constructor(private http: HttpClient) {}

  public createCheckoutSession(checkoutData: CheckoutModel): Observable<any> {
    const stripeAPIURL = 'https://localhost:7149/Checkout/CreateCheckoutSession'; // Erstat med Stripe's API-endepunkt
    const stripeAPIKey = 'sk_test_51MawfMFFxCTt81aXVC5LLXg1nzTYwEQLM20LidrDRVjR3FDF3SKhazAzDgaR9871rABLvbotyuLA14hjqYmboS2x00ujPqdm9F'; // Erstat med din Stripe API-nøgle

    const httpOptions = {
      headers: {
        'Authorization': `Bearer ${stripeAPIKey}`,
        'Content-Type': 'application/json'
      }
    };

    return this.http.post<any>(stripeAPIURL, checkoutData, httpOptions);
  }

}

//create-checkout-session
