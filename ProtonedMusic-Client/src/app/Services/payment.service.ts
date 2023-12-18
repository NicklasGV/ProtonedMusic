import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { StripeChekoutModel } from '../Models/StripeChekoutItems';
import { CheckoutModel } from '../Models/CheckoutModel';


@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private readonly url = environment.apiUrl;
  private static readonly stripeAPIKey = 'STRIPE_API_KEY';

  constructor(private http: HttpClient) { }

  public CreateCheckoutSession(cartItems: any[], customerEmail: string): Observable<any> {
    const stripeAPIURL = this.url + 'CreateCheckoutSession';
    const httpOptions = {
      headers: {
        'Authorization': `Bearer ${PaymentService.stripeAPIKey}`,
        'Content-Type': 'application/json'
      }
    };

    const checkoutData = {
      cartItems,
      customerEmail,
    };

    console.log('Sending CreateCheckoutSession request with data:', cartItems);
    return this.http.post<CheckoutModel>(stripeAPIURL, checkoutData, httpOptions);
  }
}
