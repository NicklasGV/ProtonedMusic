import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductModel } from '../Models/ProductModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private readonly url = environment.apiUrl + 'Product';

  constructor(private http: HttpClient) { }

  getAllProducts(): Observable<ProductModel[]> {
    return this.http.get<ProductModel[]>(this.url);
  }

  getProductById(productId: number): Observable<ProductModel> {
    return this.http.get<ProductModel>(this.url + '/' + productId);
  }

  createProduct(product: ProductModel): Observable<ProductModel> {
    return this.http.post<ProductModel>(this.url, product);
  }

  updateProduct(productId:number, product:ProductModel): Observable<ProductModel> {
    return this.http.put<ProductModel>(this.url + '/' + productId, product);
  }

  deleteProduct(productId:number): Observable<ProductModel> {
    return this.http.delete<ProductModel>(this.url + '/' +'id?id='+ productId);
  }
}


