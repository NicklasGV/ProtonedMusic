import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { StripeChekoutModel } from '../Models/StripeChekoutItems';


@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private readonly url = environment.apiUrl;
  private static readonly stripeAPIKey = 'STRIPE_API_KEY';

  constructor(private http: HttpClient) { }

  public createDeliveryAddressSession(cartItems: StripeChekoutModel[]): Observable<any> {
    const stripeAPIURL = this.url + 'CreateDeliveryAddressSession';
    const httpOptions = {
      headers: {
        'Authorization': `Bearer ${PaymentService.stripeAPIKey}`,
        'Content-Type': 'application/json'
      }
    };
    console.log('Sending createDeliveryAddressSession request with data:', cartItems);
    return this.http.post<any>(stripeAPIURL, cartItems, httpOptions);
  }

  public createCheckoutSession(cartItems: StripeChekoutModel[]): Observable<any> {
    const stripeAPIURL = this.url + 'CreateCheckoutSession';
    const httpOptions = {
      headers: {
        'Authorization': `Bearer ${PaymentService.stripeAPIKey}`,
        'Content-Type': 'application/json'
      }
    };
    console.log('Sending createCheckoutSession request with data:', cartItems);
    return this.http.post<any>(stripeAPIURL, cartItems, httpOptions);
  }

}
