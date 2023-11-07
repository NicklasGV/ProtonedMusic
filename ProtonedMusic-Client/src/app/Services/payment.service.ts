import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { CheckoutModel } from '../Models/CheckoutModel';

@Injectable({
  providedIn: 'root'
})

export class PaymentService {
  private readonly url = environment.apiUrl + 'Payment';

  constructor(private http: HttpClient) {}

  public createCheckoutSession(checkoutData: CheckoutModel): Observable<any> {
    return this.http.post<any>(this.url + '/createCheckoutSession', checkoutData);
  }
}

//create-checkout-session
