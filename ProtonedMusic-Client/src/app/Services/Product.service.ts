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

  public getAllProducts(): Observable<ProductModel[]> {
    return this.http.get<ProductModel[]>(this.url);
  }

  public createProduct(product: ProductModel): Observable<ProductModel> {
    const formData = new FormData();
  
    formData.append('name', product.name);
    formData.append('price', product.price.toString());
    formData.append('description', product.description);
    formData.append('productPicturePath', product.productPicturePath);
    product.categoryIds.forEach(categoryId => {
      formData.append('categoryIds', categoryId.toString());
    });

  
    if (product.pictureFile) {
      formData.append('pictureFile', product.pictureFile, product.pictureFile.name);
    }

    return this.http.post<ProductModel>(this.url, formData);
  }

  public updateProduct(productId:number, product: ProductModel): Observable<ProductModel> {
    const formData = new FormData();
  
    formData.append('name', product.name);
    formData.append('price', product.price.toString());
    formData.append('description', product.description);
    formData.append('productPicturePath', product.productPicturePath);
    product.categoryIds.forEach(categoryId => {
      formData.append('categoryIds', categoryId.toString());
    });
    formData.append('isDiscounted', JSON.stringify(product.isDiscounted));
    formData.append('discountProcent', product.discountProcent.toString());

  
    if (product.pictureFile) {
      formData.append('pictureFile', product.pictureFile, product.pictureFile.name);
    }

    return this.http.put<ProductModel>(this.url + '/' + productId, formData);
  }
  
  public deleteProduct(productId: number): Observable<ProductModel> {
    return this.http.delete<ProductModel>(this.url + '/' + productId);
  }

  public getProductById(productId: number): Observable<ProductModel> { 
    return this.http.get<ProductModel>(this.url + '/' + productId);
  }

  uploadProductPicture(productId: number, file: FormData): Observable<ProductModel> {
    return this.http.post<ProductModel>(this.url + '/upload-product-picture/' + productId, file);
  }
}


