import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CheckoutModel } from '../Models/CheckoutModel';

@Injectable({
  providedIn: 'root'
})

export class PaymentService {
  private readonly url = environment.apiUrl + 'Payment';

  constructor(private http: HttpClient) {}

  public createCheckoutSession(checkoutData: CheckoutModel): Observable<any> {
    return this.http.post<any>(this.url + '/CreateCheckoutSession', checkoutData);
  }
}

//create-checkout-session
