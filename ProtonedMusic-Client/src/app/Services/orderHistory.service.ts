import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { environment } from "src/environments/environment";
import { OrderHistoryModel } from '../Models/OrderHistoryModel';

@Injectable({
  providedIn: "root"
})
export class OrderHistoryService {
  private readonly url = environment.apiUrl + "OrderHistory";

  constructor(private http: HttpClient ) {}

  public getOrderById(customerId: number): Observable<OrderHistoryModel[]> { 
    return this.http.get<OrderHistoryModel[]>(this.url + '/' + customerId);
  }

  public createOrder(order: OrderHistoryModel): Observable<OrderHistoryModel> {
    return this.http.post<OrderHistoryModel>(this.url + '/CreateOrder', order);
  }
}
