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

  public createOrder(order: OrderHistoryModel, userId: any): Observable<OrderHistoryModel> {
    const formData = new FormData();

    formData.append('customerId', userId)
    formData.append('orderDate', order.orderDate.toISOString())
    order.productIds.forEach(productId => {
      formData.append('productIds', productId.toString());
    });

    return this.http.post<OrderHistoryModel>(this.url, formData);
  }
}
