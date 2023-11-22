import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { StripeChekoutModel } from '../Models/StripeChekoutItems';
import { AccountInfo } from '../Models/AccountInfo';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private readonly url = environment.apiUrl;

  constructor(private http: HttpClient) {}

  public CreateAccountInfoSession(accountInfo: AccountInfo): Observable<any> {
    const stripeAPIURL = this.url + 'CreateAccountInfoSession';
    return this.http.post<any>(stripeAPIURL, accountInfo);

  };
  public CreateDeliveryAddressSession(previousSessionId: string): Observable<any> {
    const stripeAPIURL = this.url + 'CreateDeliveryAddressSession';
    return this.http.post<any>(stripeAPIURL, previousSessionId);
  }

  public createCheckoutSession(cartItems: StripeChekoutModel[]): Observable<any> {
    const stripeAPIURL = this.url + 'CreateCheckoutSession';
    const stripeAPIKey = 'sk_test_51MawfMFFxCTt81aXVC5LLXg1nzTYwEQLM20LidrDRVjR3FDF3SKhazAzDgaR9871rABLvbotyuLA14hjqYmboS2x00ujPqdm9F';
    const httpOptions = {
      headers: {
        'Authorization': `Bearer ${stripeAPIKey}`,
        'Content-Type': 'application/json'
      }
    };

    console.log('Sending createCheckoutSession request with data:', cartItems);

    return this.http.post<any>(stripeAPIURL, cartItems, httpOptions);
  }
}
