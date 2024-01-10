import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Order } from "../Models/Order";
import { Observable } from 'rxjs';
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root"
})
export class OrderHistoryService {
  private readonly url = environment.apiUrl + "OrderHistory";

  constructor(private http: HttpClient ) {}

  public GetAllOrderHistory(customerId: string): Observable<Order[]> {
    console.log(customerId);
    return this.http.get<Order[]>(`${this.url}/${customerId}`);
  }
}
