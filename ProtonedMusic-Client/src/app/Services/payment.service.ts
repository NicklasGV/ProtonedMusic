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
  private static readonly stripeAPIKey = 'STRIPE_API_KEY';

  constructor(private http: HttpClient) { }

  public CreateAccountInfoSession(accountInfo: AccountInfo): Observable<any> {
    const stripeAPIURL = this.url + 'CreateAccountInfoSession';
    const httpOptions = {
      headers: {
        'Authorization': `Bearer ${PaymentService.stripeAPIKey}`,
        'Content-Type': 'application/json'
      }
    };
    return this.http.post<any>(stripeAPIURL, accountInfo, httpOptions);
  };

  public CreateDeliveryAddressSession(previousSessionId: string): Observable<any> {
    const stripeAPIURL = this.url + 'CreateDeliveryAddressSession';
    const httpOptions = {
      headers: {
        'Authorization': `Bearer ${PaymentService.stripeAPIKey}`,
        'Content-Type': 'application/json'
      }
    };
    return this.http.post<any>(stripeAPIURL, previousSessionId, httpOptions);
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

  //Test for alle sessions
  public CreateCombinedSession(accountInfo: AccountInfo, cartItems: StripeChekoutModel[], previousSessionId: string): Observable<any> {
    const stripeAPIURL = this.url + 'CreateStripeSession';
    const httpOptions = {
      headers: {
        'Authorization': `Bearer ${PaymentService.stripeAPIKey}`,
        'Content-Type': 'application/json'
      }
    };

    const sessionData = {
      accountInfo: accountInfo,
      cartItems: cartItems,
      previousSessionId: previousSessionId
    };

    return this.http.post<any>(stripeAPIURL, sessionData, httpOptions);
  }

}
