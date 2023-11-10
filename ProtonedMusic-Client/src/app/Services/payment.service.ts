import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { StripeChekoutModel } from '../Models/StripeChekoutItems';

@Injectable({
  providedIn: 'root'
})

export class PaymentService {
  private readonly url = environment.apiUrl + 'Checkout';

  constructor(private http: HttpClient) {}

  public createCheckoutSession(cartItems: StripeChekoutModel[]): Observable<any> {
    const stripeAPIURL = 'https://localhost:7149/Checkout/CreateCheckoutSession';
    const stripeAPIKey = 'sk_test_51MawfMFFxCTt81aXVC5LLXg1nzTYwEQLM20LidrDRVjR3FDF3SKhazAzDgaR9871rABLvbotyuLA14hjqYmboS2x00ujPqdm9F';
    const httpOptions = {
      headers: {
        'Authorization': `Bearer ${stripeAPIKey}`,
        'Content-Type': 'application/json'
      }
    };
    return this.http.post<any>(stripeAPIURL, cartItems, httpOptions);
  }
}

