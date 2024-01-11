import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable, map } from 'rxjs';
import { environment } from "src/environments/environment";
import { OrderHistory } from '../Models/OrderHistoryModel';

@Injectable({
  providedIn: "root"
})
export class OrderHistoryService {
  private readonly url = environment.apiUrl + "OrderHistory";

  constructor(private http: HttpClient ) {}

  public GetOrdersByCustomerId(customerId: number): Observable<OrderHistory[]> {
    console.log(customerId);
    return this.http.get<OrderHistory[]>(this.url + '/' + customerId).pipe(
      // Ved hjælp af map-operatøren til at transformere data efter modtagels
      map((data: OrderHistory | OrderHistory[]) => Array.isArray(data) ? data : [data])
      // Hvis data er et array, returner det uændret; ellers, omslut det i et array
    );
  }
}
